using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstAspApi.Models;

namespace FirstAspApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserinfoesController : ControllerBase
    {
        private readonly UserinfoContext _context;

        public UserinfoesController(UserinfoContext context)
        {
            _context = context;
        }

        // GET: api/Userinfoes
        [HttpGet]
        public IEnumerable<Userinfo> GetUserinfo()
        {
            Console.Write("进入GetUserinfo方法");
            return _context.Userinfo;
        }

        // GET: api/Userinfoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserinfo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userinfo = await _context.Userinfo.FindAsync(id);

            if (userinfo == null)
            {
                return NotFound();
            }

            return Ok(userinfo);
        }

        // PUT: api/Userinfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserinfo([FromRoute] long id, [FromBody] Userinfo userinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userinfo.id)
            {
                return BadRequest();
            }

            _context.Entry(userinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserinfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Userinfoes
        [HttpPost]
        public async Task<IActionResult> PostUserinfo([FromBody] Userinfo userinfo)
        {
 
           /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
          
            _context.Userinfo.Add(userinfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserinfo), new { id = userinfo.id }, userinfo);
        }

        // DELETE: api/Userinfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserinfo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userinfo = await _context.Userinfo.FindAsync(id);
            if (userinfo == null)
            {
                return NotFound();
            }

            _context.Userinfo.Remove(userinfo);
            await _context.SaveChangesAsync();

            return Ok(userinfo);
        }

        private bool UserinfoExists(long id)
        {
            return _context.Userinfo.Any(e => e.id == id);
        }
    }
}