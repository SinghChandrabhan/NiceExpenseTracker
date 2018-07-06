using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Models
{
    public class ExpenseModel
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        public decimal Amount { get; set; }
        public string Category { get; set; }
        
        public DateTime DateSubmitted { get; set; }
    }
}
