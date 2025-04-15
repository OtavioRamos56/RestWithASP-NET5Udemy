using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNETErudio.Model.Base;

namespace RestWithASPNETErudio.Data.VO
{
    [Table("books")]
    public class BookVO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }
    }
}
