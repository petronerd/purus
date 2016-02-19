using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services
{
     public class SearchModel
    {
        public SearchModel()
        {

        }
        public int NumberOfResultPerPage {get; set;}
        public string InsuranceType {get; set;}
        public string Location { get; set; }
        public int? MinimumCoverage {get; set;}
        public int? MaximumCoverage { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? MinTotalNumberofYearsInsurance { get; set; }
        public int? MaxTotalNumberofYearsInsurance { get; set; }
        public int Page { get; set; }
    }
}