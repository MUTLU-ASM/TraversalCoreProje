using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class ReservationController : Controller
    {
        DestinationManager destinationManager = new(new EfDestinationDal());
        ReservationManager reservationManager = new(new EfReservationDal());

        private readonly UserManager<AppUser> _userManager;

        public ReservationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MyCurrentReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valueList = reservationManager.GetListWithReservationByAccepted(values.Id);
            return View(valueList);
        }
        public async Task<IActionResult> MyOldReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valueList = reservationManager.GetListWithReservationByPrevious(values.Id);
            return View(valueList);
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valueList = reservationManager.GetListWithReservationByWaitApproval(values.Id);
            return View(valueList);
        }
        [HttpGet]
        public IActionResult NewReservation()
        {
            List<SelectListItem> values = (from x in destinationManager.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.City,
                                               Value = x.DestinationId.ToString()
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public IActionResult NewReservation(Reservation r)
        {
            r.AppUserId = 1;
            r.Status = "Onay Bekleniyor";
            reservationManager.TAdd(r);
            return RedirectToAction("MyCurrentReservation");
        }
    }
}
