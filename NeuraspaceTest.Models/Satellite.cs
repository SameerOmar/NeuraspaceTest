// -----------------------------------------------------------------------
//  <copyright file="Satellite.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NeuraspaceTest.Models
{
    [Index(nameof(SatelliteId))]
    public class Satellite : EntityModelBase
    {
        public virtual List<CollisionEvent> CollisionEvents { get; set; }

        [Required]
        [Column("satellite_name")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("operator_id")]
        public virtual Operator Operator { get; set; }

        [Required]
        [Column("satellite_id")]
        public string SatelliteId { get; set; }
    }
}