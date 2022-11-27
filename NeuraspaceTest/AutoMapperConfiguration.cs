// -----------------------------------------------------------------------
//  <copyright file="AutoMapperConfiguration.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using NeuraspaceTest.DataTransferModels;
using NeuraspaceTest.Models;

namespace NeuraspaceTest
{
    /// <summary>
    ///     AutoMapper configuration class.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class AutoMapperConfiguration : Profile
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoMapperConfiguration" /> class.
        ///     Creates all the mappings
        /// </summary>
        public AutoMapperConfiguration()
        {
            CreateMap<CollisionEvent, CollisionEventData>();
            CreateMap<CollisionEventData, CollisionEvent>();

            CreateMap<Operator, OperatorData>();
            CreateMap<OperatorData, Operator>();

            CreateMap<Satellite, SatelliteData>();
            CreateMap<SatelliteData, Satellite>();
        }
    }
}