using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gyak09_R14MHJ.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoatController : ControllerBase
    {
        [HttpGet]
        [Route("hajo/kerdesek")]
        public IActionResult mind1()
        {
            Models.HajosContext hajoscontext = new Models.HajosContext();
            var lisa = from x in hajoscontext.Questions select x;
            return Ok(lisa);
        }
        [HttpGet]
        [Route("hajo/kerdes/{id}")]
        public IActionResult mind2(int id)
        {
            Models.HajosContext hajoscontext = new Models.HajosContext();
            var list = (from x in hajoscontext.Questions
                        where x.QuestionId == id
                        select x).FirstOrDefault();
            //var lisa2 = hajoscontext.Questions.Where(x => x.QuestionId == id)
            return Ok(list);
        }
        [HttpGet]
        [Route("hajo/kerdesszam")]
        public int mind3() 
        {
            Models.HajosContext context = new Models.HajosContext();
            int kérdésekSzáma = context.Questions.Count();

            return kérdésekSzáma;
        }
    }
}
