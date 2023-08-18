using OnlineQuiz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Domain.Models
{
    public class InnerCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InnerCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string InnerCategoryName { get; set; }   
        
        public TimeSpan Time { get; set; }

        [ForeignKey(nameof(CategoryName))]

        public Category CategoryName { get; set; }
        public override string ToString()
        {
            return $"InnerCategoryId: {InnerCategoryId}\tInnerCategoryName: {InnerCategoryName}\tTime: {Time}\tCategory_Name: {CategoryName}";
        }
    }
}
