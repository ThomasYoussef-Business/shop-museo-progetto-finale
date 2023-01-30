using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers {
    public class AdminController : Controller {
        public IActionResult Index() {
            using ApplicationDbContext db = new ApplicationDbContext();
            List<Product> productList = db.Products.ToList();

            return View(productList);
        }

        #region Create Product
        [HttpGet]
        public IActionResult CreateProduct() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product creationData) {
            if (!ModelState.IsValid) {

                return View(creationData);
            }

            using (ApplicationDbContext db = new ApplicationDbContext()) {
                db.AddProduct(creationData);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Update Product
        [HttpGet]
        public IActionResult UpdateProduct(int id) {
            using ApplicationDbContext db = new ApplicationDbContext();
            Product productFound = db.Products.Find(id);
            if (productFound is not null) {
                return View(productFound);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(int id, Product updateData) {
            using ApplicationDbContext db = new ApplicationDbContext();
            if (!ModelState.IsValid) {
                return View(updateData);
            }

            else {
                Product productFound = db.Products.Find(id);
                if (productFound is null) {
                    return NotFound($"Non è stato trovato nessun prodotto con {id}");
                }

                productFound.Name = updateData.Name;
                productFound.Price = updateData.Price;
                productFound.Description = updateData.Description;
                productFound.PictureUrl = updateData.PictureUrl;
                productFound.Quantity = updateData.Quantity;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id) {
            using ApplicationDbContext db = new ApplicationDbContext();
            Product productFound = db.Products.Find(id);
            if (productFound != null) {
                db.RemoveProduct(productFound);
                return RedirectToAction("Index");
            }
            else {
                return NotFound("Il prodotto da cancellare non è stato trovato");
            }
        }
        #endregion

    }
}
