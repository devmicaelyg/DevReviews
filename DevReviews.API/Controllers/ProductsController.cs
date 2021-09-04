using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Models.ViewModels;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public ProductsController(IMapper mapper, IProductRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await this.repository.GetAllAsync();

            var productsViewModel = this.mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModel); 
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var product = await this.repository.GetByIdAsync(id);

            if(product == null)
                return NotFound();

            var productDetails = this.mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetails); 
        }


        [HttpPost]
        public async Task<ActionResult> Post(AddProductInputModel model)
        {
            var product = new Product(model.Title, model.Description, model.Price);

            await this.repository.AddAsync(product);
           
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }

        
      [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductInputModel model)
        {
            var product = await this.repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Update(model.Description, model.Price);

            await this.repository.UpdateAsync(product);

            return NoContent();
        }

    }
}