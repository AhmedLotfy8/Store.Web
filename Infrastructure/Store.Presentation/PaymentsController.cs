using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities.Orders;
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
        IServiceManager _serviceManager,
        IConfiguration _configuration) : ControllerBase {

        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntent(string basketId) {

            var result = await _serviceManager.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok(result);

        }

        // stripe listen --forward-to https://localhost:7145/api/payments/webhook
        [Route("webhook")]
        [HttpPost]
        public async Task<IActionResult> Index() {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var endpointSecret = _configuration["StripeOptions:WebhookSecret"];

            var signatureHeader = Request.Headers["Stripe-Signature"];
            var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;


            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded) {
               await _serviceManager.PaymentService.UpdatePaymentIntentForSucceedOrFailed(paymentIntent.Id, OrderStatus.PaymentSuccess);

            }

            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed) {
                await _serviceManager.PaymentService.UpdatePaymentIntentForSucceedOrFailed(paymentIntent.Id, OrderStatus.PaymentFailed);

            }

            else {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }

    }

}

