using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.ToList();

            if (products == null)
            {
                return NotFound();
            }
            return Ok(new { products });

        }
        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
          
            var product = _context.Products.FirstOrDefault(x => x.StudentId == id);
            if (product == null)
            {
                
                return NotFound(new { Error_Message = $"Product {id} Not Found!" });
            }
            return Ok(product);
        }

        [HttpGet("{CatId}")]
        public ActionResult<IEnumerable<Product>> GetByCategory(string CatId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.StudentId == CatId);

            if(category == null)
            {
                return NotFound(new { Error_Message = $"Category  {CatId} not found" });
            }
            var products = _context.Products.Where(x => x.Category == CatId).ToList();
            //if(products == null)
            //{
                
            //}

            return Ok(new { products });
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            var findProduct = _context.Products.FirstOrDefault(x =>x.StudentId == product.StudentId);
            if (findProduct != null)
            {
               return BadRequest(new { Error_Message ="This ID exists already!"});
            }
           

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message ="Something went wrong while saving!" });
            }

            //to know if the value that im returning is from the database
            var newProduct = _context.Products.FirstOrDefault(x => x.StudentId == product.StudentId);
            //if(newProduct != null)
            //{

            //}
            return new CreatedAtRouteResult("GetProduct",
                new {id = product.StudentId},
                
                product
                );
           // return null;
        }

        [HttpDelete("{id}")] //new endpoint
        //delete works the same way as get one product as they receive same parameters, method is diff
        public ActionResult Delete(string id)
        {
            var product = _context.Products.FirstOrDefault(x => x.StudentId == id);

            if(product == null)
            {
                return NotFound(new { Error_Message = $"Product {id} Not Found! "});
            }
            try
            {
                _context.Remove(product);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = "Something went wrong while saving!" });
            }
            return Ok(product);
        }

        [HttpPut("{id}")]

        public ActionResult Put(string id, Product product)
        {
            if(id != product.StudentId)
            {
                return BadRequest(new { Message = "IDs do not match" });
            }

            var productToUpdate = _context.Products.FirstOrDefault(x =>x.StudentId == id);
            if (productToUpdate == null)
            {
                return NotFound(new { Error_Message = $"Product {id} Not Found! " });

            }
             _context.Entry(productToUpdate).State = EntityState.Detached; //this will modify only the field which were changed
            try
                {
                _context.Products.Update(product);
                _context.SaveChanges();
            }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = "Something went wrong while saving!" });
                }
            

           
            return Ok(product);

        }
    }
    }
