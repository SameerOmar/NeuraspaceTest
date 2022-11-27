// -----------------------------------------------------------------------
//  <copyright file="OperatorService.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using NeuraspaceTest.Contracts;
using NeuraspaceTest.Contracts.Services;
using NeuraspaceTest.DataAccess;
using NeuraspaceTest.DataTransferModels;
using NeuraspaceTest.Models;

namespace NeuraspaceTest.Services
{
    /// <summary>
    ///     Operator Service class.
    /// </summary>
    public class OperatorService : EntityServiceBase<OperatorData, Operator>,
        IOperatorService<OperatorData, Operator>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OperatorService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appDbContext">The application database context.</param>
        public OperatorService(ILogger<OperatorService> logger, IMapper mapper, AppDbContext appDbContext)
            : base(logger, mapper, appDbContext)
        {
        }
    }
}