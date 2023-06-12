using Microsoft.AspNetCore.Mvc;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Models.Customers;
using PAWebApp.Application.Services.CustomerService;

namespace PAWebApp.API.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Returns a list of customers
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CustomerViewModel>> Get(CancellationToken cancellationToken)
        {
            return await _customerService.GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a customer by its ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task<CustomerViewModel> Get(int id, CancellationToken cancellationToken)
        {
            return await _customerService.GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Creates a customer entity
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<CustomerViewModel> Add([FromBody] AddCustomerRequestModel customerModel, CancellationToken cancellationToken)
        {
            return await _customerService.AddAsync(customerModel, cancellationToken);
        }

        /// <summary>
        /// Deletes a customer by its ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _customerService.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// Updates a ggiven entity
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task<CustomerViewModel> Update([FromBody] AddCustomerRequestModel customerModel, CancellationToken cancellationToken)
        {
            return await _customerService.UpdateAsync(customerModel, cancellationToken);
        }
    }
}
