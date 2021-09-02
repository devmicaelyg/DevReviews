using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Models.ViewModels;
using DevReviews.API.Persistence;
using DevReviews.API.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DevReviewsDbContext dbContext;
        private readonly IMapper mapper;

        public ProductsController(DevReviewsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var products = this.dbContext.Products;

            // ISSO QUE O AUTOMAPPER FAZ -> var productsViewModel = products.Select(p => new ProductViewModel(p.Id, p.Title, p.Price));
            var productsViewModel = this.mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModel); 
        }


        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            //SingleOrDefault vai buscar um elemento, caso não tenha, ele retorna null
            var product = this.dbContext.Products.SingleOrDefault(p => p.Id == id);

            if(product == null)
                return NotFound();

            // ISSO QUE O AUTOMAPPER FAZ -> var reviews = product.Reviews.Select(r => new ProductReviewViewModel(r.Id, r.Author, r.Rating, r.RegisteredAt)).ToList();
            // ISSO QUE O AUTOMAPPER FAZ -> var productDetails = new ProductDetailsViewModel(product.Id, product.Title, product.Description, product.Price, product.RegisteredAt, reviews);
            var productDetails = this.mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetails); 
        }


        [HttpPost]
        public ActionResult Post(AddProductInputModel model)
        {
            var product = new Product(model.Title, model.Description, model.Price);

            this.dbContext.Products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }

        
        [HttpPut("{id}")]
        public ActionResult Put(int id, UpdateProductInputModel model)
        {
            if(model.Description.Length > 50)
            {
                return BadRequest();
            }

            var product = this.dbContext.Products.SingleOrDefault(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            product.Update(model.Description, model.Price);

            return NoContent();
        }

    }
}