using AutoMapper;
using AdminGestaoApi.Domain.Entity.Usuario;
using AdminGestaoApi.ApplicationService.Requests.Usuario;
using AdminGestaoApi.ApplicationService.Responses.Usuario;

namespace AdminGestaoApi.Infra.CrossCutting.IoC.Profiles.Usuario
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioEntity, UsuarioRequestInsercao>().ReverseMap();
            CreateMap<UsuarioEntity, UsuarioResponse>().ReverseMap();
            CreateMap<UsuarioEntity, UsuarioRequestUpdate>().ReverseMap();
        }

    }
}
