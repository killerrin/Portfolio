using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.API.Models
{
    public class PortfolioItemLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int PortfolioItemID { get; set; }

        [JsonIgnore]
        [ForeignKey("PortfolioItemID")]
        public PortfolioItem PortfolioItem { get; set; }

        public PortfolioItemLink()
        {

        }
    }
}