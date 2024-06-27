using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Task_TodoList.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; } = string.Empty;
    }
}
