using System;

namespace Api.Models
{
    public class Resultset<T>
    {
        public Int64 TotalRows { get; set; }
        public T Payload { get; set; }
    }
}
