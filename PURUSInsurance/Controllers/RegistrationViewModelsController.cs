using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PURUSInsurance.Models;
using Services;

namespace PURUSInsurance.Controllers
{
    public class RegistrationViewModelsController : Controller
    {
        private IQuote QuotesService; 
        private ICustomer CustomersService;

        public RegistrationViewModelsController(IQuote QuotesService, ICustomer UserService)
        {
            this.QuotesService = QuotesService;
            this.CustomersService = UserService;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback Feedback;

                Customer Customer = new Customer();
                Customer.Email = registrationViewModel.Email;
                Customer.Password = registrationViewModel.ConfirmPassword;
                Customer.Age = registrationViewModel.Age;
                Customer.City  = registrationViewModel.City;
                Customer.FullName = registrationViewModel.FullName;
                Customer.YearsOfInsurance = registrationViewModel.YearsOfInsurance;
                Customer.InsuranceType = "Unknown";

                Feedback = CustomersService.Add (Customer);
                
                if (Feedback.Result) 
                {

                Quote Quote = new Quote();
                Quote.Status = QuoteStatus.Submitted.ToString();
                Quote.CustomerID = CustomersService.GetByEmail(Customer.Email).ID;

                QuotesService.Add(Quote);
                       

                     return RedirectToAction("Index");

                }
                 else
                {

                     ViewBag.ErrorMessage = Feedback.Result;
                     return View(registrationViewModel);
                }
                
            }

            return View(registrationViewModel);
        }

    }
}
