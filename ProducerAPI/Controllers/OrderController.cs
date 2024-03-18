using Microsoft.AspNetCore.Mvc;

namespace ProducerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get([FromQuery] int id)
        {
            var newOrder = new Order
            {
                Id = id,
                Name = "Test",
                Price = 24,


            };
            return Ok(newOrder); ;
        }

        [HttpPost("post")]
        public ActionResult Post([FromBody] Order o)
        {
            var newOrder = new Order
            {
                Id = o.Id,
                Name = o.Name,
                Price = o.Price,
            };
            return Ok(newOrder); ;
        }
    }
}
