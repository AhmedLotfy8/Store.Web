using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation {

    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase {


        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id) {

            var result = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);

        }

        
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto) {

            var result = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(1));
            return Ok(result);

        }

        
        [HttpDelete]
        public async Task<IActionResult> CreateOrUpdateBasket(string id) {

            var result = await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();

        }

        





    }
}
