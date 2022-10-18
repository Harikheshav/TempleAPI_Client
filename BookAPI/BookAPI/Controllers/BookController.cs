using BookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        string user_url = "https://localhost:7285/api/User";
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
                    foreach (var x_user in ABkngInfo)
                    {
                        if (x_user.Userid != null)
                        {
                            using (var httpClient = new HttpClient())
                            {
                                using (var response = await httpClient.GetAsync(user_url + "/Detail/" + x_user.Userid))
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    x_user.User = JsonConvert.DeserializeObject<User>(apiResponse);
                                }
                            }
                        }
                    }
                    return ABkngInfo;
                }
                else
                {
                    return null;
                }

            }
        }
        [HttpGet("PBkng")]
        public async Task<List<PoojaBkng>> GetPBkng()
        {

            string Baseurl = "https://localhost:7171/";
            List<PoojaBkng> PBkngInfo = new List<PoojaBkng>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/PoojaBkng");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PBkngResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    PBkngInfo = JsonConvert.DeserializeObject<List<PoojaBkng>>(PBkngResponse);
                    foreach (var pooja in PBkngInfo)
                    {
                        HttpClient poojaclient;
                        HttpResponseMessage pooja_res;
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync("https://localhost:7050/api/Pooja/Detail/" + pooja.Pooid))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                pooja.Poo = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                            }
                        }
                    }

                    foreach (var x_user in PBkngInfo)
                    {
                        if (x_user.Userid != null)
                        {
                            using (var httpClient = new HttpClient())
                            {
                                using (var response = await httpClient.GetAsync(user_url + "/Detail/" + x_user.Userid))
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    x_user.User = JsonConvert.DeserializeObject<User>(apiResponse);
                                }
                            }
                        }
                    }
                    return PBkngInfo;
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
                    foreach (var x_user in FBkngInfo)
                    {
                        if (x_user.Userid != null)
                        {
                            using (var httpClient = new HttpClient())
                            {
                                using (var response = await httpClient.GetAsync(user_url + "/Detail/" + x_user.Userid))
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    x_user.User = JsonConvert.DeserializeObject<User>(apiResponse);
                                }
                            }
                        }
                    }
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
                    foreach (var x_user in CBkngInfo)
                    {
                        HttpResponseMessage x_res;
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync(user_url + "/Detail/" + x_user.Userid))
                            {
                                if (x_user.Userid != null)
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    x_user.User = JsonConvert.DeserializeObject<User>(apiResponse);
                                }
                            }
                        }
                    }
                    return CBkngInfo;
                }
                else
                {
                    return null;
                }

            }

        }
        [HttpGet]
        [Route("PBkng/{id}")]
        public async Task<PoojaBkng> GetPBkngbyid(int id)
        {
            PoojaBkng pbkng;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7171/api/PoojaBkng/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pbkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                    using (var userClient = new HttpClient())
                    {
                        using (var userres = await userClient.GetAsync(user_url + "/Detail/" + pbkng.Userid))
                        {
                            if (pbkng.Userid != null)
                            {
                                string user_api_res = await userres.Content.ReadAsStringAsync();
                                pbkng.User = JsonConvert.DeserializeObject<User>(user_api_res);
                            }
                        }
                    }
                    using (var poojaClient = new HttpClient())
                    {
                        using (var poojares = await poojaClient.GetAsync("https://localhost:7050/api/Pooja/Detail/" + pbkng.Pooid))
                        {
                            if (pbkng.Pooid != null)
                            {
                                string pooja_api_res = await poojares.Content.ReadAsStringAsync();
                                pbkng.Poo = JsonConvert.DeserializeObject<Pooja>(pooja_api_res);
                            }
                        }
                    }
                }
            }
            return pbkng;
        }
        [HttpGet]
        [Route("FBkng/{id}")]
        public async Task<FnHallBkng> GetFBkngbyid(int id)
        {
            FnHallBkng fbkng;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7004/api/FnHallBkng/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fbkng = JsonConvert.DeserializeObject<FnHallBkng>(apiResponse);
                    using (var userClient = new HttpClient())
                    {
                        using (var userres = await userClient.GetAsync(user_url + "/Detail/" + fbkng.Userid))
                        {
                            if (fbkng.Userid != null)
                            {
                                string user_api_res = await userres.Content.ReadAsStringAsync();
                                fbkng.User = JsonConvert.DeserializeObject<User>(user_api_res);
                            }
                        }
                    }
                }
            }
            return fbkng;
        }
        [HttpGet]
        [Route("CBkng/{id}")]
        public async Task<ConHallBkng> GetCBkngbyid(int id)
        {
            ConHallBkng cbkng;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7282/api/ConHallBkng/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cbkng = JsonConvert.DeserializeObject<ConHallBkng>(apiResponse);
                    using (var userClient = new HttpClient())
                    {
                        using (var userres = await userClient.GetAsync(user_url + "/Detail/" + cbkng.Userid))
                        {
                            if (cbkng.Userid != null)
                            {
                                string user_api_res = await userres.Content.ReadAsStringAsync();
                                cbkng.User = JsonConvert.DeserializeObject<User>(user_api_res);
                            }
                        }
                    }
                }
            }
            return cbkng;
        }
        [HttpGet]
        [Route("ABkng/{id}")]
        public async Task<AnDhanBkng> GetABkngbyid(int id)
        {
            AnDhanBkng abkng;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7201/api/AnDhanBkng/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    abkng = JsonConvert.DeserializeObject<AnDhanBkng>(apiResponse);
                    HttpResponseMessage x_res;
                    using (var userClient = new HttpClient())
                    {
                        using (var userres = await userClient.GetAsync(user_url + "/Detail/" + abkng.Userid))
                        {
                            if (abkng.Userid != null)
                            {
                                string user_api_res = await userres.Content.ReadAsStringAsync();
                                abkng.User = JsonConvert.DeserializeObject<User>(user_api_res);
                            }
                        }
                    }
                }
            }
            return abkng;
        }

    }
}

