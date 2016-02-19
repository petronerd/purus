using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PURUSInsurance.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using Services;

namespace PURUSInsurance.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ICustomer CustomersService;
        private IQuote QuotesService;

        public AccountController(ICustomer CustomersService, IQuote QuotesService)
        {
            this.CustomersService = CustomersService;
            this.QuotesService = QuotesService;
        }

       
        public ActionResult ShowQuote()
        {
            Customer Customer = CustomersService.GetByEmail (User.Identity.Name);
            Quote Quote = QuotesService.GetByUserID (Customer.ID);
            QuotesViewModel QuotesViewModel = new QuotesViewModel();

            QuotesViewModel.Email = Customer.Email;
            QuotesViewModel.FullName = Customer.FullName;
            QuotesViewModel.MaximumCoverage = Quote.MaximumCoverage;
            QuotesViewModel.MinimumCoverage = Quote.MinimumCoverage;
            QuotesViewModel.Status = ((QuoteStatus)Enum.Parse(typeof(QuoteStatus), Quote.Status));
            
            return View(QuotesViewModel);
        }
        
        //
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Customer Customer = new Services.Customer();
            Customer.Email = model.Email;
            Customer.Password = model.Password;

            Feedback Feedback = CustomersService.Authenticate(Customer);

            if (Feedback.Result) 
            {
            
            Customer= CustomersService.GetByEmail(model.Email);

            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = Customer.ID;
            serializeModel.FirstName = Customer.FullName;
            serializeModel.LastName = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     model.Email,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);


            return RedirectToAction("Index", "Home"); // auth succeed 
            }
            else
            {
                return View(model);
            }
        }
    }
}