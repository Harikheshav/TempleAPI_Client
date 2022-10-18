using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoojaAPI.Models;

namespace PoojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoojaController : ControllerBase
    {
        private readonly TempleContext db;
        public PoojaController(TempleContext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Pooja P)
        {
            if (P == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.Poojas.Add(P);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] Pooja newpooja)
        {
            db.Poojas.Update(newpooja);
            await db.SaveChangesAsync();
            return Ok(newpooja);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Pooja p = await db.Poojas.FindAsync(id);
            db.Poojas.Remove(p);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok(await db.Poojas.ToListAsync());
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            Pooja p = await db.Poojas.FindAsync(id);
            return Ok(p);
        }
    }
}
