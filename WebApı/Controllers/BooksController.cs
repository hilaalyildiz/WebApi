
using Azure;
using Entitites.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IRepositoryManager _manager;
        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")] int id) {
            try
            {
                var book = _manager.Book.GetOneBookById(id,false);
                if (book is null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();

                _manager.Book.CreateOneBook(book);
                _manager.Save();

                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")] int id, [FromBody] Book book)
        {
            try
            {
                // check book? güncellenecek kitabın bilgisi çekiliyor
                var entity = _manager.Book.GetOneBookById(id, true);

                if (entity is null)
                    return NotFound();

                // check id 
                if (id != book.Id)
                    return BadRequest();

                // yeni değerler mevcut varlıklar üzerinden güncellenir : mapperlar ile yapılacak
                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();
                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(id,false);
                if (entity is null)
                    return NotFound(
                        new
                        {
                            statusCode = 404, message = $"book with id:{id} could not found."
                        });
                _manager.Book.DeleteOneBook(entity);
                _manager.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null)
                    return NotFound();

                bookPatch.ApplyTo(entity);
                _manager.Book.Update(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
