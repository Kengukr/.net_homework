using Microsoft.AspNetCore.Mvc;
using task_new.Data.UnitOfWork;
using task_new.Models;

namespace task_new.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] int? year = null)
        {
            if (year.HasValue)
            {
                var filteredBooks = await _unitOfWork.Books.GetBooksByYearAsync(year.Value);
                return Ok(filteredBooks);
            }
            else
            {
                var books = await _unitOfWork.Books.GetAllWithAuthorsAsync();
                return Ok(books);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _unitOfWork.Books.GetBookWithAuthorAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (string.IsNullOrEmpty(book.Title))
            {
                return BadRequest("Назва книги не може бути порожньою");
            }

            if (book.Year <= 0 || book.Year > DateTime.Now.Year)
            {
                return BadRequest("Неправильний рік видання");
            }

            var author = await _unitOfWork.Authors.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest("Вказаний автор не існує");
            }

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(book.Title))
            {
                return BadRequest("Назва книги не може бути порожньою");
            }

            if (book.Year <= 0 || book.Year > DateTime.Now.Year)
            {
                return BadRequest("Неправильний рік видання");
            }

            var author = await _unitOfWork.Authors.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest("Вказаний автор не існує");
            }

            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _unitOfWork.Books.Remove(book);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("custom/route/{id?}")]
        public IActionResult CustomRoute(int id = 0)
        {
            return Ok(new { message = " необов'язкови1 параметр", id });
        }

        [HttpGet]
        [Route("constrained/{id:int:min(1)}")]
        public IActionResult ConstrainedRoute(int id)
        {
            return Ok(new { message = "обмеженням параметру (мінімум 1)", id });
        }

        [HttpGet]
        [Route("minlength/{text:minlength(10)}")]
        public IActionResult MinLengthRoute(string text)
        {
            return Ok(new { message = "не менше 10 символів", text });
        }

        [HttpGet]
        [Route("search/{keyword}/page/{page=1}")]
        public IActionResult MultiSegment(string keyword, int page)
        {
            return Ok(new { message = "з кількома сегментами", keyword, page });
        }
    }
}