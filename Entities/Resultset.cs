using System;

namespace Entities
{
    public class Resultset<T>
    {
        public Resultset()
        {

        }

        public Resultset(Int64 totalRows, T payload)
        {
            TotalRows = totalRows;
            Payload = payload;
        }
        public Int64 TotalRows { get; set; }
        public T Payload { get; set; }
    }
}
