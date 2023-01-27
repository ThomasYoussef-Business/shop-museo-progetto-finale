using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public IActionResult Products()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> listaProdotti = db.Products.ToList<Product>();


                return Ok(listaProdotti);
            }
        }
        //----------------------------
        [Route("details")]
        public IActionResult Details(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Product productFound = db.Products.Find(id); //ti da prodotto che ha trovato o ti da nullo. cerca se c'è un elemento con quell'id. 

                if (productFound == null)
                {
                    return NotFound("l'articolo che hai cercato non esiste");
                } else
                {
                    return Ok(productFound);
                }

            }
        }
        //-----------------------------------PURCHASE
        [Route("purchasesView")]
        [HttpGet]
        public IActionResult purchasesView()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            { 
 List<Purchase> purchasesList = db.Purchases.ToList<Purchase>();
                return Ok(purchasesList);
            }

        }

        
        [Route("purchase")]
        [HttpGet]
        public IActionResult Purchase(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Find(id);
                if (productFound != null)
                {
                    return Ok("questo prodotto non puoi acquistare");
                } else
                {
                    PurchaseProductView modelPurchase = new PurchaseProductView();
                    modelPurchase.Product = productFound;
                    modelPurchase.Quantity = 0;
                    return Ok(modelPurchase);
                }
            }
        }

        [Route("purchase")]
        [HttpPost]

        public IActionResult Purchase(PurchaseProductView formData)
        {
            //qua arriverà quanità che vuole acquistare e nome, 

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return Ok(formData);
                }
                else
                {
                    Purchase newPurchase = new Purchase();
                    newPurchase.Date = DateOnly.FromDateTime(DateTime.Now);
                    newPurchase.Quantity = formData.Quantity;
                    newPurchase.PurchasedProduct = formData.Product;
                    db.Purchases.Add(newPurchase);
                    db.SaveChanges();
                    return Ok(newPurchase);

                }


            }
        }

       

        //----------------------------------------------RESUPPLIES
     

    }
}
