using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class LoanMessages
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Description")]
        public string LoanRequest { get; set; }
        [Required]
        [Range(0, 9999999.99, ErrorMessage = "Value must be between 0 and 9999999.99")]
        [Display(Name = "Loan Amount")]
        public Decimal Amount { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status status { get; set; }
        public string Message { get; set; }
    }
}
