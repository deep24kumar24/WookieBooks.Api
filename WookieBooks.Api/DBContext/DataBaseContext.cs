using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.DBModels;

namespace WookieBooks.Api.DBContext
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {

        }

        public DbSet<BookDTO> Books { get; set; }

    }
}
