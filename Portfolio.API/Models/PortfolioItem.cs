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



        #region Tags
        public List<PortfolioItemCategory> Categories { get; set; } = new List<PortfolioItemCategory>();
        public List<PortfolioItemFramework> Frameworks { get; set; } = new List<PortfolioItemFramework>();
        public List<PortfolioItemKeyword> Keywords { get; set; } = new List<PortfolioItemKeyword>();
        public List<PortfolioItemProgLanguage> ProgrammingLanguages { get; set; } = new List<PortfolioItemProgLanguage>();
        #endregion

        public PortfolioItem()
        {
        }
    }
}
