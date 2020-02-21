using System;
using AutoMapper;
using Entities;
using Api.Models;

namespace Api.Mappers
{
    public class ResultsetProfile<T>: Profile
    {
        public ResultsetProfile()
        {
            this.CreateMap<Entities.Resultset<T>, Models.Resultset<T>>();
            this.CreateMap<Models.Resultset<T>, Entities.Resultset<T>>();
        }
    }
}
