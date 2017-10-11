using System;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace HeroesApi.Controllers
{
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
	    private readonly IClusterClient _client;

	    public HeroesController(IClusterClient client)
	    {
		    _client = client;
	    }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
	        var heroGrain = _client.GetGrain<IHeroGrain>("Znrolax");
	        await heroGrain.Create(new Hero
	        {
		        Id = Guid.NewGuid().ToString(),
				Name = "Znorlax"
	        });

	        return Ok(await heroGrain.Get());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
