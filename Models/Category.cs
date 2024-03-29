﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samostijna.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(5)")] 
        [Required(ErrorMessage = "Icon is required.")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")] 
        public string Type { get; set; } = "Expense";

        [NotMapped]
        public string TitleWithIcon => Icon + " " + Title;
    }
}
