using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Entities;
using AutoMapper;

namespace AdocaoPB.Application.Services.AutoMapper;

public class AutoMapperConfiguration : Profile {

    public AutoMapperConfiguration() {
        RequestToEntity();
        EntityToResponse();
    }


    private void RequestToEntity() {
        CreateMap<Communication.Requests.RequestRegisterUserJson, Domain.Entities.User>();  
        CreateMap<Communication.Requests.RequestAddRefreshToken, Domain.Entities.RefreshToken>();

        CreateMap<Communication.Requests.RequestAddPet, Domain.Entities.Pet>();
        CreateMap<Communication.Requests.RequestAddEndereco, Domain.Entities.EnderecoUsers>();
        CreateMap<RequestUpdateEndereco, EnderecoUsers>();
    }


    private void EntityToResponse() {
        CreateMap<Domain.Entities.Pet, Communication.Responses.ResponseAddPet>();

        CreateMap<Domain.Entities.Pet, Communication.Responses.ResponseGetAllPets>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name));

        CreateMap<Domain.Entities.Pet, ResponseToPetsForUser>()
            .ForMember(dest => dest.Adopter, opt => opt.MapFrom(src => src.Adopter));

        CreateMap<Domain.Entities.Pet, ResponseToPetsForAdopter>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name))
            .ForMember(dest => dest.OwnerPhoneNumber, opt => opt.MapFrom(src => src.Owner.PhoneNumber));

        CreateMap<Pet, ResponseGetPetById>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new ResponseUser {
                // Mapeie as propriedades do Owner e EnderecoUser aqui
                Name = src.Owner.Name,
                Email = src.Owner.Email,
                PhoneNumber = src.Owner.PhoneNumber,
                ResponseEnderecoUser = src.Owner.EnderecoUser != null ? new ResponseEnderecoUser {
                    // Mapeie as propriedades de EnderecoUser aqui se não for nulo
                    Cidade = src.Owner.EnderecoUser.Cidade,
                    Estado = src.Owner.EnderecoUser.Estado,
                    Complemento = src.Owner.EnderecoUser.Complemento,
                    Logradouro = src.Owner.EnderecoUser.Logradouro,
                    Numero = src.Owner.EnderecoUser.Numero,
                    Bairro = src.Owner.EnderecoUser.Bairro
                } : null
            }));

        CreateMap<Domain.Entities.User, ResponseGetUserById>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new ResponseUser
            {
                // Mapeie as propriedades do Owner e EnderecoUser aqui
                Name = src.Name,
                Email = src.Email,
                PhoneNumber = src.PhoneNumber,
                ResponseEnderecoUser = src.EnderecoUser != null ? new ResponseEnderecoUser
                {
                    // Mapeie as propriedades de EnderecoUser aqui se não for nulo
                    Cidade = src.EnderecoUser.Cidade,
                    Estado = src.EnderecoUser.Estado,
                    Complemento = src.EnderecoUser.Complemento,
                    Logradouro = src.EnderecoUser.Logradouro,
                    Numero = src.EnderecoUser.Numero,
                    Bairro = src.EnderecoUser.Bairro
                } : null
            }));

        CreateMap<User, ResponseGetAllUsersJson>();

    }

}
