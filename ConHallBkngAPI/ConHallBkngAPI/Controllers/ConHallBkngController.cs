using ConHallBkngAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConHallBkngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConHallBkngController : ControllerBase
    {
        private readonly TempleContext db;
        public ConHallBkngController(TempleContext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ConHallBkng Cb)
        {
            if (Cb == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.ConHallBkngs.Add(Cb);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ConHallBkng newcbkng)
        {
            db.ConHallBkngs.Update(newcbkng);
            await db.SaveChangesAsync();
            return Ok(newcbkng);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ConHallBkng cbkng = db.ConHallBkngs.Find(id);
            db.ConHallBkngs.Remove(cbkng);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok(await db.ConHallBkngs.ToListAsync());
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ConHallBkng cbkng = await db.ConHallBkngs.FindAsync(id);
            return Ok(cbkng);
        }
    }
}

