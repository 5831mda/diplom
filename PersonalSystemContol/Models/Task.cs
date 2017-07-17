using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace PersonalSystemContol.Models
{
    /// <summary>
    /// Задача.
    /// </summary>
    [Table("Tasks")]
    [JsonObject]
    public class Task
    {
        [Key]
        [Column("id_task")]
        [JsonProperty("id_task")]
        public int Id { get; set; }

        [JsonProperty("id_engineer")]
        [Column("id_engineer")]
        public int EngineerId { get; set; }

        [JsonProperty("task_name")]
        [Column("task_name")]
        [Required]
        [RegularExpression("^[A-Za-zА-Яа-я]+$")]
        public string Name { get; set; }

        [JsonProperty("task_description")]
        [Column("task_description")]
        [Required]
        [RegularExpression("^[A-Za-zА-Яа-я]+$")]    
        public string Description { get; set; }

        [JsonProperty("start_time")]
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Required]
        [JsonProperty("photo_required")]
        [Column("photo_required")]
        public bool? PhotoRequired { get; set; }

        [JsonProperty("report")]
        public ICollection<Report> Reports { get; set; }
    }
}
