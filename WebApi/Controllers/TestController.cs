using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllersp
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Teacher> _teacherManager;
        private readonly UserManager<Student> _studentManager;

        public Timer timer;
        public TestController(SchoolKitContext context, UserManager<Teacher> teacherManager, UserManager<Student> studentManager)
        {
            _context = context;
            _teacherManager = teacherManager;
            _studentManager = studentManager;
        }

        [HttpPost]
        [Route("create")]
        //authorise for teachers only
        public async Task<IActionResult> Create(Test model)
        { // create both test and questions for the test
        // remember to test
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var user = await _teacherManager.FindByIdAsync(userId);
            int SchoolID = user.SchoolID;
            var termId = _context.Sessions
                .Where(x => x.SchoolID == SchoolID && x.Current)
                .Include(x => x.Terms)
                .SelectMany(x => x.Terms)
                .Where(x => x.Current)
                .Select(x => x.TermID)
                .FirstOrDefault();
            
            model.SchoolID = SchoolID;   
            //let teacher choose start and close date 
            model.TermID = termId;
            await _context.Tests.AddAsync(model);
            await _context.SaveChangesAsync();

            int totalTime = (int)(model.CloseDate - model.StartDate).TotalMilliseconds;
            int ID = model.TestID;

            timer = new Timer(
            callback: new TimerCallback(CloseTest),
            state: ID,
            dueTime: totalTime,
            period: Timeout.Infinite);

            return Ok(model.TestID);/// create test and add questions/////////////make sure to check that this works
        }

        [HttpPost]
        [Route("add")]
        // authorise for teachers only
        public async Task<IActionResult> Question(Question model)
        { 
           await _context.Questions.AddAsync(model);//remember to test......................
            return Ok(model.TestID);
        }

        [HttpGet]
        [Route("check")]
        //authorize for students only
        public async Task<TestAttempt> Check(int testId)
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var student  = await _studentManager.FindByIdAsync(studentId);

           var attempt = await _context.TestAttempts
           .Where(x => x.TestID == testId && x.StudentID == studentId)
           .SingleOrDefaultAsync();
           //check time and update status
            return attempt;
        }

        [HttpGet]
        [Route("start")]
        //authorize for students only
        public async Task<IActionResult> Start(int testId)
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var student  = await _studentManager.FindByIdAsync(studentId);

            List<RQAttemptModel> rQuestions = new List<RQAttemptModel>();

           var attempt = await _context.TestAttempts
           .Where(x => x.TestID == testId && x.StudentID == studentId)
           .SingleOrDefaultAsync();

           int qNumber = await _context.Tests
           .Where(c => c.TestID == testId)
           .Select(g => g.QNPS)
           .SingleOrDefaultAsync();
           
            if(attempt == null ){
                var questions = await _context.Tests
                .Include(c => c.Questions)
                .ThenInclude(c => c.Options)
                .Where(t => t.TestID == testId)
                .SelectMany(e => e.Questions)
                .Select(e => new Question{
                    QuestionID = e.QuestionID,
                    Qn = e.Qn,
                    QType = e.QType,
                    TestID = e.TestID,
                    Options = e.Options
                })
                .OrderBy(y => Guid.NewGuid()).Take(qNumber) 
                .ToListAsync(); 

            var testAttempt = new TestAttempt{
                    StartTime = DateTime.UtcNow.AddHours(1),
                    Score = 0,
                    StudentID = studentId,
                    TestID = testId
                }; 

                await _context.TestAttempts.AddAsync(testAttempt);
                await _context.SaveChangesAsync();

                foreach(var question in questions)
                {
                     Random random = new Random();
                     int a = random.Next(0,100);
                     Random orderR = new Random(a);
                    var queAttempt = new QAttempt{
                        TestAttemptID = testAttempt.AttemptID,
                        QuestionID = question.QuestionID,
                        OptSeed = a
                    };
                   await _context.QuestionAttempts.AddAsync(queAttempt);
                   await _context.SaveChangesAsync();

                   question.Options = question.Options.OrderBy(d => orderR.Next()).ToList();

                   var rQ = new RQAttemptModel{
                       QAttemptID = queAttempt.QuestionAttemptID,
                       Question = question
                   };

                   rQuestions.Add(rQ);////////////test in class controller tonight
               
                }
                 
                return Ok(rQuestions.Take(5).ToList());
            }
            else {
                var timeNow = DateTime.UtcNow.AddHours(1);
                var startTime = attempt.StartTime;
                int TimeSpent = (startTime - timeNow).Minutes;

                var testMinutes = await _context.Tests.
                Where(i => i.TestID == testId)
                .Select(i => i.Minutes)
                .SingleOrDefaultAsync();
                if(!(TimeSpent > testMinutes) && !attempt.Finished)
                {
                    //get question attempts
                    var queAttempts = await _context.TestAttempts
                    .Where(x => x.TestID == testId && x.StudentID == studentId)
                    .Include(x => x.QuestionAttempts)
                    .ThenInclude(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .SelectMany(x => x.QuestionAttempts).OrderBy(x => x.QuestionAttemptID).Take(5).ToListAsync();

                    foreach(var queAttempt in queAttempts)
                    {
                        Random orderR = new Random(queAttempt.OptSeed);

                       var rQ = new RQAttemptModel{
                        QAttemptID = queAttempt.QuestionAttemptID,
                        Question = new Question{
                             QuestionID = queAttempt.QuestionID,
                             Qn = queAttempt.Question.Qn,
                            QType = queAttempt.Question.QType,
                            TestID = queAttempt.Question.TestID,
                            Options = queAttempt.Question.Options.OrderBy(c => orderR.Next()).ToList()
                        }
                    };

                    rQuestions.Add(rQ);
                    }   
                }
                else{
                    if(!attempt.Finished){
                        attempt.TimeSpent = testMinutes;
                        attempt.Finished = true;
                        _context.Update(attempt);
                        _context.SaveChanges();
                    }
                    
                }
                
                return Ok(rQuestions);
            }
            
        }

        [HttpPost]
        [Route("Qsubmit")]
        public async Task<IActionResult> QSubmit(SubmissionModelWP models)
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var attempt = await _context.TestAttempts
           .Where(x => x.TestID == models.TestID && x.StudentID == studentId)
           .SingleOrDefaultAsync();

            List<RQAttemptModel> rQuestions = new List<RQAttemptModel>();

            if(!(attempt == null))
            {
                var timeNow = DateTime.UtcNow.AddHours(1);
                var startTime = attempt.StartTime;
                int TimeSpent = (startTime - timeNow).Minutes;

                var testMinutes = await _context.Tests.
                Where(i => i.TestID == models.TestID)
                .Select(i => i.Minutes)
                .SingleOrDefaultAsync();
                if(!(TimeSpent > testMinutes) && !attempt.Finished)
                {
                    //submit options picked
                       foreach(var model in models.model)
                       {
                           foreach(var qp in model.QAOptions)
                           {
                               var qAOptions = new QAttemptsOption{
                                   QuestionAttemptID = model.QAttemptID,
                                   OptionID = qp
                                   };   
                            }
                        }
                        await _context.SaveChangesAsync();
                    //get question attempts
                    var queAttempts = await _context.TestAttempts
                    .Where(x => x.TestID == models.TestID && x.StudentID == studentId)
                    .Include(x => x.QuestionAttempts)
                    .ThenInclude(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .SelectMany(x => x.QuestionAttempts).OrderBy(x => x.QuestionAttemptID).Skip(models.PageNo*5).Take(5).ToListAsync();

                    foreach(var queAttempt in queAttempts)
                    {
                        Random orderR = new Random(queAttempt.OptSeed);

                       var rQ = new RQAttemptModel{
                        QAttemptID = queAttempt.QuestionAttemptID,
                        Question = new Question{
                             QuestionID = queAttempt.QuestionID,
                             Qn = queAttempt.Question.Qn,
                            QType = queAttempt.Question.QType,
                            TestID = queAttempt.Question.TestID,
                            Options = queAttempt.Question.Options.OrderBy(c => orderR.Next()).ToList()
                        }
                    };

                    rQuestions.Add(rQ);
                    }   
                }
                else{
                   if(!attempt.Finished){
                        attempt.TimeSpent = testMinutes;
                        attempt.Finished = true;
                        attempt.Score = (int)Math.Round(Score(models.TestID, studentId));
                        _context.Update(attempt);
                        _context.SaveChanges();
                    }
                    
                }
                
            }
            
            return Ok(rQuestions);
        }

        [HttpPost]
        [Route("Fsubmit")]
        public async Task<IActionResult> FSubmit(SubmissionModelWP models)
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var attempt = await _context.TestAttempts
           .Where(x => x.TestID == models.TestID && x.StudentID == studentId)
           .SingleOrDefaultAsync();

            if(!(attempt == null))
            {
                var timeNow = DateTime.UtcNow.AddHours(1);
                var startTime = attempt.StartTime;
                int TimeSpent = (startTime - timeNow).Minutes;

                var testMinutes = await _context.Tests.
                Where(i => i.TestID == models.TestID)
                .Select(i => i.Minutes)
                .SingleOrDefaultAsync();
                if(!(TimeSpent > testMinutes) && !attempt.Finished)
                {
                    //submit options picked
                       foreach(var model in models.model)
                       {
                           foreach(var qp in model.QAOptions)
                           {
                               var qAOptions = new QAttemptsOption{
                                   QuestionAttemptID = model.QAttemptID,
                                   OptionID = qp
                                   };   
                            }
                        }
                     await _context.SaveChangesAsync();

                     attempt.TimeSpent = TimeSpent;
                     attempt.Finished = true;
                     attempt.Score = (int)Math.Round(Score(models.TestID, studentId));
                     _context.Update(attempt);
                     _context.SaveChanges();
                     
                }
                else{
                    if(!attempt.Finished){
                        attempt.TimeSpent = testMinutes;
                        attempt.Finished = true;
                        attempt.Score = (int)Math.Round(Score(models.TestID, studentId));
                        _context.Update(attempt);
                        _context.SaveChanges();
                    }
                }

            }
            return Ok();
        }

        [HttpGet]
        [Route("getTests")]
        public async Task<List<Test>> GetTests()
        { 
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var student  = await _studentManager.FindByIdAsync(studentId);

            var term = _context.Sessions
                .Where(x => x.SchoolID == student.SchoolID && x.Current)
                .Include(x => x.Terms)
                .SelectMany(x => x.Terms)
                .Where(x => x.Current)
                .Select(x => x.TermID)
                .FirstOrDefault();

            var tests = await _context.Enrollments
            .Include(x => x.ClassSubject)
            .ThenInclude(x => x.Tests)
            .Where(x => x.StudentID == studentId && x.TermID == term)
            .SelectMany(x => x.ClassSubject.Tests)
            .Where(x => x.SchoolID == student.SchoolID && x.TermID == term).ToListAsync();

            return tests;
        }

        [HttpGet]
        [Route("teachertests")]
        //authorise for teacher
        public async Task<List<Test>> GetTeacherTests()
        { 
            var teacherId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var teacher  = await _teacherManager.FindByIdAsync(teacherId);

            var term = _context.Sessions
                .Where(x => x.SchoolID == teacher.SchoolID && x.Current)
                .Include(x => x.Terms)
                .SelectMany(x => x.Terms)
                .Where(x => x.Current)
                .Select(x => x.TermID)
                .FirstOrDefault();

            var tests = await _context.TeacherSubjects
            .Include(x => x.ClassSubject)
            .ThenInclude(x => x.Tests)
            .Where(x => x.TeacherID == teacherId)
            .SelectMany(x => x.ClassSubject.Tests)
            .Where(x => x.SchoolID == teacher.SchoolID && x.TermID == term).ToListAsync();
            return tests;
        }

        [HttpGet]
        [Route("attempts")]
        public async Task<List<AttemptsModel>> TestsAttempts(int TestId)
        { 
            
            var attempts = await _context.Tests
            .Include(x => x.Attempts)
            .ThenInclude(x => x.Student)
            .Where(x => x.TestID == TestId)
            .SelectMany(x => x.Attempts)
            .Select(x => new AttemptsModel{
                 AttemptID = x.AttemptID,
                 TimeSpent = x.TimeSpent,
                 Score = x.Score,
                 StudentID = x.StudentID,
                 StudentName = x.Student.FirstName + " " +x.Student.LastName
                 
            }).ToListAsync();
            
            return attempts;
        }


        public void CloseTest(object testId)
        {
            //close tests yet to be closed
            
            using(var context = new SchoolKitContext())
            {
                var testAttempts = context.TestAttempts
                .Where(x => x.TestID == (int)testId && x.Finished == false);
                
                var test = context.Tests.Where(x => x.TestID == (int)testId).FirstOrDefault();
                test.Closed = true;
                foreach ( var testAttempt in testAttempts)
                {
                        testAttempt.TimeSpent = test.Minutes;
                        testAttempt.Finished = true;
                        testAttempt.Score = (int)Math.Round(Score(testAttempt.TestID, testAttempt.StudentID));
                        context.Update(testAttempt);   
                }

                context.SaveChanges();
                
            }
        }
        
        public double Score (int testId, string studentId)
        {
                     double score = 0;
                     var queAttempts = _context.TestAttempts
                    .Where(x => x.TestID == testId & x.StudentID == studentId)
                    .Include(x => x.QuestionAttempts)
                    .SelectMany(x => x.QuestionAttempts)
                    .Include(x => x.Question)
                    .Include(x => x.QAttemptsOptions)
                    .ThenInclude(x => x.Option);
                    foreach(var queAttempt in queAttempts)
                    {
                        if ( queAttempt.Question.QType == QuestionType.Radio)
                        {
                            
                            if (queAttempt.QAttemptsOptions.FirstOrDefault().Option.Correct)
                            {
                                score += queAttempt.Question.Mark;
                            }
                          
                        }
                        else{
                            int correctOptCount = 0;
                            if(queAttempt.Question.AllOptionsCorrect)
                            {
                                foreach(var qAOption in queAttempt.QAttemptsOptions)
                                {
                                    if(qAOption.Option.Correct)
                                    {
                                        correctOptCount++;
                                    }
                                }
                                if(correctOptCount == queAttempt.Question.CorrectOptions)
                                {
                                    score += queAttempt.Question.Mark;
                                }
                            }
                            else
                            {
                                foreach(var qAOption in queAttempt.QAttemptsOptions)
                                {
                                    if(qAOption.Option.Correct)
                                    {
                                        correctOptCount++;
                                    }
                                }
                                var markPerOpt = queAttempt.Question.Mark/queAttempt.Question.CorrectOptions;
                                score += (correctOptCount*markPerOpt);
                            }

                        }
                        
                    }
                    return score;
        }

        [HttpGet]
        [Route("tests")]
        //authorise for students
        public async Task<IQueryable<Test>> Tests()
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var student =  await _studentManager.FindByIdAsync(studentId);

           var term = _context.Sessions
                    .Where(x => x.SchoolID == student.SchoolID && x.Current)
                    .Include(x => x.Terms)
                    .SelectMany(x => x.Terms)
                    .Where(x => x.Current)
                    .Select(x => x.TermID)
                    .FirstOrDefault();

            var tests = _context.Enrollments
            .Include(x => x.ClassSubject)
            .ThenInclude(x => x.Tests)
            .ThenInclude(x => x.Attempts)
            .Where( x => x.TermID == term && x.StudentID == studentId)
            .SelectMany(x => x.ClassSubject.Tests)
            .Where(x => x.TermID == term && !x.Closed && x.Attempts.Any(p => (p.StudentID == studentId && !p.Finished) || !(p.StudentID == studentId)));

            return tests;
        }
    }
}
