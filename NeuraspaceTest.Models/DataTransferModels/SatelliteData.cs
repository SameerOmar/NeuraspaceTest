// -----------------------------------------------------------------------
//  <copyright file="SatelliteData.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NeuraspaceTest.Models.DataTransferModels
{
    /// <summary>
    ///     Satellite Request
    /// </summary>
    public class SatelliteData
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [JsonPropertyName("satellite_name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the operator identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }

        /// <summary>
        ///     Gets or sets the satellite identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("satellite_id")]
        public string SatelliteId { get; set; }
    }
}