using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> listaProdotti = db.Products.ToList<Product>();


                Ok(listaProdotti);
            }
        }

        public IActionResult Details(int id)
        {

        }


    }
}
