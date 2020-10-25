using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week10DemoAngularJSCRUD.Models;

namespace Week10DemoAngularJSCRUD.Interface
{
    interface IBookRepository
    {
        IEnumerable<Book> GetAll(); 
        Book Get(int id); 
        Book Add(Book item); 
        bool Update(Book item); 
        bool Delete(int id);
    }
}
