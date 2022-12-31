using AirBnb1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirBnb1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        // GET: api/<FlatsController>
        [HttpGet]
        public IEnumerable<Flat> Get()
        {
            return Flat.Read();
        }

        // GET api/<FlatsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("/GetFlatsByCityAndPrice")]
        public List<Flat> GetFlatsByCityAndPrice(string city, double price)
        {
            Flat f=new Flat();
            return f.GetFlatsByCTnPC(city,price);
        }



        // POST api/<FlatsController>
        [HttpPost]
        public int Post([FromBody] Flat flat)
        {
            return flat.Insert();
        }

        // PUT api/<FlatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Flat flat)
        {

        }

        // DELETE api/<FlatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
