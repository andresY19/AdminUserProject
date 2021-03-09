using AutoMapper;
using Queue.Models;
using Queue.Models.ResponseDTO;

namespace Queue.Action.Mapper
{
    public abstract class AutoMapperBase
    {
        protected readonly IMapper _mapper;

        protected AutoMapperBase()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<Agent_User, ResponseLoginDTO>();
                x.CreateMap<Agent_Configuration, ResponseConfigurationDTO>();

            });
            _mapper = config.CreateMapper();
        }
    }
}