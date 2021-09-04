using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Models.ViewModels;
using DevReviews.API.Persistence;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/productreviews")]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public ProductReviewsController(IMapper mapper,  IProductRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        
        // GET api/products/1/productreviews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int productId, int id){
            
            var productReview = this.repository.GetReviewByIdAsync(id);
            
            if(productReview == null){
                return NotFound();
            }

            var productDetails = this.mapper.Map<ProductReviewDetailsViewModel>(productReview);
            
            
            return Ok(productDetails);
        }

        // POST api/products/1/productreviews
        [HttpPost]
        public async Task<IActionResult> Post(int productId, AddProductReviewInputModel model){

            var productReview = new ProductReview(model.Author, model.Rating, model.Comments, productId);

            await this.repository.AddReviewAsync(productReview);

            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId = productId }, model);
        }
    }
}