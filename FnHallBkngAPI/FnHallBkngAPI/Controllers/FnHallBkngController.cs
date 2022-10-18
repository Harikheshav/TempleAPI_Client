using FnHallBkngAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FnHallBkngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FnHallBkngController : ControllerBase
    {
        private readonly TempleContext db;
        public FnHallBkngController(TempleContext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] FnHallBkng Fb)
        {
            if (Fb == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.FnHallBkngs.Add(Fb);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FnHallBkng newfbkng)
        {
            db.FnHallBkngs.Update(newfbkng);
            await db.SaveChangesAsync();
            return Ok(newfbkng);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            FnHallBkng fbkng = db.FnHallBkngs.Find(id);
            db.FnHallBkngs.Remove(fbkng);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok(await db.FnHallBkngs.ToListAsync());
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FnHallBkng fbkng = await db.FnHallBkngs.FindAsync(id);
            return Ok(fbkng);
        }
    }
}
