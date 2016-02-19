using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services;

namespace PURUSInsurance.Models
{

    public class RegistrationViewModel
    {
        [Key]
        public int ID { get; set; }

        string fullName;
        string email;
        int age;
        int yearsOfInsurance;
        string city;
        InsuranceType insuranceType;

        [Required]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        [Required]
        [Display(Name = "Total Years of Insurance")]
        public int YearsOfInsurance
        {
            get { return yearsOfInsurance; }
            set { yearsOfInsurance = value; }
        }

        [Required]
        [Display(Name = "City")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        [Required]
        [Display(Name = "Insurance Type")]
        public InsuranceType InsuranceType
        {
            get { return insuranceType; }
            set { insuranceType = value; }
        }



    }

    public class QuotesViewModel
    {
        [Key]
        public int ID { get; set; }

        string fullName;
        string email;

        decimal minimumCoverage;
        decimal maximumCoverage;
        QuoteStatus status;

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        [Display(Name = "Email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Display(Name = "Minimum Coverage")]
        public decimal MinimumCoverage
        {
            get { return minimumCoverage; }
            set { minimumCoverage = value; }
        }

        [Display(Name = "Maximum Coverage")]
        public decimal MaximumCoverage
        {
            get { return maximumCoverage; }
            set { maximumCoverage = value; }
        }


        [Display(Name = "Status")]
        public QuoteStatus Status
        {
            get { return status; }
            set { status = value; }
        }



    }

    
    
}