using System;
using AutoMapper;
using Entities;
using Api.Models;

namespace Api.Mappers
{
    public class DatosFacturacionProfile: Profile
    {
        public DatosFacturacionProfile()
        {
            this.CreateMap<Entities.DatosFacturacion, Models.DatosFacturacion>();
            this.CreateMap<Models.DatosFacturacion, Entities.DatosFacturacion>();
        }
    }
}
