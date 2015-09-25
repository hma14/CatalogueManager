using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Catalogue.Models;
using Catalogue_AngularJS.Models;

namespace Catalogue_AngularJS.Controllers
{
    public class CategoriesController : ApiController
    {
        private Catalogue_AngularJSContext db = new Catalogue_AngularJSContext();

        // GET: api/Categories
        public IQueryable<CategoryDTO> GETCategory()
        {
            var categories = (from c in db.Categories
                              select new CategoryDTO
                              {
                                  Id = c.Id,
                                  CategoryName = c.CategoryName,
                                  ParentCategoryId = c.ParentCategoryId,
                                  ParentCategoryName = (from cat in db.Categories
                                                        where cat.ParentCategoryId == c.Id
                                                        select c.CategoryName).FirstOrDefault(),

                                  CategoryList = (from ca in db.Categories
                                                  where ca.ParentCategoryId == c.Id
                                                  select new SubCategory
                                                  {
                                                      Id = ca.Id,
                                                      CategoryName = ca.CategoryName,
                                                      ParentCategoryId = ca.ParentCategoryId,
                                                      ProdList = (from p in db.Products
                                                                  where p.CategoryId == ca.Id
                                                                  select p).ToList(),

                                                  }).ToList(),

                              });

            return categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GETCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = (from cat in db.Categories
                                      where cat.ParentCategoryId == category.Id
                                      select category.CategoryName).FirstOrDefault(),



                CategoryList = (from ca in db.Categories
                                where ca.ParentCategoryId == category.Id
                                select new SubCategory
                                {
                                    Id = ca.Id,
                                    CategoryName = ca.CategoryName,
                                    ParentCategoryId = ca.ParentCategoryId,
                                    ProdList = (from p in db.Products
                                                where p.CategoryId == ca.Id
                                                select p).ToList()

                                }).ToList(),
            };

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, categoryDTO);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}