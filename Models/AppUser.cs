using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Models
{
    public class DateOfBirthAttribute : RangeAttribute
    {
        public DateOfBirthAttribute()
            : base(typeof(DateTime), "1/1/1900", DateTime.Now.AddYears(-18).ToShortDateString())
        {
            ErrorMessage = $"Date of Birth year must be at least 1900 and you should be at leas 18 years old.";
        }
    }

    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = null!;

        

        public string? UserGender { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DateOfBirth]
        public DateTime? DateOfBirth { get; set; }

        public int? Age
        {
            get
            {
                if (DateOfBirth == null)
                {
                    return null; // Age is undefined for users with no birthdate
                }

                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Value.Year;

                // Check if the birthday has occurred for the current year
                if (DateOfBirth.Value.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }


        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Fake card number must be a 16-digit number.")]
        public string? CardNumber { get; set; }
    }
}