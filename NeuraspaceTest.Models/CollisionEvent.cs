// -----------------------------------------------------------------------
//  <copyright file="CollisionEvent.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NeuraspaceTest.Models
{
    [Index(nameof(MessageId), IsUnique = true)]
    [Index("operator_id")]
    [Index("satellite_id")]
    public class CollisionEvent : EntityModelBase
    {
        public bool Canceled { get; set; }

        [Required]
        [Column("chaser_object_id")]
        public string ChaserObjectId { get; set; }

        [Required]
        [Column("collision_date")]
        public DateTime CollisionDate { get; set; }

        [Required]
        [Column("event_id")]
        public string EventId { get; set; }

        [Required]
        [Column("message_id")]
        public string MessageId { get; set; }

        [Required]
        [ForeignKey("operator_id")]
        public virtual Operator Operator { get; set; }

        [Required]
        [Column("probability_of_collision")]
        public double ProbabilityOfCollision { get; set; }

        [Required]
        [ForeignKey("satellite_id")]
        public virtual Satellite Satellite { get; set; }
    }
}