using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Specification.Models.Specs;

namespace Specification.Models
{
    public class SmartPlaylist
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        [NotMapped]
        public GlobalSongSpecification Specification { get; set; }

        [JsonIgnore, Required]
        [Display(Name = "Json")]
        public string SpecificationJson
        {
            get { return JsonConvert.SerializeObject(Specification); }
            set { Specification = JsonConvert.DeserializeObject<GlobalSongSpecification>(value); }
        }
    }
}
