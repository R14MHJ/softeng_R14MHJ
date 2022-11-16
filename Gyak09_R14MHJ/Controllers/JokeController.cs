using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gyak09_R14MHJ.Controllers
{
    [Route("api/jokes")]
    [ApiController]
    public class JokeController : ControllerBase
    {
        // GET: api/jokes
        [HttpGet]
        public IActionResult Get()
        {
            JokeModels.FunnyDatabaseContext Context = new JokeModels.FunnyDatabaseContext();
            return Ok(Context.Jokes.ToList());
        }

        // GET api/jokes/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            JokeModels.FunnyDatabaseContext Context = new JokeModels.FunnyDatabaseContext();
            var kervicc = (from x in Context.Jokes
                          where x.JokeSk == id
                          select x).FirstOrDefault();
            return Ok(kervicc);
        }

        // POST api/jokes
        [HttpPost]
        public void Post([FromBody] JokeModels.Joke újvicc)
        {
            JokeModels.FunnyDatabaseContext Context = new JokeModels.FunnyDatabaseContext();
            Context.Jokes.Add(újvicc);
            Context.SaveChanges();
        }

        // PUT api/<JokeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/jokes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            JokeModels.FunnyDatabaseContext context = new JokeModels.FunnyDatabaseContext();
            var todel = (from x in context.Jokes
                         where id == x.JokeSk
                         select x).FirstOrDefault();
            context.Jokes.Remove(todel);
            context.SaveChanges();
        }
    }
}
