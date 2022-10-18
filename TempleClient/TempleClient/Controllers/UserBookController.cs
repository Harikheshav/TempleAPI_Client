using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class UserBookController : Controller
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
                    userBook.PoojaBkngs = userBook.PoojaBkngs.Where(x => x.Userid == null).ToList();
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
                    userBook.ConHallBkngs = userBook.ConHallBkngs.Where(x => x.Userid == null).ToList();
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
                    userBook.AnDhanBkngs = userBook.AnDhanBkngs.Where(x => x.Userid == null).ToList();
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
                    userBook.FnHallBkngs = userBook.FnHallBkngs.Where(x => x.Userid == null).ToList();

                }
            }
            return View(userBook);
        }

        public async Task<IActionResult> AddUser(int ch, int id)
        //Add's a user to a unassigned booking
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            User user;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7285/api/User/Detail/" + userid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
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
                    pbkng.Userid = userid;
                    pbkng.User = user;
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(pbkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7171/api/PoojaBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            pbkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("UserBooking");
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
                    abkng.Userid = userid;
                    abkng.User = user;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = abkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(abkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7201/api/AnDhanBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            abkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("UserBooking");
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
                    conBkng.Userid = userid;
                    conBkng.User = user;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = conBkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(conBkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7282/api/ConHallBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            conBkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("UserBooking");
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
                    fnBkng.Userid = userid;
                    fnBkng.User = user;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = fnBkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(fnBkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7004/api/FnHallBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            fnBkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("UserBooking");
            }
            else
                return new EmptyResult();
        }
        public async Task<IActionResult> CancelBooking(int ch, int id)
        //Add's a user to a unassigned booking
        {
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
                    pbkng.Userid = null;
                    pbkng.User = null;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = pbkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(pbkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7171/api/PoojaBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            pbkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("Index");
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
                    abkng.Userid = null;
                    abkng.User = null;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = abkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(abkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7201/api/AnDhanBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            abkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("Index");
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
                    conBkng.Userid = null;
                    conBkng.User = null;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = conBkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(conBkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7282/api/ConHallBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            conBkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("Index");
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
                    fnBkng.Userid = null;
                    fnBkng.User = null;
                    using (var httpClient = new HttpClient())
                    {
                        int xid = fnBkng.Bkid;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(fnBkng), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7004/api/FnHallBkng/" + id, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            fnBkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return new EmptyResult();
        }

        public async Task<IActionResult> UserBooking()
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
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
                    userBook.PoojaBkngs = userBook.PoojaBkngs.Where(x => x.Userid == userid).ToList();
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
                    userBook.ConHallBkngs = userBook.ConHallBkngs.Where(x => x.Userid == userid).ToList();
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
                    userBook.AnDhanBkngs = userBook.AnDhanBkngs.Where(x => x.Userid == userid).ToList();
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
                    userBook.FnHallBkngs = userBook.FnHallBkngs.Where(x => x.Userid == userid).ToList();

                }
            }
            return View(userBook);
        }
        public async Task<IActionResult> GenerateChallan(int ch, int id)
        {
            ViewBag.User = HttpContext.Session.GetString("UserName");
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
                }
                return View();
            }
            else
                return new EmptyResult();
        }
    }


}

