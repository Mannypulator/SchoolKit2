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
            
            var term = _context.Terms
            .Where(x => x.Current == true )
            .SingleOrDefault();
            if(term == null){
                return BadRequest(new {Message = "There is no active term"});
            }
            else{
                List<Claim> claims = User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                var isDirector = claims.Any(x => x.Value == "Director");
                if(isDirector){
                    var UserID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                    var director = await _principalManager.FindByIdAsync(UserID);
                    feeModel.SchoolID = director.SchoolID;
                }
                else{
                    var UserID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                    var burser = await _teacherManager.FindByIdAsync(UserID);
                    feeModel.SchoolID = burser.SchoolID;
                }
                feeModel.TermID = term.TermID;
                await _context.Fees.AddAsync(feeModel);
                await _context.SaveChangesAsync();
                
                if(feeModel.FeeType == FeeType.General)
                {
                    var students = _studentManager.Users.Where(x => x.SchoolID == feeModel.SchoolID && !x.HasGraduated);
                    
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

                else if(feeModel.FeeType == FeeType.Student)
                {
                    var students = feeModel.Students;
                    
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
                return Ok();
            }
            
        }

        [HttpPost]
        [Route("addStudentFee")]
        public async Task<IActionResult> AddStudentFee(StudentFee model)
        {
            await _context.StudentFees.AddAsync(model);
            await _context.SaveChangesAsync();
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
            foreach(var sales in model.ProductSales){
                model.Stock = model.Stock - sales.Quantity;
            }
            
            await _context.ProductSales.AddRangeAsync(model.ProductSales);
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


        

        // GET: Finance/Details/5
    }
}
