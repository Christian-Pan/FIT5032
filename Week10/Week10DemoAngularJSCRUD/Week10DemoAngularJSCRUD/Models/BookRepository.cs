using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week10DemoAngularJSCRUD.Interface;
using Week10DemoAngularJSCRUD.Models;

namespace Week10DemoAngularJSCRUD.Models
{
    public class BookRepository : IBookRepository
    {
        BookDatabaseEntities BookDB = new BookDatabaseEntities();

        IEnumerable<Book> IBookRepository.GetAll()
        {  //throw new NotImplementedException();  } 
            return BookDB.Books;
        }

        Book IBookRepository.Get(int id)
        {  //throw new NotImplementedException();  
            return BookDB.Books.Find(id);
        }

        Book IBookRepository.Add(Book item)
        {
            //throw new NotImplementedException();  
            if (item == null) 
            { 
                throw new ArgumentNullException("item"); 
            }

            BookDB.Books.Add(item); 
            BookDB.SaveChanges();
            return item;
        }

        bool IBookRepository.Update(Book item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var books = BookDB.Books.Single(a => a.Id == item.Id);
            books.BookTitle = item.BookTitle;
            books.Author = item.Author;
            books.Price = item.Price;
            BookDB.SaveChanges();

            return true;
        }

        bool IBookRepository.Delete(int id)
        {
            Book books = BookDB.Books.Find(id);
            BookDB.Books.Remove(books);
            BookDB.SaveChanges();
            return true;
        }
    }
}