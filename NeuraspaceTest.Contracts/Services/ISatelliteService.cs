// -----------------------------------------------------------------------
//  <copyright file="ISatelliteService.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace NeuraspaceTest.Contracts.Services
{
    public interface ISatelliteService<TRequest, TEntity> : IEntityServiceBase<TRequest, TEntity>
        where TRequest : class
        where TEntity : class
    {
    }
}