using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Data.Models
{
    public class User
    {
        public User()
        {
            UserCreatedOn = DateTime.UtcNow;
            Projects = new List<Project>();
        }

        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage = "Password should be at least 4 characters")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword{ get; set; }

        //[ForeignKey("CV")]
        //public int CVId { get; set; }

        //public CV UserCV { get; set; }

        public ICollection<Project> Projects { get; set; }

        public DateTime UserCreatedOn { get; set; }
    }
}
