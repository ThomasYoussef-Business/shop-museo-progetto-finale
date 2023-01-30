using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;
using System.Diagnostics;

namespace ShopMuseoProgettoFinale.Controllers {
    public class UserController : Controller {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Buy(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Where(p => p.Id == id).FirstOrDefault();

                if (productFound is null)
                {
                    return NotFound("il prodotto non è stato trovato");
                }
                else
                {
                    return View("Buy", productFound);
                }
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}