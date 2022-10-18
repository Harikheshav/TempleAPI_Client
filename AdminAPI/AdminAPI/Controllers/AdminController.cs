using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminAPI.Models;
namespace AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly TempleContext db;
        public AdminController(TempleContext _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("{Uname}")]
        public async Task<IActionResult> login(string Uname,string Pword)
        {
            var result = await db.Admins.Where(x => x.Uname == Uname && x.Pword == Pword).SingleOrDefaultAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> register([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
