using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.DBModels;

namespace WookieBooks.Api.DBContext
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataBaseContext(serviceProvider.GetRequiredService<DbContextOptions<DataBaseContext>>()))
            {
                if(context.Books.Any())
                {
                    return; //Data has already been seesded.
                }

                context.Books.AddRange(
                      new BookDTO
                      {
                          Id = 1,
                          Author = "Andy Hunt and Dave Thomas",
                          Description = "The Pragmatic Programmer: From Journeyman to Master is a book about computer programming and software engineering, written by Andrew Hunt and David Thomas and published in October 1999.",
                          Price = 31.50m,
                          CoverImage = imgToByteArray(Directory.GetCurrentDirectory() + @"\images\clean_code.jpg"),
                          Title = "The Pragmatic Programmer"
                      },
                    new BookDTO
                    {
                        Id = 2,
                        Author = "Robert Cecil Martin",
                        Description = "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees.",
                        CoverImage = imgToByteArray(Directory.GetCurrentDirectory() + @"\images\tpp.jpg"),
                        Price = 42.69m,
                        Title = "Clean Code"
                    }
                );

                context.SaveChanges();
            }
        }

        private static byte[] imgToByteArray(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
