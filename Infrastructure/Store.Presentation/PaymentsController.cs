using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation {

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(
        IServiceManager _serviceManager) : ControllerBase {

        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId) {

            var result = await _serviceManager.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok(result);

        }

        // stripe listen --forward-to https://localhost:7145/api/payments/webhook
        [Route("webhook")]
        [HttpPost]
        public async Task<IActionResult> Index() {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_1de299b6ddba262e522bf07506efdf1b0d0bb2324464687498a79098c27c4d4d";

            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded) {


            }

            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed) {


            }

            else {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }

    }

}

