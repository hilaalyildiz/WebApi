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
                throw new Exception($"Book with id:{id} could not found.");

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void UpdateOneBook(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
