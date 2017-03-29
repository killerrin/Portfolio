using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Portfolio.API.Models.JoinTables;
using Newtonsoft.Json;

namespace Portfolio.API.Models
{
    public class Keyword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<PortfolioItemKeyword> Portfolios { get; set; } = new List<PortfolioItemKeyword>();
        public Keyword()
        {

        }
    }
}
