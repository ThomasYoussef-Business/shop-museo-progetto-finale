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
        public IActionResult Details(int id)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {

                Product productFound = db.Products.Find(id); //ti da prodotto che ha trovato o ti da nullo. cerca se c'è un elemento con quell'id. 

                if (productFound != null)
                {
                    return NotFound("l'articolo che hai cercato non esiste");
                } else
                {
                    return Ok(productFound);
                }

            }
        }
        //-----------------------------------

    }
}
