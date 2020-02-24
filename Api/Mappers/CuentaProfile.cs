using System;
using AutoMapper;
using Entities;
using Api.Models;

namespace Api.Mappers
{
    public class CuentaProfile: Profile
    {
        public CuentaProfile()
        {
            this.CreateMap<Entities.Cuenta, Models.Cuenta>();
            this.CreateMap<Models.Cuenta, Entities.Cuenta>();
        }
    }
}
