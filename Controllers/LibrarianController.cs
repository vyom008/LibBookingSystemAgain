using LibBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibBooking.Controllers
{
    public class LibrarianController : Controller
    {
        public IActionResult Index()
        {
            // 示例数据
            var librarians = new List<Librarian>
            {
                new Librarian { ID = 1, Name = "Judith Hall", Campus = "Porirua Campus", Email = "judith@library.com", ImageUrl = "/images/judith.jpg", SubjectGuidesUrl = "#" },
                new Librarian { ID = 2, Name = "Sarah Knox", Campus = "Te Auaha Campus", Email = "sarah@library.com", ImageUrl = "/images/sarah.jpg", SubjectGuidesUrl = "#" },
                new Librarian { ID = 3, Name = "Maddie Bowles", Campus = "Petone Campus", Email = "maddie@library.com", ImageUrl = "/images/maddie.jpg", SubjectGuidesUrl = "#" }
            };

            return View(librarians);
        }
    }
}
