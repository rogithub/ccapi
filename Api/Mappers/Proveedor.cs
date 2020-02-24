using System;
using AutoMapper;
using Entities;
using Api.Models;

namespace Api.Mappers
{
    public class ProveedorProfile: Profile
    {
        public ProveedorProfile()
        {
            this.CreateMap<Entities.Proveedor, Models.Proveedor>();
            this.CreateMap<Models.Proveedor, Entities.Proveedor>();
        }
    }
}
