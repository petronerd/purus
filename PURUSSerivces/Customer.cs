//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Services
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.Quotes = new HashSet<Quote>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> YearsOfInsurance { get; set; }
        public string City { get; set; }
        public string InsuranceType { get; set; }
    
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
