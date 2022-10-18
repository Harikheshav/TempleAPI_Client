using AnDhanBkngAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnDhanBkngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnDhanBkngController : ControllerBase
    {
        private readonly TempleContext db;
        public AnDhanBkngController(TempleContext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AnDhanBkng Ab)
        {
            if (Ab == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.AnDhanBkngs.Add(Ab);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AnDhanBkng newabkng)
        {
            db.AnDhanBkngs.Update(newabkng);
            await db.SaveChangesAsync();
            return Ok(newabkng);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            AnDhanBkng abkng = db.AnDhanBkngs.Find(id);
            if (ModelState.IsValid)
            {
                db.AnDhanBkngs.Remove(abkng);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok(await db.AnDhanBkngs.ToListAsync());
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            AnDhanBkng abkng = await db.AnDhanBkngs.FindAsync(id);
            return Ok(abkng);
        }
    }
}

