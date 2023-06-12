using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PAWeb.Domain.Entities;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Customers;
using PAWebApp.Infrastructure.Repositories.CustomerRepository;

namespace PAWebApp.Application.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCustomerRequestModel> _validator;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IValidator<AddCustomerRequestModel> validator)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CustomerViewModel> AddAsync(AddCustomerRequestModel customerModel, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(customerModel, cancellationToken);
            if (!validationResult.IsValid)
                throw new ModelValidationException(validationResult.Errors);

            var customerDbModel = _mapper.Map<AddCustomerRequestModel, Customer>(customerModel);

            var customer = await _customerRepository.AddAsync(customerDbModel, cancellationToken);

            return _mapper.Map<Customer, CustomerViewModel>(customer);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

            if (customer == null)
                throw new EntityNotFoundException($"Entity not found with the id {id}");

            await _customerRepository.DeleteAsync(customer, cancellationToken);
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);

            return customers.Select(_mapper.Map<Customer, CustomerViewModel>).ToList();
        }

        public async Task<CustomerViewModel> GetByIdAsync(int customerId, CancellationToken cancellationToken)
        {
            var query = _customerRepository.GetQueriable()
                .Include(c => c.Transactions).ThenInclude(c => c.TransactionArticles)
                .Where(c => c.Id == customerId)
                .AsQueryable();

            var customer = await query.FirstOrDefaultAsync(cancellationToken);

            return customer == null ?
                throw new EntityNotFoundException($"Entity not found with the id {customerId}") :
                _mapper.Map<CustomerViewModel>(customer);
        }

        public async Task<CustomerViewModel> UpdateAsync(AddCustomerRequestModel customerMdel, CancellationToken cancellationToken)
        {
            var customerDbModel = _mapper.Map<AddCustomerRequestModel, Customer>(customerMdel);

            var updateCustomerModel = await _customerRepository.UpdateAsync(customerDbModel, cancellationToken);

            return _mapper.Map<Customer, CustomerViewModel>(updateCustomerModel);
        }
    }
}
