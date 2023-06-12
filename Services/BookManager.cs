using Entitites.Exceptions;
using Entitites.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        public BookManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Book CreateOneBook(Book book)
        {
            if(book is null)
                throw new ArgumentNullException(nameof(book));

            _manager.Book.CreateOneBook(book);
            _manager.Save();

            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            // check entity
            var entity = _manager.Book.GetOneBookById(id,trackChanges);

            if (entity is null)
                throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);

            return book;
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if(entity is null)
            {
                throw new BookNotFoundException(id);
            }

            entity.Title = book.Title;
            entity.Price = book.Price;

            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
