using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models.ViewModels;

namespace DevReviews.API.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<ProductReview, ProductReviewViewModel>();
            
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductDetailsViewModel>();
        }
    }
}