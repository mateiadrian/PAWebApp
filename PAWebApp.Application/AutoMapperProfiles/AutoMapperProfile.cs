using AutoMapper;
using PAWeb.Domain.Entities;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Models.Customers;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Application.Models.Transactions;

namespace PAWebApp.Application.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleViewModel>().ReverseMap();
            CreateMap<Article, AddArticleRequestModel>().ReverseMap();
            CreateMap<ArticleViewModel, AddArticleRequestModel>();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            //    .ForMember(dest => dest.Transactions, opt => opt.Ignore());
            CreateMap<Customer, AddCustomerRequestModel>().ReverseMap();
            CreateMap<Payment, PaymentViewModel>().ReverseMap()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
            CreateMap<AddPaymentRequestModel, Payment>().ReverseMap();
            CreateMap<Transaction, AddTransactionRequestModel>().ReverseMap();
        }
    }
}
