using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models
{
    public class Customer
    {
        [Key]
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
    }
}
