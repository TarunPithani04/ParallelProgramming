using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Status")]
        public string StatusName { get; set; }
    }
}