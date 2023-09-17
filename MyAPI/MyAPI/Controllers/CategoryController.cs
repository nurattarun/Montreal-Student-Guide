using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories.ToList();

            if (categories == null)
            {
                return NotFound();
            }
            return Ok(new { categories });
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.StudentId == id);
            if (category == null)
            {
                return NotFound(new { Error_Message = $"Category {id} Not Found!" });
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            var findCategory = _context.Categories.FirstOrDefault(x => x.StudentId == category.StudentId);

            if (findCategory != null)
            {
                return BadRequest(new { Error_Message = "This ID already exists!" });
            }

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //return BadRequest(new {Error_Message = ex.Message});
                return BadRequest(new { Error_Message = "Something went wrong when trying to save!" });
            }

            return new CreatedAtRouteResult(
                "GetCategory",
                new { id = category.StudentId },
                category
                );
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, Category category)
        {
            if (id != category.StudentId)
            {
                return BadRequest(new { Error_Message = "Ids do not match!" });
            }

            var categoryToUpdate = _context.Categories.FirstOrDefault(x => x.StudentId == category.StudentId);
            if (categoryToUpdate == null)
            {
                return NotFound(new { Error_Message = $"Category {id} Not Found!" });
            }
            _context.Entry(categoryToUpdate).State = EntityState.Detached;

            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //return BadRequest(new {Error_Message = ex.Message});
                return BadRequest(new { Error_Message = "Something went wrong when trying to save!" });
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.StudentId == id);

            if (category == null)
            {
                return NotFound(new { Error_Message = $"Category {id} Not Found!" });
            }
            var products = _context.Products.Where(x => x.Category == id).ToList();
            try
            {
                _context.Remove(category);

                foreach (var item in products)
                {
                    _context.Remove(item);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { Error_Message = "Something went wrong when trying to save!" });
            }

            return Ok(new { category, deleted_products = products });
        }

    }
}
