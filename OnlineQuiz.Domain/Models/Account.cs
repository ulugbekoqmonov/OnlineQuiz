using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Domain.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(13)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"AccountId: {AccountId}\tUsername: {Username}\tPassword: {Password}\tEmail: {Email}\tPhoneNumber: {PhoneNumber}";
        }
    }
}
