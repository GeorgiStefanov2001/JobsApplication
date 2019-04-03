using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobApplication.ViewModels
{
    public class CreateJobViewModel
    {
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a last name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "A name should only contain letters")]
        public string Name { get; set; }

        [Display(Name = "Salary")]
        public int Salary { get; set; }

        [Display(Name = "Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a last name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "A category should only contain letters")]
        public string Category { get; set; }

        public string Description { get; set; }

        [Display(Name = "Rerquired Experience")]
        public int? RequiredExperience { get; set; }

        [Display(Name = "Required Education")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a last name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "An education should only contain letters")]
        public string RequiredEducation { get; set; }
    }
}
