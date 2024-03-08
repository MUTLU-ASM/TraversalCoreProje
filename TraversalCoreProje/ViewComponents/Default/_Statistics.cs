using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Default
{
    public class _Statistics : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var db = new Context();
            ViewBag.v1 = db.Destinations.Count();
            ViewBag.v2=db.Guides.Count();
            ViewBag.v3 = "285";
            return View();
        }
    }
}
