using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Portfolio.API.Models.JoinTables;

namespace Portfolio.API.Models
{
    public class PortfolioItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Url)]
        public string SourceCodeUrl { get; set; }
        public List<PortfolioItemLink> Links { get; set; } = new List<PortfolioItemLink>();

        public bool Published { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public string Awards { get; set; }
        public string MyRole { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }

        public List<PortfolioItemTag> Tags { get; set; } = new List<PortfolioItemTag>();
        public List<PortfolioItemRelatedItem> RelatedItems { get; set; } = new List<PortfolioItemRelatedItem>();

        public PortfolioItem()
        {
        }
    }
}
