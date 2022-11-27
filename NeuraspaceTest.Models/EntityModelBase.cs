// -----------------------------------------------------------------------
//  <copyright file="EntityModelBase.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NeuraspaceTest.Models
{
    public class EntityModelBase
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
    }
}