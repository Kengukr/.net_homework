using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using task_new.Data.UnitOfWork;
using task_new.Models;

namespace task_new.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetAuthorWithBooksAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            if (string.IsNullOrEmpty(author.Name))
            {
                return BadRequest("Ім'я автора не може бути порожнім");
            }

            if (author.BirthYear <= 0 || author.BirthYear > DateTime.Now.Year)
            {
                return BadRequest("Неправильний рік народження");
            }

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(author.Name))
            {
                return BadRequest("Ім'я автора не може бути порожнім");
            }

            if (author.BirthYear <= 0 || author.BirthYear > DateTime.Now.Year)
            {
                return BadRequest("Неправильний рік народження");
            }

            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            _unitOfWork.Authors.Update(author);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}