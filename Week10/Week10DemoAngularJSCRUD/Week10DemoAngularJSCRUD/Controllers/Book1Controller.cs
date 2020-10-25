using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Week10DemoAngularJSCRUD.Interface;
using Week10DemoAngularJSCRUD.Models;

namespace Week10DemoAngularJSCRUD.Controllers
{
    public class Book1Controller : ApiController
    {
        static readonly IBookRepository repository = new BookRepository();

        public IEnumerable<Book> GetAllBooks() 
        { 
            return repository.GetAll(); 
        }

        public IEnumerable<Book> PutBook(int id, Book book)
        {
            book.Id = id;
            if (repository.Update(book))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }

        public bool DeleteBook(int id)
        {
            if (repository.Delete(id))
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }
    }
}
