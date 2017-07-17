using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Newtonsoft.Json;

namespace PersonalSystemContol.Models
{
    /// <summary>
    /// Фото.
    /// </summary>
    [Table("Photos")]
    [JsonObject]
    public class Photo
    {
        [Key]
        [Column("id_photo")]
        [JsonProperty("id_photo")]
        public int Id { get; set; }

        [JsonProperty("report")]
        [Column("id_report")]
        public int? ReportId { get; set; }

        [JsonProperty("link")]
        [Column("link")]
        public string Link { get; set; }
    }
}
