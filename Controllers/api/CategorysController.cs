using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EFDataAccess.DataModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebAppIdenty.Controllers.api
{
    [Route("api/Categorys")]
  //  [Authorize(Policy = "adminpolicy")]
    public class CategorysController : Controller
    {
        private readonly InventoryContext _context;
        private ILogger<CategorysController> _logger;
        public CategorysController(InventoryContext context, ILogger<CategorysController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeaCategory slot)
        {
            if (ModelState.IsValid)
            {
                _context.TeaCategorys.Add(slot);
                _logger.LogInformation((int)1, "Add category to database");
                await _context.SaveChangesAsync();
            }
            return Json("done");
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            List<TeaCategory> result = new List<TeaCategory>();
            if (ModelState.IsValid)
            {
                IQueryable<TeaCategory> rtn = from temp in _context.TeaCategorys select temp;
                _logger.LogInformation((int)2, "get category from database");
                result = rtn.ToList();
            }

            return Json(result);
        }

        public int GetnewId()
        {
            int id = 0;
            if (ModelState.IsValid)
            {
                if (_context.TeaCategorys != null)
                {
                    if (_context.TeaCategorys.Count() > 0)
                    {
                        IQueryable<TeaCategory> rtn = from temp in _context.TeaCategorys select temp;
                        _logger.LogInformation((int)3, "get new id slot from database");
                        id = rtn.ToList().Max(x => x.SlotNo) + 1;
                    }
                    else
                    {
                        id = 1;
                    }
                }
                else
                {
                    id = 1;
                }

            }
            return id;
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            TeaCategory result = new TeaCategory();
            if (ModelState.IsValid)
            {
                result = _context.TeaCategorys.FirstOrDefault(c => c.Id == id);
                _logger.LogInformation((int)4, "get each category from database");
            }
            return Json(result);
        }

        //        postman, 
        //post method.header: type-json, no auth,
        //body raw
        //json format

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TeaCategory jslot)
        {

            var updateSlot = _context.TeaCategorys.FirstOrDefault(c => c.Id == jslot.Id);

            if (ModelState.IsValid)
            {

                updateSlot.Id = jslot.Id;

                updateSlot.CategoryName = jslot.CategoryName;

                updateSlot.SlotNo = jslot.SlotNo;

                updateSlot.SubSlot = jslot.SubSlot;

                _context.TeaCategorys.Attach(updateSlot);

                _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Modified;

                _logger.LogInformation((int)6, "update category from database");

                await _context.SaveChangesAsync();

            }

            return Json("done");
        }

        //        postman, 
        //post method.header: type-json, no auth,
        //body raw
        //json format


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            //  in postman http://localhost:49961/api/Slots?id=3

            if (ModelState.IsValid)
            {

                var updateSlot = _context.TeaCategorys.FirstOrDefault(x => x.Id == id);

                _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Deleted;

                _logger.LogInformation((int)2, "delete category from database");

                await _context.SaveChangesAsync();

            }

            return Json("done");
        }

        // POST api/values

    }
}
