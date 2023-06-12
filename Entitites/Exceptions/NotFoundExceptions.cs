using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.Exceptions
{
    public abstract class NotFoundExceptions : Exception
    {
        protected NotFoundExceptions(string message) : base(message) 
        {
            
        }
    }
    public sealed class BookNotFound : NotFoundExceptions
    {
        public BookNotFound(int id) : base($"The book with id : {id} could not found")
        {
                
        }
    }
}
