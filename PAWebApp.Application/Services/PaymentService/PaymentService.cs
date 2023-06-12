using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PAWeb.Domain.Entities;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Infrastructure.Repositories.PaymentRepository;

namespace PAWebApp.Application.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddPaymentRequestModel> _validator;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IValidator<AddPaymentRequestModel> validator)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PaymentViewModel> AddAsync(AddPaymentRequestModel paymentModel, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(paymentModel, cancellationToken);
            if (!validationResult.IsValid)
                throw new ModelValidationException(validationResult.Errors);

            //should validate with a 3rd party? that the customer has enough money/points available to make the payment

            var paymentDbModel = _mapper.Map<AddPaymentRequestModel, Payment>(paymentModel);

            var payment = await _paymentRepository.AddAsync(paymentDbModel, cancellationToken);

            return _mapper.Map<Payment, PaymentViewModel>(payment);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);

            if (payment == null)
                throw new EntityNotFoundException($"Entity not found with the id {id}");

            await _paymentRepository.DeleteAsync(payment, cancellationToken);
        }

        public async Task<IEnumerable<PaymentViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllAsync(cancellationToken);

            return payments.Select(_mapper.Map<Payment, PaymentViewModel>).ToList();
        }

        public async Task<PaymentViewModel> GetByIdAsync(int paymentId, CancellationToken cancellationToken)
        {
            var query = _paymentRepository.GetQueriable()
                .Include(c => c.Customer)
                .ThenInclude(c => c.Transactions)
                .Where(c => c.Id == paymentId);

            var payment = await query.FirstOrDefaultAsync(cancellationToken);

            return payment == null
                ? throw new EntityNotFoundException($"Entity not found with the id {paymentId}")
                : _mapper.Map<Payment, PaymentViewModel>(payment);
        }

        public async Task<PaymentViewModel> UpdateAsync(AddPaymentRequestModel paymentModel, CancellationToken cancellationToken)
        {
            var aymentViewModel = _mapper.Map<AddPaymentRequestModel, Payment>(paymentModel);

            var updatedPaymentModel = await _paymentRepository.UpdateAsync(aymentViewModel, cancellationToken);

            return _mapper.Map<Payment, PaymentViewModel>(updatedPaymentModel);
        }
    }
}
