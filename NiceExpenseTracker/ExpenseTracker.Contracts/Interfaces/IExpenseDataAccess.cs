using ExpenseTracker.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Contracts.Interfaces
{
    public interface IExpenseDataAccess
    {
        Task<IList<ExpenseEntity>> GetAllExpenses();
        Task<IList<ExpenseEntity>> GetExpensesAsync(Expression<Func<ExpenseEntity, bool>> match);
        Task<ExpenseEntity> AddExpense(ExpenseEntity expense);
    }
}
