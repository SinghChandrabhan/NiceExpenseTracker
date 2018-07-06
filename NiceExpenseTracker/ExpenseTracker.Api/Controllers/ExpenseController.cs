using ExpenseTracker.Api.Models;
using ExpenseTracker.Contracts.Entities;
using ExpenseTracker.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Expense")]
    public class ExpenseController : Controller
    {
        #region Private Member
        private IExpenseDataAccess _iExpenseDataAccess;
        private IConfiguration _configuration;
        #endregion 
        public ExpenseController(IExpenseDataAccess iExpenseDataAccess, IConfiguration configuration)
        {
            _iExpenseDataAccess = iExpenseDataAccess;
            _configuration = configuration;
        }

        #region API Methods
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //Important: I avoided using a layer between API and DataAccess to keep it simple
                //In more complex cases, we should consider bringing Business layer in between
                var expenses = await _iExpenseDataAccess.GetAllExpenses();

                if (expenses?.Count == 0)
                    return Ok(_configuration["HttpErroMessages:NotFound"]);

                return Ok(expenses);
            }
            catch(Exception ex)
            {
                //TODO:Catch and log exception
                return StatusCode((int)HttpStatusCode.InternalServerError, _configuration["HttpErroMessages:ServerError"]);
            }
        }

        [HttpGet("{name}/{month}/{year}")]
        public async Task<IActionResult> Get(string name, int month, int year)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest(_configuration["HttpErroMessages:BadQuery"]);

            try
            {
                //Important: I avoided using a layer between API and DataAccess to keep it simple
                //In more complex cases, we should consider bringing Business layer in between
                Expression<Func<ExpenseEntity, bool>> searchPredicate = (entity) =>
                    string.Equals(entity.Name, name, StringComparison.InvariantCultureIgnoreCase)
                    && entity.DateSubmitted.Month == month
                    && entity.DateSubmitted.Year == year;

                var allexpenses = await _iExpenseDataAccess.GetExpensesAsync(searchPredicate);
                return Ok(allexpenses);
            }
            catch (Exception ex)
            {
                //TODO:Catch and log exception
                return StatusCode((int)HttpStatusCode.InternalServerError, _configuration["HttpErroMessages:ServerError"]);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(ExpenseModel value)
        {
            if (!ModelState.IsValid)           
                return BadRequest(ModelState);           
            try
            {
                var expenseEntiry = new ExpenseEntity()
                {
                    Name = value.Name,
                    Amount = value.Amount,
                    Category = value.Category,
                    DateSubmitted = value.DateSubmitted
                };

                var createdExpense = await _iExpenseDataAccess.AddExpense(expenseEntiry);
                return Created($"/api/values/{createdExpense.Name}/{createdExpense.DateSubmitted.Month}/{createdExpense.DateSubmitted.Year}"
                    , createdExpense);
            }
            catch (Exception ex)
            {
                //TODO:Catch and log exception
                return StatusCode((int)HttpStatusCode.InternalServerError, _configuration["HttpErroMessages:ServerError"]);
            }
        }
        #endregion
    }
}
