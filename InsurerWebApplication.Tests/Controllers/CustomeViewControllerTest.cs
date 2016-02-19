using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsurerWebApplication;
using PURUSInsurance.Models;
using InsurerWebApplication.Controllers;
using Services;

namespace InsurerWebApplication.Tests.Controllers
{
    [TestClass]
    public class CustomeViewControllerTest
    {
        [TestMethod]
        public void UpdateQuote()
        {
            CustomersViewModelsController controller = new CustomersViewModelsController(new svcCustomer(), new svcQuote());

            QuotesViewModel QuoteViewModel = new QuotesViewModel();

            QuoteViewModel.ID =5;
            QuoteViewModel.FullName ="Asghar Taraghi";
            QuoteViewModel.MaximumCoverage =23;
            QuoteViewModel.MinimumCoverage =12;
            QuoteViewModel.Status = QuoteStatus.Ready;
            QuoteViewModel.Email = "sampleEmail@sample.com";

            ActionResult result = controller.UpdateQuote(QuoteViewModel);

            Assert.IsTrue(controller.ViewBag . Message=="Failed to update quote");

        }
    }
}
