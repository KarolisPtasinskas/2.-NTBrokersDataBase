using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApartmentsService _apartmentsService;
        private ViewDataService _viewDataService;

        public ApartmentsController(ApartmentsService apartmentsService, ViewDataService viewDataService)
        {
            _viewDataService = viewDataService;
            _apartmentsService = apartmentsService;
        }

        public IActionResult Index()
        {
            return View(_viewDataService.GetAllApartments());
        }

        public IActionResult Info(int id)
        {
            return View(_apartmentsService.GetOneApartment(id));
        }

        public IActionResult AddApartment()
        {
            return View(_viewDataService.AddApartment());
        }

        [HttpPost]
        public IActionResult AddApartment(AddApartmentViewModel addApartmentViewData)
        {
            _apartmentsService.AddApartment(addApartmentViewData);
            return RedirectToAction("Index");
        }

        public IActionResult FilterApartments(ApartmentsIndexViewModel apartmentsIndexViewData)
        {
            return View("Index", _viewDataService.GetFilteredApartments(apartmentsIndexViewData));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
