using _2._NTBrokersDataBase.Models;
using _2._NTBrokersDataBase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2._NTBrokersDataBase.Controllers
{
    public class BrokersController : Controller
    {
        private BrokersService _brokersService;
        private ViewDataService _viewDataService;
        private ApartmentsService _apartmentService;

        public BrokersController(BrokersService brokersService, ViewDataService viewDataService, ApartmentsService apartmentService)
        {
            _brokersService = brokersService;
            _viewDataService = viewDataService;
            _apartmentService = apartmentService;
        }
        // GET: BrokersController
        public IActionResult Index()
        {
            return View(_brokersService.GetAllBrokers());
        }

        public IActionResult AddBroker()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBroker(BrokerModel broker)
        {
            _brokersService.AddBroker(broker);
            return RedirectToAction("Index");
        }

        public IActionResult BrokerApartments(int id)
        {
            return View(_viewDataService.ShowBrokerApartments(id));
        }

        public IActionResult AssignApartment(int id)
        {
            return View(_viewDataService.AssignApartmentsToBroker(id));
        }

        [HttpPost]
        public IActionResult AssignApartment(AssignApartmentViewModel assignApartmentViewData)
        {
            _apartmentService.AssignApartment(assignApartmentViewData);
            //return RedirectToAction("BrokerApartments", assignApartmentViewData.BrokerId);
            return View("BrokerApartments", _viewDataService.ShowBrokerApartments(assignApartmentViewData.BrokerId));
        }

        public IActionResult UnAssignApartment(int apartmentId, int brokerId)
        {
            _brokersService.UnAssignApartment(apartmentId);
            return View("BrokerApartments", _viewDataService.ShowBrokerApartments(brokerId));
        }

    }
}
