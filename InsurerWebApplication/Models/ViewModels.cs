

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services;

namespace PURUSInsurance.Models
{

    public class SearchPageViewModel
    {
        public SearchPageViewModel()
        {

        }

        List<CustomersViewModel> customersViewModel;
        SearchModel searchModel;

        public List<CustomersViewModel> CustomersViewModel { get {return customersViewModel;}  set{customersViewModel=value;}}
        public SearchModel SearchModel { get { return searchModel; } set { searchModel = value; } }
    }

    public class CustomersViewModel
    {
        public CustomersViewModel()
        {

        }

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

        public QuotesViewModel()
        {

        }
        
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