using AgreementManagement.Data.Entities;
using AgreementManagement.Web.Models;
using AutoMapper;

namespace AgreementManagement.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Agreement, AgreementViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupViewModel>().ReverseMap();
        }
    }
}