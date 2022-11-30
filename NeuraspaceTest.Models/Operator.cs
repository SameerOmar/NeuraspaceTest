// -----------------------------------------------------------------------
//  <copyright file="Operator.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NeuraspaceTest.Models
{
    [Index(nameof(OperatorId), IsUnique = true)]
    public class Operator : EntityModelBase
    {
        public virtual List<CollisionEvent> CollisionEvents { get; set; }

        [Required]
        [Column("operator_name")]
        public string Name { get; set; }

        [Required]
        [Column("operator_id")]
        public string OperatorId { get; set; }

        public virtual List<Satellite> Satellites { get; set; }
    }
}