using _2._NTBrokersDataBase.Models;
//using _2._NTBrokersDataBase.Models.ViewModels;
using _2._NTBrokersDataBase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Controllers
{
    public class CompaniesController : Controller
    {
        private CompaniesService _companiesService;
        private ViewDataService _viewDataService;

        public CompaniesController(CompaniesService companiesService, ViewDataService viewDataService)
        {
            _companiesService = companiesService;
            _viewDataService = viewDataService;
        }
        // GET: CompaniesController
        public ActionResult Index()
        {
            return View(_companiesService.GetAllCompanies());
        }

        public IActionResult AddCompany()
        {
            return View(_viewDataService.AddCompany());
        }

        [HttpPost]
        public IActionResult AddCompany(AddCompanyViewModel addCompanyViewData)
        {
            _companiesService.AddCompany(addCompanyViewData);

            return RedirectToAction("Index");
        }

        public IActionResult EditCompany(int id)
        {
            return View(_viewDataService.EditCompany(id));
        }

        [HttpPost]
        public IActionResult EditCompany(EditCompanyViewModel editCompanyViewModel)
        {
            _companiesService.EditCompany(editCompanyViewModel);
            return RedirectToAction("Index");
        }

        public IActionResult BrokersInCompany(int id)
        {
            return View(_companiesService.GetAllBrokersInCompany(id));
        }
    }
}
