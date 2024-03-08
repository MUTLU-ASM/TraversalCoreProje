using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    public class CommentController : Controller
    {
        CommentManager commentManager = new(new EfCommentDal());
        [HttpGet]
        public PartialViewResult AddComment(int id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(Comment c)
        {
            c.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            c.State = true;
            commentManager.TAdd(c);
            return RedirectToAction("Index","Destination");
        }
    }
}
