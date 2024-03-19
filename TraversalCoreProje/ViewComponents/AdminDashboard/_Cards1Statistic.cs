using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.AdminDashboard
{
    public class _Cards1Statistic:ViewComponent
    {
        Context db=new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = db.Destinations.Count();
            ViewBag.v2 = db.Users.Count();
            return View();
        }
    }
}
