using AutoMapper;
using ClubAccessSystem.API.Models.Clientes;
using ClubAccessSystem.API.Models.RegistrosAcceso;
using ClubAccessSystem.API.Models.Roles;
using ClubAccessSystem.API.Models.Usuarios;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Usuario
            CreateMap<Usuarios, UsuariosModels>();
            CreateMap<UsuariosModels, Usuarios>();
            CreateMap<AddUsuariosModels, Usuarios>();
            CreateMap<UpdateUsuariosModels, Usuarios>();
            CreateMap<Usuarios, UpdateUsuariosModels>();

            //Clientes
            CreateMap<Clientes, ClientesModels>();
            CreateMap<ClientesModels, Clientes>();
            CreateMap<AddClientesModels, Clientes>();
            CreateMap<UpdateClientesModels, Clientes>();

            //RegistrosAcceso
            CreateMap<RegistrosAcceso, RegistrosAccesoModels>();
            CreateMap<RegistrosAccesoModels, RegistrosAcceso>();
            CreateMap<AddRegistrosAccesoModels, RegistrosAcceso>();
            CreateMap<UpdateRegistrosAccesoModels, RegistrosAcceso>();

            //Roles
            CreateMap<Roles, RolesModels>();
            CreateMap<RolesModels, Roles>();
            CreateMap<AddTipoClientesModels, Roles>();
            CreateMap<UpdateTipoClientesModels, Roles>();

            //TipoClientes
            CreateMap<TipoClientes, TipoClientesModels>();
            CreateMap<TipoClientesModels, TipoClientes>();
            CreateMap<AddTipoClientesModels, TipoClientes>();
            CreateMap<UpdateTipoClientesModels, TipoClientes>();

        }
    }
}
