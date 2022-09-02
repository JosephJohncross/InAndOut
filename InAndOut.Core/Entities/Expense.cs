using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InAndOut.Core.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Expense Name")]
        [Required]
        public string ExpenseName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be graetaer than 0")]
        public decimal Amount { get; set; }
    }
}
