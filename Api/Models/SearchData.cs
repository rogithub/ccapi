using System.ComponentModel.DataAnnotations;
using Entities;

namespace Api.Models
{
    public class OrderCol
    {
        [Required]
        public string Col { get; set; }
        [Required]
        public Order Order { get; set; }
    }
    public class SearchData
    {
        public string Pattern { get; set; }
        public OrderCol[] Columns { get; set; }
        [Required]
        public int Limit { get; set; }
        [Required]
        public int Offset { get; set; }
    }
}
