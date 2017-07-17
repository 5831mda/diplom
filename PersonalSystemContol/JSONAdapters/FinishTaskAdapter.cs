using System;

using Newtonsoft.Json;

using PersonalSystemContol.Models;

namespace PersonalSystemContol.JSONAdapters
{
    /// <summary>
    /// Адаптер дла данных, посылаемых для закрытия задачи.
    /// </summary>
    [JsonObject]
    public class FinishTaskAdapter
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("gps_longitude")]
        public double GpsLongitude { get; set; }

        [JsonProperty("gps_latitude")]
        public double GpsLatitude { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// Генерация отчета на основе полученных данных.
        /// </summary>
        /// <returns></returns>
        public Report GetReport()
        {
            return new Report()
                   {
                       Comment = this.Comment,
                       EndTime = this.EndTime,
                       GpsLatitude = this.GpsLatitude,
                       GpsLongitude = this.GpsLongitude
                   };
        }

        /// <summary>
        /// Генерация записи с фото на основе полученных данных.
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public Photo GetPhoto(int reportId)
        {
            return new Photo()
                   {
                       Link = this.Link,
                       ReportId = reportId
                   };
        }

    }
}
