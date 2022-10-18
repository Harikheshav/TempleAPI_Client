using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlotBookingAPI.Models;
using System.Net.Http.Headers;

namespace SlotBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotBookingController : ControllerBase
    {
        private readonly TempleContext db;
        public SlotBookingController(TempleContext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SlotBkng slot)
        {
            if (slot == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                if (slot.SlotName == "FunctionHall")
                {
                    FnHallBkng fn = new FnHallBkng(slot);
                    db.FnHallBkngs.Add(fn);
                    db.SaveChanges();
                }
                else if (slot.SlotName == "ConcertHall")
                {
                    ConHallBkng con = new ConHallBkng(slot);
                    db.ConHallBkngs.Add(con);
                    db.SaveChanges();
                }
                else if (slot.SlotName == "Annadhanam")
                {
                    AnDhanBkng anad = new AnDhanBkng(slot);
                    db.AnDhanBkngs.Add(anad);
                    db.SaveChanges();
                }
                else
                {
                    return new EmptyResult();
                }
            }
            return Ok();
        }
            
        [HttpGet("ABkng")]
        public async Task<List<AnDhanBkng>> GetABkng()
        {

            string Baseurl = "https://localhost:7201/";
            List<AnDhanBkng> ABkngInfo = new List<AnDhanBkng>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/AnDhanBkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ABkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    ABkngInfo = JsonConvert.DeserializeObject<List<AnDhanBkng>>(ABkngResponse);
                    return ABkngInfo;
                }
                else
                {
                    return null;
                }

            }
        }
        [HttpGet("FnHallBkng")]
        public async Task<List<FnHallBkng>> GetFBkng()
        {

            string Baseurl = "https://localhost:7004/";
            List<FnHallBkng> FBkngInfo = new List<FnHallBkng>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/FnHallBkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var FBkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    FBkngInfo = JsonConvert.DeserializeObject<List<FnHallBkng>>(FBkngResponse);
                    return FBkngInfo;
                }
                else
                {
                    return null;
                }

            }

        }
        [HttpGet("ConHallBkng")]
        public async Task<List<ConHallBkng>> GetCBkng()
        {

            string Baseurl = "https://localhost:7282/";
            List<ConHallBkng> CBkngInfo = new List<ConHallBkng>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/ConHallBkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CBkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    CBkngInfo = JsonConvert.DeserializeObject<List<ConHallBkng>>(CBkngResponse);
                    return CBkngInfo;
                }
                else
                {
                    return null;
                }

            }
        }
    }
}