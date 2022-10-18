using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoojaBkngAPI.Models;

namespace PoojaBkngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoojaBkngController : ControllerBase
    {
        private readonly TempleContext db;
        public PoojaBkngController(TempleContext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PoojaBkng Pb)
        {
            if (Pb == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.PoojaBkngs.Add(Pb);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PoojaBkng newpoojabkng)
        {
            db.PoojaBkngs.Update(newpoojabkng);
            await db.SaveChangesAsync();
            return Ok(newpoojabkng);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            PoojaBkng p = db.PoojaBkngs.Find(id);
            db.PoojaBkngs.Remove(p);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            List<PoojaBkng> pb = await db.PoojaBkngs.ToListAsync();
            return Ok(pb);
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PoojaBkng pbkng = await db.PoojaBkngs.FindAsync(id);
            if (pbkng.Pooid != null)
            {
                pbkng.Poo = await db.Poojas.IgnoreAutoIncludes().SingleOrDefaultAsync(p => p.Pid == pbkng.Pooid);
            }
            return Ok(pbkng);
        }
    }
}


