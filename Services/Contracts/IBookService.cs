using Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        
        Book GetOneBookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id,Book book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
        IEnumerable<Book> GetAllBooks(bool trackChanges);
    }
}
