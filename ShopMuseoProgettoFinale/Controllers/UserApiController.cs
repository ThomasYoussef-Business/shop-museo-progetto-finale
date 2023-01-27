using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
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

                Product productFound = db.Products.Where(p=>p.Id == id).FirstOrDefault();
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
