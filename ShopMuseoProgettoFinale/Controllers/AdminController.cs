using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> productList = db.Products.ToList();
                return View("Index", productList);
            }
                
        }

//---------------------------------------------------------------------
        [HttpGet]
        public IActionResult CreateProduct()
        {

            return View();
        }
//---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateProduct(Product formData)
        {
            if(!ModelState.IsValid)
            {

                return View("Create", formData);
            }

            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Products.Add(formData);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
//---------------------------------------------------------------------
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Find(id);

                if(productFound != null)
                {
                    return View("UpdateProduct", productFound);

                } else
                {
                    return NotFound("il prodotto non è stato trovato, non esiste");
                }          
            }
        }
//---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(int id, Product formData)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {

                    return View("UpdateProduct", formData);
                }
                else
                {
                    Product productFound = db.Products.Find(id);
                    productFound.Name = formData.Name;
                    productFound.Price = formData.Price;
                    productFound.Description = formData.Description;
                    productFound.PictureUrl = formData.PictureUrl;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }
//---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id) {
        
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Find(id);
                if(productFound != null)
                {
                    db.Products.Remove(productFound);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }else
                {
                    return NotFound("il prodotto da cancellare non è stato trovato");
                }
            }
        }
//---------------------------------------------------------------
       //Metodi per Purchases
       public IActionResult PurchasesView()
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Purchase> purchaseList = db.Purchases.ToList();
                return View("PurchasesView", purchaseList);

            }
        }
//---------------------------------------------------------------------
        [HttpGet]
        public IActionResult PurchaseCreate(int id)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                    Product productFound = db.Products.Find(id);
                    if (productFound != null)
                    {
                        return NotFound("questo prodotto non puoi acquistare");
                    }
                    else
                    {
                        PurchaseProductView modelPurchase = new PurchaseProductView();
                        modelPurchase.Product = productFound;
                        modelPurchase.Quantity = 0;
                        return View("PurchaseCreate",modelPurchase);
                    }  
            }

        }

        [HttpPost]
        public IActionResult PurchaseCreate(PurchaseProductView formData)
        {
            //qua arriverà quanità che vuole acquistare e nome, 

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("PurchaseCreate",formData);
                }
                else
                {
                    Purchase newPurchase = new Purchase();
                    newPurchase.Date = DateOnly.FromDateTime(DateTime.Now);
                    newPurchase.Quantity = formData.Quantity;
                    newPurchase.PurchasedProduct = formData.Product;
                    db.Purchases.Add(newPurchase);
                    db.SaveChanges();

                    //ADESSO dimnuire la quantità nel magazzino del prodotto

                    int PurchasedProductId = newPurchase.PurchasedProduct.Id;
                    Stock aggiornaStock = db.Stocks.Find(PurchasedProductId);
                    aggiornaStock.Quantity = aggiornaStock.Quantity -formData.Quantity;

                    return RedirectToAction("PurchasesView");
                }
            }

        }
        //--------------------------RESUPPLIES--------------








    }
}
