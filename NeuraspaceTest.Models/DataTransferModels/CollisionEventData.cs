// -----------------------------------------------------------------------
//  <copyright file="CollisionEventData.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NeuraspaceTest.Models.DataTransferModels
{
    /// <summary>
    ///     Collision Event Request
    /// </summary>
    public class CollisionEventData
    {
        /// <summary>
        ///     Gets or sets the chaser object identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("chaser_object_id")]
        public string ChaserObjectId { get; set; }

        /// <summary>
        ///     Gets or sets the collision date.
        /// </summary>
        [Required]
        [JsonPropertyName("collision_date")]
        public DateTime CollisionDate { get; set; }

        /// <summary>
        ///     Gets or sets the event identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        /// <summary>
        ///     Gets or sets the message identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }

        /// <summary>
        ///     Gets or sets the operator identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }

        /// <summary>
        ///     Gets or sets the probability of collision.
        /// </summary>
        [Required]
        [JsonPropertyName("probability_of_collision")]
        public double ProbabilityOfCollision { get; set; }

        /// <summary>
        ///     Gets or sets the satellite identifier.
        /// </summary>
        [Required]
        [JsonPropertyName("satellite_id")]
        public string SatelliteId { get; set; }
    }
}