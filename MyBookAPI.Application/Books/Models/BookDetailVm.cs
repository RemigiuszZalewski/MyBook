using AutoMapper;
using MyBookAPI.Application.Common.Mappings;
using MyBookAPI.Domain.Entities;
using System.Collections.Generic;

namespace MyBookAPI.Application.Books.Models
{
    public class BookDetailVm : IMapFrom<Book>
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool ToBeSold { get; set; }
        public int Pages { get; set; }
        public string Category { get; set; }
        public string PublishingHouse { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDetailVm>()
                   .ForMember(d => d.Description, map => map.MapFrom<DescriptionResolver>())
                   .ForMember(d => d.PublishingHouse, map => map.MapFrom<PublishingHouseResolver>())
                   .ForMember(d => d.Price, map => map.MapFrom<PriceResolver>())
                   .ForMember(d => d.Category, map => map.MapFrom(src => src.Category.Name))
                   .ForMember(d => d.Author, map => map.MapFrom(src => src.Author.AuthorName.ToString()));
        }

        private class DescriptionResolver : IValueResolver<Book, object, string>
        {
            public string Resolve(Book source, object destination, string destMember, ResolutionContext context)
            {
                if (source.Description.Text is not null)
                {
                    return source.Description.Text;
                }
                return string.Empty;
            }
        }

        private class PublishingHouseResolver : IValueResolver<Book, object, string>
        {
            public string Resolve(Book source, object destination, string destMember, ResolutionContext context)
            {
                if (source.PublishingHouse.Name is not null)
                {
                    return source.PublishingHouse.Name;
                }
                return string.Empty;
            }
        }

        private class PriceResolver : IValueResolver<Book, object, decimal?>
        {
            public decimal? Resolve(Book source, object destination, decimal? destMember, ResolutionContext context)
            {
                if (source.Price is not null && source.Price > 0)
                {
                    return source.Price;
                }
                return null;
            }
        }
    }
}
