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
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string Option1 { get; set; }

        [Required]
        public string Option2 { get; set; }

        [Required]
        public string Option3 { get; set; }

        [Required]
        public string Option4 { get; set; }

        [Required]
        public int TrueOption { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }


        [ForeignKey(nameof(InnerCategory))]

        public InnerCategory InnerCategory { get; set; }
        public override string ToString()
        {
            return $"QuestionId: {QuestionId}\tQuestionText: {QuestionText}\tOption1: {Option1}\tOption2: {Option2}\tOption3: {Option3}\tOption4: {Option4}\tTrueOption: {TrueOption}\tDifficulty: {Difficulty}\tInnerCategory: {InnerCategory.InnerCategoryId}";
        }
    }
}
