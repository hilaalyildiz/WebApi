using Entitites.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(ServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {

            var book = _manager.BookService.GetOneBookById(id, false);
            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {

            if (book is null)
                return BadRequest();

            _manager.BookService.CreateOneBook(book);

            return StatusCode(201, book);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {

            if (book is null)
                return BadRequest();

            _manager.BookService.UpdateOneBook(id, book);

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {

            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {

            var entity = _manager.BookService.GetOneBookById(id, true);
            if (entity is null)
                return NotFound();

            bookPatch.ApplyTo(entity);
            _manager.BookService.UpdateOneBook(id, entity);

            return NoContent();
        }
    }
}
