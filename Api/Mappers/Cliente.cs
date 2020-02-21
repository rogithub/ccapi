using System;
using AutoMapper;
using Entities;
using Api.Models;

namespace Api.Mappers
{
    public class ClienteProfile: Profile
    {
        public ClienteProfile()
        {
            this.CreateMap<Entities.Cliente, Models.Cliente>();
            this.CreateMap<Models.Cliente, Entities.Cliente>();
        }
    }
}
