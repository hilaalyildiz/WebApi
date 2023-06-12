using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.Exceptions
{
    public sealed class BookNotFoundException : NotFoundExceptions
    {
        public BookNotFoundException(int id) : base($"The book with id : {id} could not found")
        {
        }
    }
}
