using AirBnb1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirBnb1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        // GET: api/<VacationsController>
        [HttpGet]
        public IEnumerable<Vacation> Get()
        {
            return Vacation.Read();
        }

        // GET api/<VacationsController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }


        [HttpGet("getByDates/startDate/{SDate}/endDate/{EDate}")]
        public List<Vacation> getByDates(string SDate, string EDate)
        {
            Vacation v=new Vacation();
            return v.GetOredersByDates(SDate, EDate);
        }


        // POST api/<VacationsController>
        [HttpPost]
        public int Post([FromBody] Vacation vacation)
        {
             return vacation.Insert();
        }

        // PUT api/<VacationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
