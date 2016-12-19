using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EFDataAccess.DataModels;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebAppIdenty.Controllers
{
    [EnableCors("CorsPolicy")]
    public class CrudsController : Controller
    {
       
     
            private readonly InventoryContext _context;   //init context object contains data, do not need to add to startup
            private readonly ILogger<HomeController> _logger;

            public CrudsController(InventoryContext context, ILogger<HomeController> logger)
            {
                _context = context;
                _logger = logger;
            }
            public void init()
            {
                _context.Database.Create();
                _context.SaveChanges();
            }
            public HttpClient client()
            {
                string format = "application/json";
                HttpClient yclient = new HttpClient();
                yclient.BaseAddress = new Uri("http://localhost:49961");
                yclient.DefaultRequestHeaders.Accept.Clear();
                yclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(format));

                //...token here
                return yclient;
            }

            public IActionResult Index()
            {
                // init();// cod first , run only once, after db is created, disable it
                return View();
            }
            #region Slots


            public IActionResult SlotCRUD()
            {
                return View();
            }

            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult Create([Microsoft.AspNetCore.Mvc.FromBodyAttribute] Slot jslot)
            {
                if (ModelState.IsValid)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(jslot), Encoding.UTF8, "application/json");

                    var result = client().PostAsync("api/Slots", content).Result;

                    _logger.LogInformation((int)1, "Add slot to database");
                }
                return Json("done");
            }

            [Microsoft.AspNetCore.Mvc.HttpGet]
            public IEnumerable<Slot> AllSlots()
            {
                HttpResponseMessage resp = client().GetAsync("api/Slots").Result;
                resp.EnsureSuccessStatusCode();
                var slots = resp.Content.ReadAsAsync<IEnumerable<Slot>>().Result;
                return slots;
            }

            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult GetSlot(HttpRequestMessage request)
            {
                return Json(AllSlots());
            }

            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult GetnewId()
            {
                int id = 0;
                var slots = AllSlots();
                if (slots != null)
                {
                    if (slots.Count() > 0)
                    {
                        var rtn = from temp in slots select temp;
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

                return Json(id);
            }

            //[Microsoft.AspNetCore.Mvc.HttpGet]
            //public JsonResult GetnewId()
            //{

            //    int id = 0;

            //    if (ModelState.IsValid)
            //    {
            //        if (_context.Slots != null)
            //        {
            //            if (_context.Slots.Count() > 0)
            //            {

            //                IQueryable<Slot> rtn = from temp in _context.Slots select temp;
            //                _logger.LogInformation((int)3, "get new id slot from database");
            //                id = rtn.ToList().Max(x => x.SlotNo) + 1;

            //            }
            //            else
            //            {
            //                id = 1;
            //            }
            //        }
            //        else
            //        {
            //            id = 1;
            //        }

            //    }
            //    return Json(id);
            //}
            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult EachSlot(int id)
            {
                Slot result = new Slot();
                if (ModelState.IsValid)
                {
                    //result = _context.Slots.FirstOrDefault(c => c.SlotNo == id);
                    result = AllSlots().FirstOrDefault(c => c.SlotNo == id);
                    _logger.LogInformation((int)4, "get each slot from database");
                }
                return Json(result);
            }
            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult UpdateSlot([Microsoft.AspNetCore.Mvc.FromBodyAttribute] Slot jslot)
            {

                //var updateSlot = _context.Slots.FirstOrDefault(c => c.SlotNo == jslot.SlotNo);

                //if (ModelState.IsValid)
                //{

                //    updateSlot.SlotNo = jslot.SlotNo;

                //    updateSlot.SlotName = jslot.SlotName;

                //    updateSlot.Description = jslot.Description;

                //    _context.Slots.Attach(updateSlot);

                //    _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Modified;

                //    _logger.LogInformation((int)6, "update slot from database");

                //    _context.SaveChanges();

                //}


                if (ModelState.IsValid)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(jslot), Encoding.UTF8, "application/json");

                    var result = client().PutAsync("api/Slots", content).Result;

                    _logger.LogInformation((int)1, "update slot to database");
                }

                return Json("done");
            }
            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult DeleteSlot(int id)
            {
                if (ModelState.IsValid)
                {
                    var result = client().DeleteAsync("api/Slots?id=" + id).Result;
                }
                return Json("done");
            }
            #endregion

            #region category


            public IActionResult CategoryCRUD()
            {
                return View();
            }
            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult CategoryCreate([Microsoft.AspNetCore.Mvc.FromBodyAttribute] TeaCategory teaCategory)
            {
                //if (ModelState.IsValid)
                //{
                //    _context.TeaCategorys.Add(teaCategory);

                //    _context.SaveChanges();
                //}

                if (ModelState.IsValid)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(teaCategory), Encoding.UTF8, "application/json");

                    var result = client().PostAsync("api/Categorys", content).Result;

                    _logger.LogInformation((int)1, "Add category to database");
                }
                return Json("done");
            }
            [Microsoft.AspNetCore.Mvc.HttpGet]
            public IEnumerable<TeaCategory> AllCategorys()
            {


                HttpResponseMessage resp = client().GetAsync("api/Categorys").Result;

                resp.EnsureSuccessStatusCode();

                var slots = resp.Content.ReadAsAsync<IEnumerable<TeaCategory>>().Result;

                return slots;

            }

            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult GetCategory()
            {

                List<TeaCategory> result = new List<TeaCategory>();

                if (ModelState.IsValid)
                {
                    IEnumerable<TeaCategory> rtn = from temp in AllCategorys() select temp;

                    result = rtn.ToList();
                }

                return Json(result);
            }

            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult GetnewCategoryId()
            {

                int id = 0;

                if (ModelState.IsValid)
                {
                    if (AllCategorys() != null)
                    {
                        if (AllCategorys().Count() > 0)
                        {

                            IEnumerable<TeaCategory> rtn = from temp in AllCategorys() select temp;

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
                return Json(id);
            }

            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult EachCategory(int id)
            {

                TeaCategory result = new TeaCategory();

                if (ModelState.IsValid)
                {

                    result = AllCategorys().FirstOrDefault(c => c.Id == id);

                }

                return Json(result);
            }


            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult UpdateCategory([Microsoft.AspNetCore.Mvc.FromBodyAttribute] TeaCategory jslot)
            {
                if (ModelState.IsValid)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(jslot), Encoding.UTF8, "application/json");

                    var result = client().PutAsync("api/Categorys", content).Result;

                    _logger.LogInformation((int)1, "Add tea items to database");
                }
                return Json("done");
            }

            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult DeleteCategory(int id)
            {

                if (ModelState.IsValid)
                {

                    var updateSlot = _context.TeaCategorys.FirstOrDefault(x => x.Id == id);

                    _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Deleted;

                    _context.SaveChanges();

                }

                return Json("done");
            }
            #endregion

            #region tea item

            //tea item
            public IActionResult TeaItemCRUD()
            {
                return View();
            }


            [Microsoft.AspNetCore.Mvc.HttpGet]
            public IEnumerable<TeaItem> AllTeaItems()
            {
                HttpResponseMessage resp = client().GetAsync("api/TeaItems").Result;
                resp.EnsureSuccessStatusCode();
                IEnumerable<TeaItem> slots = resp.Content.ReadAsAsync<IEnumerable<TeaItem>>().Result;
                return slots;
            }


            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult TeaItemCreate([Microsoft.AspNetCore.Mvc.FromBodyAttribute] TeaItem teaItem)
            {
                if (ModelState.IsValid)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(teaItem), Encoding.UTF8, "application/json");

                    var result = client().PostAsync("api/TeaItems", content).Result;

                    _logger.LogInformation((int)1, "Add tea items to database");
                }
                return Json("done");
            }


            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult GetTeaItem(HttpRequestMessage request)
            {

                List<TeaItem> result = new List<TeaItem>();

                if (ModelState.IsValid)
                {
                    IEnumerable<TeaItem> rtn = from temp in AllTeaItems() select temp;

                    result = rtn.ToList();
                }

                return Json(result);
            }
            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult GetnewTeaItemId()
            {

                int id = 0;

                if (ModelState.IsValid)
                {
                    if (AllTeaItems() != null)
                    {
                        if (AllTeaItems().Count() > 0)
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
                return Json(id);
            }
            [Microsoft.AspNetCore.Mvc.HttpGet]
            public JsonResult EachTeaItem(int id)
            {

                TeaItem result = new TeaItem();

                if (ModelState.IsValid)
                {

                    result = AllTeaItems().FirstOrDefault(c => c.Id == id);

                }

                return Json(result);
            }

            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult UpdateTeaItem([Microsoft.AspNetCore.Mvc.FromBodyAttribute] TeaItem jslot)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jslot), Encoding.UTF8, "application/json");
                var result = client().PutAsync("api/TeaItems", content).Result;
                _logger.LogInformation((int)1, "Add tea items to database");
                return Json("done");
            }

            [Microsoft.AspNetCore.Mvc.HttpPost]
            public IActionResult DeleteTeaItem(int id)
            {
                if (ModelState.IsValid)
                {
                    var updateSlot = _context.TeaItems.FirstOrDefault(x => x.Id == id);
                    _context.Entry(updateSlot).State = System.Data.Entity.EntityState.Deleted;
                    _context.SaveChanges();
                }
                return Json("done");
            }

            #endregion


            public IActionResult ErrorFor500()
            {
                return View();
            }

            public IActionResult ErrorFor404()
            {
                return View();
            }


            public IActionResult About()
            {
                ViewData["Message"] = "Your application description page.";

                return View();
            }

            public IActionResult Contact()
            {
                ViewData["Message"] = "Your contact page.";

                return View();
            }

            public IActionResult Error()
            {
                return View();
            }

        }
    }
