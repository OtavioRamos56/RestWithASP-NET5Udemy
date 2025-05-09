﻿using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNETErudio.HyperMedia.Abstract;
using RestWithASPNETUdemy.Hypermedia;

namespace RestWithASPNETErudio.Data.VO
{
    [Table("person")]
    public class PersonVO : ISupportHyperMedia
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }
        
        public string Gender { get; set; }

        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get ; set ; } = new List<HyperMediaLink>();
    }
}
