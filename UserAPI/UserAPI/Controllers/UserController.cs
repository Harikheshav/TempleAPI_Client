using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TempleContext db;
        public UserController(TempleContext _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("{Uname}")]
        public async Task<IActionResult> login(string Uname, string Pword)
        {
            var result = await db.Users.Where(x => x.Uname == Uname && x.Pword == Pword).SingleOrDefaultAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            User user = await db.Users.FindAsync(id);
            return Ok(user);
        }
    }
}
