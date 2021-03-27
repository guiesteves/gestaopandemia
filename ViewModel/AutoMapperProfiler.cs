using AutoMapper;
using CVC19.Data.Dao;
using CVC19.Models;

namespace CVC19.ViewModel
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(PaisDao paisDao)
        {
            CreateMap<TipoAgentePatogenicoViewModel, TipoAgentePatogenico>();
            CreateMap<TipoAgentePatogenico, TipoAgentePatogenicoViewModel>();

            CreateMap<TipoVacinaViewModel, TipoVacina>();
            CreateMap<TipoVacina, TipoVacinaViewModel>();

            CreateMap<LaboratorioViewModel, Laboratorio>();
            CreateMap<Laboratorio, LaboratorioViewModel>()
                .ForMember(dest => dest.NomePais, opt => opt.MapFrom(ori => ori.Pais.Nome));

            CreateMap<VacinaViewModel, Vacina>();
            CreateMap<Vacina, VacinaViewModel>()
                .ForMember(dest => dest.AgentePatogenicoNome, opt => opt.MapFrom(ori => ori.AgentePatogenico.Nome))
                .ForMember(dest => dest.LaboratorioNome, opt => opt.MapFrom(ori => ori.Laboratorio.Nome))
                .ForMember(dest => dest.TipoVacinaNome, opt => opt.MapFrom(ori => ori.TipoVacina.Nome));

            CreateMap<VarianteAgentePatogenicoViewModel, VarianteAgentePatogenico>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(ori => paisDao.RecuperarPorId(int.Parse(ori.PaisId))));
            CreateMap<VarianteAgentePatogenico, VarianteAgentePatogenicoViewModel>().ForMember(dest => dest.PaisNome, opt => opt.MapFrom(ori => ori.Pais.Nome));


            CreateMap<AgentePatogenicoViewModel, AgentePatogenico>()
                .ForMember(dest => dest.ListaVarianteAgentePatogenico, opt => opt.MapFrom(ori => ori.ListaVarianteAgentePatogenicoViewModel));
            CreateMap<AgentePatogenico, AgentePatogenicoViewModel>()
                .ForMember(dest => dest.TipoAgentePatogenicoNome, opt => opt.MapFrom(ori => ori.TipoAgentePatogenico.Nome))
                .ForMember(dest => dest.ListaVarianteAgentePatogenicoViewModel, opt => opt.MapFrom(ori => ori.ListaVarianteAgentePatogenico));
        }
    }
}
