using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CityController : Controller
	{
		IDestinationService _destinationService;

        public CityController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index()
		{
			return View();
		}
		public IActionResult CityList()
		{
			var jsonCity = JsonConvert.SerializeObject(_destinationService.TGetList());
			return Json(jsonCity);
		}

        [HttpPost]
        public IActionResult AddCityDestination(Destination destination)
        {
            destination.Status = true;
            _destinationService.TAdd(destination);
            var values = JsonConvert.SerializeObject(destination);
            return Json(values);
        }

        public IActionResult GetByIdDestination(int DestinationId)
        {
            var values = _destinationService.TGetByID(DestinationId);
            var jsonValues = JsonConvert.SerializeObject(values);
            return Json(jsonValues);
        }
        public IActionResult DeleteDestination(int id)
        {
            var values = _destinationService.TGetByID(id);
            _destinationService.TDelete(values);
            return NoContent();
        }
        [HttpPost]
        public IActionResult UpdateDestination(Destination destination)
        {
            _destinationService.TUpdate(destination);
            var JsonValue = JsonConvert.SerializeObject(destination);
            return Json(JsonValue);
        }
    }
}
