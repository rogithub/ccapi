using System;

namespace Entities
{
    public enum Order
    {
        Asc,
        Desc,
    }
    public class OrderCol
    {
        public string Col { get; set; }
        public Order Order { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Col,
            Order == Order.Asc ? "ASC" : "DESC");
        }
    }
    public class SearchData
    {
        public string SearchPattern { get; set; }
        public string Nombre { get; set; }
        public OrderCol[] Columns { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
