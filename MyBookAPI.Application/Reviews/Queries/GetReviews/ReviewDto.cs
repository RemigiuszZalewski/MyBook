using AutoMapper;
using MyBookAPI.Application.Common.Mappings;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Application.Reviews.Queries.GetReviews
{
    public class ReviewDto : IMapFrom<Review>
    {
        public string Text { get; set; }
        public int Stars { get; set; }
        public string User { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, ReviewDto>()
                .ForMember(d => d.User, src => src.MapFrom(src => src.User.UserName.ToString()));
        }
    }
}
