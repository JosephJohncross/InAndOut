using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InAndOut.Core.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Borrower { get; set; }
        public string Lender { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
    }
}
