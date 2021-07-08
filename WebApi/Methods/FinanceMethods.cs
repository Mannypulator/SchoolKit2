using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Methods
{
    public class FinanceMethods
    {/*
        public async void AssignFees(Term term, int schoolID, Term prevTerm, UserManager<Student> _studentManager, SchoolKitContext _context)
        {
            //will be done at the creation of the fee
            var feeModels = _context.Fees.
            Where(x => x.TermID == term.TermID && x.SchoolID == schoolID);

            foreach( var feeModel in feeModels)
            {
                if(feeModel.FeeType == FeeType.General)
                {
                    var students = _studentManager.Users
                    .Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
                    foreach(var student in students)
                    {
                        var studentFee = new StudentFee{
                            StudentID = student.Id,
                            FeeID = feeModel.FeeID,
                            AmountOwed = feeModel.Amount
                        };
                       await _context.StudentFees.AddAsync(studentFee);
                       student.Balance = student.Balance - feeModel.Amount;
                       await _studentManager.UpdateAsync(student);
                    }
                    await _context.SaveChangesAsync();
                }

                else if(feeModel.FeeType == FeeType.Class)
                {
                    var students = _context.ClassArms
                    .Where(x => x.Class == feeModel.Class)
                    .Include(x => x.Students)
                    .SelectMany(x => x.Students)
                    .Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
                    foreach(var student in students)
                    {
                        var studentFee = new StudentFee{
                            StudentID = student.Id,
                            FeeID = feeModel.FeeID,
                            AmountOwed = feeModel.Amount
                        };
                       await _context.StudentFees.AddAsync(studentFee);
                       student.Balance = student.Balance - feeModel.Amount;
                       await _studentManager.UpdateAsync(student);
                    }
                    await _context.SaveChangesAsync();
                }

                else if(feeModel.FeeType == FeeType.ClassArm)
                {
                    var students = _context.ClassArms
                    .Where(x => x.ClassArmID == feeModel.ClassArmID)
                    .Include(x => x.Students)
                    .SelectMany(x => x.Students)
                    .Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
                    foreach(var student in students)
                    {
                        var studentFee = new StudentFee{
                            StudentID = student.Id,
                            FeeID = feeModel.FeeID,
                            AmountOwed = feeModel.Amount
                        };
                       await _context.StudentFees.AddAsync(studentFee);
                       student.Balance = student.Balance - feeModel.Amount;
                       await _studentManager.UpdateAsync(student);
                    }
                    await _context.SaveChangesAsync();
                }

                            }            
            
        }

        public async void AssignFees(Term term, int schoolID, UserManager<Student> _studentManager, SchoolKitContext _context)
        {
            
            var feeModels = _context.Fees.
            Where(x => x.TermID == term.TermID && x.SchoolID == schoolID);

            foreach( var feeModel in feeModels)
            {
                if(feeModel.FeeType == FeeType.General)
                {
                    var students = _studentManager.Users
                    .Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
                    foreach(var student in students)
                    {
                        var studentFee = new StudentFee{
                            StudentID = student.Id,
                            FeeID = feeModel.FeeID,
                            AmountOwed = feeModel.Amount
                        };
                       await _context.StudentFees.AddAsync(studentFee);
                       student.Balance = student.Balance - feeModel.Amount;
                       await _studentManager.UpdateAsync(student);
                    }
                    await _context.SaveChangesAsync();
                }

                else if(feeModel.FeeType == FeeType.Class)
                {
                    var students = _context.ClassArms
                    .Where(x => x.Class == feeModel.Class)
                    .Include(x => x.Students)
                    .SelectMany(x => x.Students)
                    .Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
                    foreach(var student in students)
                    {
                        var studentFee = new StudentFee{
                            StudentID = student.Id,
                            FeeID = feeModel.FeeID,
                            AmountOwed = feeModel.Amount
                        };
                       await _context.StudentFees.AddAsync(studentFee);
                       student.Balance = student.Balance - feeModel.Amount;
                       await _studentManager.UpdateAsync(student);
                    }
                    await _context.SaveChangesAsync();
                }

                else if(feeModel.FeeType == FeeType.ClassArm)
                {
                    var students = _context.ClassArms
                    .Where(x => x.ClassArmID == feeModel.ClassArmID)
                    .Include(x => x.Students)
                    .SelectMany(x => x.Students)
                    .Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
                    foreach(var student in students)
                    {
                        var studentFee = new StudentFee{
                            StudentID = student.Id,
                            FeeID = feeModel.FeeID,
                            AmountOwed = feeModel.Amount
                        };
                       await _context.StudentFees.AddAsync(studentFee);
                       student.Balance = student.Balance - feeModel.Amount;
                       await _studentManager.UpdateAsync(student);
                    }
                    await _context.SaveChangesAsync();
                }

            }            
            
        }*/
    }
}