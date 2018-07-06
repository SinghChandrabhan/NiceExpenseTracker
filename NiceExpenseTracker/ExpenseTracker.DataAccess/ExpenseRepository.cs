using ExpenseTracker.Contracts.Entities;
using ExpenseTracker.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess
{
    public class ExpenseRepository : GenericRepository<ExpenseEntity>, IExpenseDataAccess
    {
        public ExpenseRepository(ExpenseTrackerDataContext context)
         : base(context)
        {

        }
        public async Task<IList<ExpenseEntity>> GetAllExpenses() => await GetAllAsync();
        public async Task<IList<ExpenseEntity>> GetExpensesAsync(Expression<Func<ExpenseEntity, bool>> match)
        {
            return await FindAllAsync(match);
        }
        public async Task<ExpenseEntity> AddExpense(ExpenseEntity expense)
        {
            await InsertAsync(expense);
            return expense;
        }       
    }
}
