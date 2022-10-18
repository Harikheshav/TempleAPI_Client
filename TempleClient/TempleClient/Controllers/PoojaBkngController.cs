using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TempleClient.Models;

namespace TempleClient.Controllers
{
    public class PoojaBkngController : Controller
    {
        string baseurl = "https://localhost:7171/api/PoojaBkng";
        public async Task<List<Pooja>> GetPoojas(string PoojaUrl)
        {
            List<Pooja> Poojainfo = new List<Pooja>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(PoojaUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllPoojas using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Pooja");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PoojaResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Poojainfo = JsonConvert.DeserializeObject<List<Pooja>>(PoojaResponse);
                    
                }
            }
            return Poojainfo;
        }
        public async Task<ActionResult> Index()
        {
          PoojaBkng pooja = new PoojaBkng();
          var Poojainfo = await GetPoojas("https://localhost:7050/");
          ViewBag.Pooid = new SelectList(Poojainfo, "Pid", "Name");
          return View(pooja);
        }

        [HttpPost]
        public async Task<ActionResult> Index(PoojaBkng pooja)
        {
            PoojaBkng pbkng = new PoojaBkng();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pooja), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(baseurl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pbkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            PoojaBkng poojaBkng = new PoojaBkng();
            var Poojainfo = await GetPoojas("https://localhost:7050/");
            ViewBag.Pooid = new SelectList(Poojainfo, "Pid", "Name");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl+"/Detail/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    poojaBkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                }
            }
            return View(poojaBkng);
        }
        private string DateValidation(DateTime? sdt, DateTime? edt)
        {
            if (sdt <= DateTime.Now)
            {
                return "Cannot Assign Events on Past Date and Time";
            }
            if (edt <= sdt)
            {
                return "Event ends before it gets started";
            }
            else
                return null;

        }
        [HttpPost]
        public async Task<ActionResult> Edit(PoojaBkng pbkng)
        {
            PoojaBkng poojaBkng = new PoojaBkng();
            string valid = DateValidation(pbkng.Sdt, pbkng.Edt);
            if (valid != null)
            {
                ModelState.AddModelError("Error", valid);
                return View(pbkng);
            }
            using (var httpClient = new HttpClient())
            {
                int id = pbkng.Bkid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(pbkng), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(baseurl + "/" + pbkng.Bkid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    poojaBkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                }
            }
            return RedirectToAction("Index", "AdminBook");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            PoojaBkng pbkng = new PoojaBkng();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseurl+"/Detail/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pbkng = JsonConvert.DeserializeObject<PoojaBkng>(apiResponse);
                }
            }
            return View(pbkng);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(PoojaBkng pbkng)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(baseurl + "/" + pbkng.Bkid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "AdminBook");
        }
    }
}
