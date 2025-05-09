﻿using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNETErudio.Model.Base;

namespace RestWithASPNETErudio.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }


    }
}
