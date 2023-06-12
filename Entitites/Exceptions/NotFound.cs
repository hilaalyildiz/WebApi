using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.Exceptions
{
    public abstract class NotFound : Exception
    {
        protected NotFound(string message) : base(message) 
        {
            
        }
    }
    public sealed class BookNotFound : NotFound
    {
        public BookNotFound(int id) : base($"The book with id : {id} could not found")
        {
                
        }
    }
}
