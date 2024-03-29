using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.VisualBasic;

namespace Samostijna.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Select a category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Note{ get; set; }

        public DateTime Data { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon => Category?.Icon + " " + Category?.Title;
			
        [NotMapped]
        public string? AmountWithCurrencyIndex =>  (((Category == null || Category.Type == "Expense") ? "-" : "+") + Amount.ToString("C0"));

    }
}
