using AutoMapper;
using Library.BLL.DTOs.PublisherDTO;

namespace Library.BLL.Mapping
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherDto>();

            CreateMap<CreatePublisherDto, Publisher>();

            CreateMap<UpdatePublisherDto, Publisher>();
        }
    }
}