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

        [HttpGet]
        public IActionResult BuyProduct(int id)
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
                    Purchase newPurchase = new Purchase();
                    newPurchase.ProductId = productFound.Id;
                    newPurchase.Date = DateOnly.FromDateTime(DateTime.Now);
                    PurchaseProductView newView = new PurchaseProductView();
                    newView.Product = productFound;
                    newView.Purchase = newPurchase;
                    return View("BuyProduct", newView);
                }
            }
        }

        [HttpPost]
        public IActionResult BuyProduct(PurchaseProductView formData)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                if (!ModelState.IsValid)
                {
                    return View("BuyProduct",formData);
                }
                else
                {

                    db.Purchases.Add(formData.Purchase);
                    int quantity = formData.Purchase.Quantity;
                    Stock stock = db.Stocks.Where(p => p.ProductId == formData.Product.Id).FirstOrDefault();
                    stock.Quantity = stock.Quantity - quantity;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}