// -----------------------------------------------------------------------
//  <copyright file="OperatorData.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NeuraspaceTest.DataTransferModels
{
    /// <summary>
    ///     Operator request
    /// </summary>
    public class OperatorData
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [JsonPropertyName("operator_name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the operator identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }
    }
}