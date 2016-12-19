using EFDataAccess.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppIdenty.Controllers.api
{
    [Route("api/TeaItems")]
    [Authorize(Policy = "adminpolicy")]
    public class TeaItemsController : Controller
    {

        private readonly InventoryContext _context;
        private ILogger<TeaItemsController> _logger;

        public TeaItemsController(InventoryContext context, ILogger<TeaItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Post([Microsoft.AspNetCore.Mvc.FromBodyAttribute] TeaItem teaItem)
        {
            if (ModelState.IsValid)
            {
                _context.TeaItems.Add(teaItem);

                _context.SaveChanges();
            }

            return Json("done");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<JsonResult> Get()
        {
            List<TeaItem> result = new List<TeaItem>();
            if (ModelState.IsValid)
            {
                IQueryable<TeaItem> rtn = from temp in _context.TeaItems select temp;

                result = rtn.ToList();
            }

            return Json(result);
        }

        public int GetnewTeaItemId()
        {
            int id = 0;
            if (ModelState.IsValid)
            {
                if (_context.TeaItems != null)
                {
                    if (_context.TeaItems.Count() > 0)
                    {
                        IQueryable<TeaItem> rtn = from temp in _context.TeaItems select temp;
                        id = rtn.ToList().Max(x => x.Id) + 1;
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
        }  //only allow one get ...

        [Microsoft.AspNetCore.Mvc.HttpGet("{id:int}")]
        public JsonResult Get(int id)
        {
            TeaItem result = new TeaItem();
            if (ModelState.IsValid)
            {

                result = _context.TeaItems.FirstOrDefault(c => c.Id == id);

            }

            return Json(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        public IActionResult Put([Microsoft.AspNetCore.Mvc.FromBody] TeaItem jslot)
        {
            var updateSlot = _context.TeaItems.FirstOrDefault(c => c.Id == jslot.Id);
            if (ModelState.IsValid)
            {
                updateSlot.Id = jslot.Id;
                updateSlot.ItemName = jslot.ItemName;  //user new input
                updateSlot.CategoryId = jslot.CategoryId;
                updateSlot.ItemPrice = jslot.ItemPrice;
                updateSlot.ItemUnit = jslot.ItemUnit;
                updateSlot.UnitNumber = jslot.UnitNumber;
                updateSlot.MeasureUnit = jslot.MeasureUnit;
                updateSlot.Sizes = jslot.Sizes;
                updateSlot.ItemType = jslot.ItemType;
                updateSlot.ProductDate = jslot.ProductDate;
                updateSlot.StorageDate = jslot.StorageDate;
                updateSlot.ItemTaken = jslot.ItemTaken;
                updateSlot.Imagepath = jslot.Imagepath;

                _context.TeaItems.Attach(updateSlot);

                _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Modified;

                _context.SaveChanges();
            }
            return Json("done");
        }
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public IActionResult Delete(int id)
        {

            if (ModelState.IsValid)
            {

                var updateSlot = _context.TeaItems.FirstOrDefault(x => x.Id == id);

                _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Deleted;

                _context.SaveChanges();

            }

            return Json("done");
        }

    }
}
