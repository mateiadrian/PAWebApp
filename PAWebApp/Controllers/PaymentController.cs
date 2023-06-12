using Microsoft.AspNetCore.Mvc;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Application.Services.PaymentService;

namespace PAWebApp.API.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Returns a list of payments
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentViewModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<PaymentViewModel>> Get(CancellationToken cancellationToken)
        {
            return await _paymentService.GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a payment by its ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task<PaymentViewModel> Get(int id, CancellationToken cancellationToken)
        {
            return await _paymentService.GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Creates a payment entity
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<PaymentViewModel> Add([FromBody] AddPaymentRequestModel paymentModel, CancellationToken cancellationToken)
        {
            return await _paymentService.AddAsync(paymentModel, cancellationToken);
        }

        /// <summary>
        /// Deletes a payment by its ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _paymentService.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// Updates a given entity
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PaymentViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task<PaymentViewModel> Update([FromBody] AddPaymentRequestModel paymentModel, CancellationToken cancellationToken)
        {
            return await _paymentService.UpdateAsync(paymentModel, cancellationToken);
        }
    }
}
