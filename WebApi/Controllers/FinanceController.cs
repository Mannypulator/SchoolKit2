using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/finance")]
    //authorise for burser
    public class FinanceController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<Principal> _principalManager;
        private  readonly UserManager<Teacher> _teacherManager;

        public FinanceController(SchoolKitContext context, UserManager<Student> studentManager, UserManager<Principal> principalManager,UserManager<Teacher> teacherManager)
        {
            _context = context;
            _studentManager = studentManager;
            _principalManager = principalManager;
            _teacherManager = teacherManager;
        }

        [HttpPost]
        [Route("addFee")]
        public async Task<IActionResult> AddFee(Fee feeModel)
        {  
            await _context.AddAsync(feeModel);
            await _context.SaveChangesAsync();

            var schoolID = _context.Terms
            .Where(x => x.TermID == feeModel.TermID)
            .Include(y => y.Session)
            .Select(f => f.Session.SchoolID)
            .SingleOrDefault(); 

            if(feeModel.FeeType == FeeType.General)
                {
                    var students = _studentManager.Users
                    .Where(x => x.SchoolID == schoolID && !x.HasGraduated);
                    
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
                    .Where(x => x.SchoolID == schoolID && !x.HasGraduated);
                    
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
                    .Where(x => x.SchoolID == schoolID && !x.HasGraduated);
                    
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
                else if(feeModel.FeeType == FeeType.Student)
                {
                    //check to see if this isn't needed
                    
                    //if fee type is student, send studentfee along with feeModel from the front end 
                    //and add everything at once
                }


                
            return Ok();
            
            
        }

        [HttpPost]
        [Route("addStudentFee")]
        public async Task<IActionResult> AddStudentFee(StudentFee model)
        {
            await _context.StudentFees.AddAsync(model);
            await _context.SaveChangesAsync();
            var student = await _studentManager.FindByIdAsync(model.StudentID);
            student.Balance = student.Balance - model.Amount;
            await _studentManager.UpdateAsync(student);

            return Ok();

        }

        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProduct(Product model)
        {
          
            await _context.Products.AddAsync(model);//it is from the expenses that we can know how many we have in stock
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("addProductExpense")]
        public async Task<IActionResult> AddProductExpense(Product model)
        {
            foreach(var expense in model.ProductExpenses){
                model.Stock = model.Stock + expense.Quantity;
            }
            
            await _context.ProductExpenses.AddRangeAsync(model.ProductExpenses);//it is from the expenses that we can know how many we have in stock
            _context.Products.Update(model);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("addProductSales")]
        public async Task<IActionResult> AddProductSales(Product model)
        {
            foreach(var sale in model.ProductSales){
                model.Stock = model.Stock - sale.Quantity;
                //sale.TimeStamp = DateTime.UtcNow.AddHours(1);
                await _context.ProductSales.AddAsync(sale);
            }
            
            _context.Products.Update(model);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("addGeneralExpense")]
        public async Task<IActionResult> AddGeneralExpense(GeneralExpense model)
        {   
            await _context.GeneralExpenses.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("getInflow")]
        public ActionResult GetInflow(Period period)
        {   
            if(period.Order == Order.Week)
            {
                var fPayments = _context.Sessions
                .Where(t => t.SchoolID == period.SchoolID)
                .Include(k => k.Terms)
                .ThenInclude(p => p.Fees)
                .ThenInclude(m => m.FeePayments)
                .SelectMany(t => t.Terms)
                .SelectMany(c => c.Fees)
                .SelectMany(c => c.FeePayments)
                .Where(x => x.TimeStamp >= period.StartDate && x.TimeStamp <= period.StopDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.AddDays(-(int)y.TimeStamp.DayOfWeek ));

                var pSales = _context.Products
                .Where(x => x.SchoolID == period.SchoolID)
                .Include(x => x.ProductSales)
                .SelectMany(c => c.ProductSales)
                .Where(x => x.TimeStamp >= period.StartDate && x.TimeStamp <= period.StopDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.AddDays(-(int)y.TimeStamp.DayOfWeek )); //continue from here

                return Ok(new {feePayments = fPayments, productSales = pSales});
            }
            else if(period.Order == Order.Month)
            {
                
                var fPayments = _context.Sessions
                .Where(t => t.SchoolID == period.SchoolID)
                .Include(k => k.Terms)
                .ThenInclude(p => p.Fees)
                .ThenInclude(m => m.FeePayments)
                .SelectMany(t => t.Terms)
                .SelectMany(c => c.Fees)
                .SelectMany(c => c.FeePayments)
                .Where(x => x.TimeStamp >= period.StartDate && x.TimeStamp <= period.StopDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.Month);

                var pSales = _context.Products
                .Where(x => x.SchoolID == period.SchoolID)
                .Include(x => x.ProductSales)
                .SelectMany(c => c.ProductSales)
                .Where(x => x.TimeStamp >= period.StartDate && x.TimeStamp <= period.StopDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.Month); //continue from here

                return Ok(new {feePayments = fPayments, productSales = pSales});
            }
            else if(period.Order == Order.Date)
            {
                
                var fPayments = _context.Sessions
                .Where(t => t.SchoolID == period.SchoolID)
                .Include(k => k.Terms)
                .ThenInclude(p => p.Fees)
                .ThenInclude(m => m.FeePayments)
                .SelectMany(t => t.Terms)
                .SelectMany(c => c.Fees)
                .SelectMany(c => c.FeePayments)
                .Where(x => x.TimeStamp >= period.StartDate && x.TimeStamp <= period.StopDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.Date);

                var pSales = _context.Products
                .Where(x => x.SchoolID == period.SchoolID)
                .Include(x => x.ProductSales)
                .SelectMany(c => c.ProductSales)
                .Where(x => x.TimeStamp >= period.StartDate && x.TimeStamp <= period.StopDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.Date); //continue from here

                return Ok(new {feePayments = fPayments, productSales = pSales});
            }
            else{

                var date = DateTime.UtcNow.AddHours(1);
                var startDate = date.AddDays(-(int)date.DayOfWeek);
                var endDate = startDate.AddDays(6);
                
                var fPayments = _context.Sessions
                .Where(t => t.SchoolID == period.SchoolID)
                .Include(k => k.Terms)
                .ThenInclude(p => p.Fees)
                .ThenInclude(m => m.FeePayments)
                .SelectMany(t => t.Terms)
                .SelectMany(c => c.Fees)
                .SelectMany(c => c.FeePayments)
                .Where(x => x.TimeStamp >= startDate && x.TimeStamp <= endDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.AddDays(-(int)y.TimeStamp.DayOfWeek ));

                var pSales = _context.Products
                .Where(x => x.SchoolID == period.SchoolID)
                .Include(x => x.ProductSales)
                .SelectMany(c => c.ProductSales)
                .Where(x => x.TimeStamp >= startDate && x.TimeStamp <= endDate)
                .OrderBy(x => x.TimeStamp)
                .GroupBy(y => y.TimeStamp.AddDays(-(int)y.TimeStamp.DayOfWeek )); //continue from here

                return Ok(new {feePayments = fPayments, productSales = pSales});
            }
            

        }

        ///make feepayments
        [HttpPost]
        [Route("addFeePayment")]
        public async Task<IActionResult> AddFeePayment(FeePaymentModel model)
        {
            await _context.FeePayments.AddAsync(model.FeePayment);
            model.StudentFee.AmountPaid = model.StudentFee.AmountPaid + model.FeePayment.AmountPaid;
            model.StudentFee.AmountOwed = model.StudentFee.AmountOwed - model.FeePayment.AmountPaid;
            _context.StudentFees.Update(model.StudentFee);
            await _context.SaveChangesAsync();
            var student = await _studentManager.FindByIdAsync(model.FeePayment.StudentID);
            student.Balance = student.Balance + model.FeePayment.AmountPaid;
            await _studentManager.UpdateAsync(student);

            return Ok();

        }

        [HttpPost]
        [Route("Products")]
        public async Task<IActionResult> GetProducts(int schoolID)
        {
            var products = _context.Products
            .Where(c => c.SchoolID == schoolID)
            .Include(t => t.ProductSales)
            .Include(p => p.ProductExpenses);

            return Ok(products);

        }
    }

    
}
