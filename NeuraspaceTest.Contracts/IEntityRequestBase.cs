// -----------------------------------------------------------------------
//  <copyright file="IEntityRequestBase.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace NeuraspaceTest.Contracts
{
    public interface IEntityRequestBase
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }
    }
}