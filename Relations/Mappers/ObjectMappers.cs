using AutoMapper;
using Relations.DAL;
using Relations.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Mappers
{
    internal class ObjectMappers
    {


    }
    internal class ProductMapper: Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductDTO,Product>().ReverseMap();
            
        }
    }
}
