using AutoMapper;
using FilmeAPI.Data.Dtos;
using FilmeAPI.Models;

namespace FilmeAPI.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        
        CreateMap<Cinema, ReadCinemaDto>()//Criando mapeamento entre Cinema <--> ReadCinemaDto
            .ForMember(cinemaDto => cinemaDto.Endereco,// Pegar sómente Endereco do ReadCinemaDto
            opt => opt.MapFrom(cinema => cinema.Endereco));
        
        CreateMap<Cinema, ReadCinemaDto>().ForMember(cinemaDto => cinemaDto.Sessoes, opt => opt.MapFrom(cinema => cinema.Sessoes));
        
        CreateMap<Cinema, UpdateCinemaDto>();

    }
}
