using AutoMapper;
using OganiShop.Model.Models;
using OganiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OganiShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
        }
    }
}