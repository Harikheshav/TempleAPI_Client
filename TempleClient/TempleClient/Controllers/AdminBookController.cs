using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TempleClient.Models;
namespace TempleClient.Controllers
{
    public class AdminBookController : Controller
    {
        UserBook userBook = new UserBook();
        string baseurl = "https://localhost:7069/";
        public async Task<IActionResult> Index()
        {

            HttpResponseMessage Res;
            HttpClient client;
            using (client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllPoojaBookings using HttpClient  
                Res = await client.GetAsync("api/Book/PBkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PBkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    userBook.PoojaBkngs = JsonConvert.DeserializeObject<List<PoojaBkng>>(PBkngResponse);
                }
            }
            using (client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllConHallBkngs using HttpClient  
                Res = await client.GetAsync("api/Book/ConHallBkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CBkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the ConHallBkng list  
                    userBook.ConHallBkngs = JsonConvert.DeserializeObject<List<ConHallBkng>>(CBkngResponse);
                }
            }
            using (client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllABkngs using HttpClient  
                Res = await client.GetAsync("api/Book/ABkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ABkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the ABkng list  
                    userBook.AnDhanBkngs = JsonConvert.DeserializeObject<List<AnDhanBkng>>(ABkngResponse);
                }
            }
            using (client = new HttpClient())
            {
                            //Passing service base url  
                            client.BaseAddress = new Uri(baseurl);

                            client.DefaultRequestHeaders.Clear();
                            //Define request data format  
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                            Res = await client.GetAsync("api/Book/FnHallBkng");

                            //Checking the response is successful or not which is sent using HttpClient  
                            if (Res.IsSuccessStatusCode)
                            {
                                //Storing the response details recieved from web api   
                                var FBkngResponse = Res.Content.ReadAsStringAsync().Result;

                                //Deserializing the response recieved from web api and storing into the Employee list  
                                userBook.FnHallBkngs = JsonConvert.DeserializeObject<List<FnHallBkng>>(FBkngResponse);
                                
                            }
            }
           return View(userBook);
        }
        public async Task<IActionResult> GenerateBill(int ch, int id)
        {
            ViewBag.Admin = HttpContext.Session.GetString("AdminName");
            if (ch == 1)
            {
                PoojaBkng pbkng;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7069/api/Book/PBkng/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pbkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                    }
                }
                if (pbkng != null)
                {
                    TempData["Booking_Type"] = "Pooja";
                    ViewBag.Details = pbkng.Poo.Name;
                    ViewBag.StartDate = pbkng.Sdt;
                    ViewBag.EndDate = pbkng.Edt;
                    ViewBag.Cost = pbkng.Poo.Cost;
                    ViewBag.User = pbkng.User.Uname;
                }
                return View();
            }
            else if (ch == 2)
            {
                AnDhanBkng abkng;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7069/api/Book/ABkng/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        abkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                    }
                }
                if (abkng != null)
                {
                    TempData["Booking_Type"] = "Annadhanam";
                    ViewBag.Details = abkng.Det;
                    ViewBag.StartDate = abkng.Sdt;
                    ViewBag.EndDate = abkng.Edt;
                    ViewBag.Cost = abkng.Cost;
                    ViewBag.User = abkng.User.Uname;
                }
                return View();
            }
            else if (ch == 3)
            {
                ConHallBkng conBkng;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7069/api/Book/CBkng/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        conBkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                    }
                }
                if (conBkng != null)
                {
                    TempData["Booking_Type"] = "Concert Hall";
                    ViewBag.Details = conBkng.Det;
                    ViewBag.StartDate = conBkng.Sdt;
                    ViewBag.EndDate = conBkng.Edt;
                    ViewBag.Cost = conBkng.Cost;
                    ViewBag.User = conBkng.User.Uname;
                }
                return View();
            }
            else if (ch == 4)
            {
                FnHallBkng fnBkng;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7069/api/Book/FBkng/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        fnBkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                    }
                }
                if (fnBkng != null)
                {
                    TempData["Booking_Type"] = "Function Hall";
                    ViewBag.Details = fnBkng.Det;
                    ViewBag.StartDate = fnBkng.Sdt;
                    ViewBag.EndDate = fnBkng.Edt;
                    ViewBag.Cost = fnBkng.Cost;
                    ViewBag.User = fnBkng.User.Uname;
                }
                return View();
            }
            else
                return new EmptyResult();
        }
    }
}

