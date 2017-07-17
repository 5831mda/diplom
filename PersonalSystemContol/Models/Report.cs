using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace PersonalSystemContol.Models
{
    /// <summary>
    /// Отчет.
    /// </summary>
    [Table("Reports")]
    [JsonObject]
    public class Report
    {
        [Key]
        [Column("id_report")]
        [JsonProperty("id_report")]
        public int Id { get; set; }

        [Column("id_task")]
        [JsonProperty("task")]
        [ConcurrencyCheck]
        public int TaskId { get; set; }

        [JsonProperty("comment")]
        [Column("comment")]
        [Required]
        [RegularExpression("^[A-Za-zА-Яа-я]+$")]
        public string Comment { get; set; }

        [JsonProperty("end_time")]
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("gps_longitude")]
        [Column("gps_longitude")]
        public double GpsLongitude { get; set; }

        [JsonProperty("gps_latitude")]
        [Column("gps_latitude")]
        public double GpsLatitude { get; set; }

        [JsonProperty("photo")]
        public ICollection<Photo> Photos { get; set; }
    }
}
