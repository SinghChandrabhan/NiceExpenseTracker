using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Contracts.Entities
{
    public class ExpenseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
