using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.DBModels;

namespace WookieBooks.Api.Models
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {

            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

        }

    }
}
