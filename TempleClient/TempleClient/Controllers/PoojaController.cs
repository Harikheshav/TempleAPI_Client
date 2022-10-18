using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TempleClient.Models;
namespace TempleClient.Controllers
{
    public class PoojaController : Controller
    {
            // GET: Pooja

        string baseurl = "https://localhost:7050/api/Pooja";
        public async Task<ActionResult> Create()
        {
            Pooja pooja = new Pooja();
            return View(pooja);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pooja pooja)
        {
            Pooja poo = new Pooja();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pooja), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(baseurl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    poo = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                }
            }
            return RedirectToAction("Details");  
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            Pooja pooja = new Pooja();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pooja = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                }
            }
            return View(pooja);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Pooja newpooja)
        {
            Pooja pooja = new Pooja();

            using (var httpClient = new HttpClient())
            {
                int id = newpooja.Pid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(newpooja), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(baseurl, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    pooja = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                }
            }
            return RedirectToAction("Details");
        }      
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            Pooja pooja = new Pooja();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pooja = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                }
            }
            return View(pooja);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Pooja pooja)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(baseurl + "/" + pooja.Pid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pooja = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                }
            }

            return RedirectToAction("Details");
        }
        public async Task<ActionResult> Details()
        {
            List<Pooja> PoojaInfo = new List<Pooja>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllPoojas using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(baseurl);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PoojaResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Pooja list  
                    PoojaInfo = JsonConvert.DeserializeObject<List<Pooja>>(PoojaResponse);

                }
                //returning the Pooja list to view  
                return View(PoojaInfo);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            Pooja pooja = new Pooja();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl + "/Detail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pooja = JsonConvert.DeserializeObject<Pooja>(apiResponse);
                }
            }
            return View(pooja);
        }

    }


}

