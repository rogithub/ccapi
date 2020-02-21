using System;
using AutoMapper;
using Entities;
using Api.Models;

namespace Api.Mappers
{
    public class MaterialProfile: Profile
    {
        public MaterialProfile()
        {
            this.CreateMap<Entities.Material, Models.Material>();
            this.CreateMap<Models.Material, Entities.Material>();
        }
    }
}
