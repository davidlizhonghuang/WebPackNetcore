using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EFDataAccess.DataModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

//api controller from inventorycontext
//nventorycontext from dbcontext
//dbcontext from code first
//run a test controller to init database, then quit this test
//db now is managed by sql management studio
//use this dbcontext in api controllers as a data layer
//injet this into api controller
//now we develop api/controller....get put etc. resource

//client call this
//client could be mvc controler httpclient
//client could be $http in angular

//api security set up
//host api
// call it from anywhere




namespace WebAppIdenty.Controllers.api
{
    [Route("api/Slots")]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "adminpolicy")]
    public class SlotsController : Controller
    {
        private readonly InventoryContext _context;
        private ILogger<SlotsController> _logger;
        public SlotsController(InventoryContext context, ILogger<SlotsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Slot slot)

        {
            if (ModelState.IsValid)
            {
                _context.Slots.Add(slot);

                _logger.LogInformation((int)1, "Add slot to database");

                await   _context.SaveChangesAsync();

                
            }

             return  Json("done");
        }
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            List<Slot> result = new List<Slot>();
            if (ModelState.IsValid)
            {
                IQueryable<Slot> rtn = from temp in _context.Slots select temp;

                _logger.LogInformation((int)2, "get slot from database");

                result = rtn.ToList();
            }

            return  Json(result);
        }
        public int GetnewId()
        {

            int id = 0;

            if (ModelState.IsValid)
            {
                if (_context.Slots != null)
                {
                    if (_context.Slots.Count() > 0)
                    {
                        IQueryable<Slot> rtn = from temp in _context.Slots select temp;
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
        [HttpGet("{id:int}")]
        public async  Task<JsonResult> Get(int id)
        {

            Slot result = new Slot();

            if (ModelState.IsValid)
            {

                result = _context.Slots.FirstOrDefault(c => c.SlotNo == id);
                _logger.LogInformation((int)4, "get each slot from database");

            }

            return Json(result);
        }

//        postman, 
//post method.header: type-json, no auth,
//body raw
//json format



        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Slot jslot)
        {

            var updateSlot = _context.Slots.FirstOrDefault(c => c.SlotNo == jslot.SlotNo);

            if (ModelState.IsValid)
            {

                updateSlot.SlotNo = jslot.SlotNo;

                updateSlot.SlotName = jslot.SlotName;

                updateSlot.Description = jslot.Description;

                _context.Slots.Attach(updateSlot);

                _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Modified;

                _logger.LogInformation((int)6, "update slot from database");

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

                var updateSlot = _context.Slots.FirstOrDefault(x => x.SlotNo == id);

                _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Deleted;

                _logger.LogInformation((int)2, "delete slot from database");

              await  _context.SaveChangesAsync();

            }

            return Json("done");
        }

        // POST api/values
           
    }
}
