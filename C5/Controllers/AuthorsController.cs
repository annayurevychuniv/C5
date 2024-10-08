using C5.Data;
using C5.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace C5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is null)
                return NotFound("Author not found");
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<List<Author>>> AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok(await GetAllAuthors());
        }

        [HttpPut]
        public async Task<ActionResult<List<Author>>> UpdateAuthor(Author updatedAuthor)
        {
            var dbAuthor = await _context.Authors.FindAsync(updatedAuthor.Id);
            if (dbAuthor is null)
                return NotFound("Author not found");

            dbAuthor.Name = updatedAuthor.Name;
            dbAuthor.Info = updatedAuthor.Info;
            
            await _context.SaveChangesAsync();
            return Ok(await _context.Authors.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Author>>> DeleteAuthor(int id)
        {
            var dbAuthor = await _context.Authors.FindAsync(id);
            if (dbAuthor is null)
                return NotFound("Author not found");

            _context.Authors.Remove(dbAuthor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }
    }
}
