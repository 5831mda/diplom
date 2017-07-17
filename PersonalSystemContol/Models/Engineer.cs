using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace PersonalSystemContol.Models
{
    /// <summary>
    /// Инженер.
    /// </summary>
    [Table("Engineers")]
    [JsonObject]
    public class Engineer
    {
        [JsonProperty("id_engineer")]
        [Key]
        [Column("id_engineer")]
        public int Id { get; set; }

        [JsonProperty("full_name")]
        [Column("full_name")]
        public string FullName { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
