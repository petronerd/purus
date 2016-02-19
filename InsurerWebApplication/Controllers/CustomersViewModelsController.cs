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

namespace InsurerWebApplication.Controllers
{
    public class CustomersViewModelsController : Controller
    {
        private ICustomer CustomersService;
        private IQuote QuoteService;

        public CustomersViewModelsController (ICustomer CustomersService, IQuote QuoteService)
        {
            this.CustomersService = CustomersService;
            this.QuoteService = QuoteService;
        }

        private MyInsuranceDBEntities db = new MyInsuranceDBEntities();


        public List<CustomersViewModel> GetCustomersViewModel(SearchModel SearchModel)
        {
            List<CustomersViewModel> ViewList = new List<CustomersViewModel>();

            List<Customer> Customers = CustomersService.GetAll(SearchModel);
            foreach (Customer Customer in Customers)
            {
                CustomersViewModel CustomersViewModel = new CustomersViewModel();
                CustomersViewModel.FullName = Customer.FullName;
                CustomersViewModel.Age = Customer.Age.GetValueOrDefault();
                CustomersViewModel.City = Customer.City;
                CustomersViewModel.Email = Customer.Email;
                CustomersViewModel.InsuranceType = ((InsuranceType)Enum.Parse(typeof(InsuranceType), Customer.InsuranceType));
                CustomersViewModel.ID = Customer.ID;

                ViewList.Add(CustomersViewModel);
            }

            return ViewList;
        }


        [HttpPost]
        public ActionResult Index(SearchPageViewModel SearchPageViewModel)
        {
            SearchPageViewModel.CustomersViewModel = GetCustomersViewModel(SearchPageViewModel.SearchModel);
            return View(SearchPageViewModel);
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<CustomersViewModel> ViewList = new List<CustomersViewModel>();

            SearchModel SearchModel = new SearchModel();
            SearchModel.Page = 1;
            SearchModel.NumberOfResultPerPage = 7;
            
            List<Customer> Customers = CustomersService.GetAll(SearchModel);
            foreach (Customer Customer in Customers)
            {
                CustomersViewModel CustomersViewModel = new CustomersViewModel();
                CustomersViewModel.FullName = Customer.FullName;
                CustomersViewModel.Age = Customer.Age.GetValueOrDefault();
                CustomersViewModel.City = Customer.City;
                CustomersViewModel.Email = Customer.Email;
                CustomersViewModel.InsuranceType = ((InsuranceType)Enum.Parse(typeof(InsuranceType), Customer.InsuranceType));
                CustomersViewModel.ID = Customer.ID;

                ViewList.Add(CustomersViewModel);
            }

            SearchPageViewModel SearchPageViewModel = new SearchPageViewModel();

            SearchPageViewModel.CustomersViewModel = ViewList;
            SearchPageViewModel.SearchModel = SearchModel;

            return View(SearchPageViewModel);
        }

        public ActionResult GetQuotePartialView(int UserID)
        {
            try
            {

                Customer Customer = CustomersService.GetByID(UserID);
                Quote Quote = QuoteService.GetByUserID(UserID);

            QuotesViewModel  QuotesViewModel = new QuotesViewModel();

            QuotesViewModel.FullName = Customer.FullName;
            QuotesViewModel.MaximumCoverage = Quote.MaximumCoverage;
            QuotesViewModel.MinimumCoverage = Quote.MinimumCoverage;
            QuotesViewModel.ID = Quote.ID;
            return Json(RenderPartialViewToString(this, "~/Views/CustomersViewModels/QuotePartialView.cshtml", QuotesViewModel));
            }
            catch (Exception E)
            {
                return Json("No quote added by this user");
            }
        }


        public ActionResult UpdateQuote(QuotesViewModel QuoteViewModel)
        {
            try
            {

            Quote Quote = new Quote ();
            Quote.ID = QuoteViewModel.ID;
            Quote.MaximumCoverage = QuoteViewModel.MaximumCoverage;
            Quote.MinimumCoverage = QuoteViewModel.MinimumCoverage;
            Quote.Status = "Ready";
            QuoteService.Update(Quote);

            ViewBag.Message = "Quote updated successfully";
            }

            catch (Exception E)
            {
                ViewBag.Message = "Failed to update quote";
            }

            return RedirectToAction("Index");
        }


        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
