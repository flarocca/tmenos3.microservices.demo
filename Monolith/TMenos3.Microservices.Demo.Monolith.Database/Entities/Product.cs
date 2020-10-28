using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TMenos3.Microservices.Demo.Monolith.Database.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public ProductStock Stock { get; set; }

        public ProductPrice Price { get; set; }
    }
}
