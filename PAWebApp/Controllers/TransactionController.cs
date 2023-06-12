using Microsoft.AspNetCore.Mvc;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Transactions;
using PAWebApp.Application.Services.TransactionService.cs;

namespace PAWebApp.API.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddTransactionRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<AddTransactionRequestModel> Add([FromBody] AddTransactionRequestModel transactionModel, CancellationToken cancellationToken)
        {
            return await _transactionService.AddAsync(transactionModel, cancellationToken);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AddTransactionRequestModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<AddTransactionRequestModel>> Get(CancellationToken cancellationToken)
        {
            return await _transactionService.GetAllAsync(cancellationToken);
        }
    }
}
