// -----------------------------------------------------------------------
//  <copyright file="SatelliteService.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Microsoft.Extensions.Logging;
using NeuraspaceTest.Contracts;
using NeuraspaceTest.Contracts.Services;
using NeuraspaceTest.DataAccess;
using NeuraspaceTest.Models;
using NeuraspaceTest.Models.DataTransferModels;

namespace NeuraspaceTest.Services
{
    /// <summary>
    ///     Satellite Service class.
    /// </summary>
    public class SatelliteService : EntityServiceBase<SatelliteData, Satellite>,
        ISatelliteService<SatelliteData, Satellite>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SatelliteService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appDbContext">The application database context.</param>
        public SatelliteService(ILogger<SatelliteService> logger, IMapper mapper, AppDbContext appDbContext)
            : base(logger, mapper, appDbContext)
        {
        }

        /// <summary>
        ///     Adds satellite record to the database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<IServiceResponse<SatelliteData>> AddAsync(SatelliteData data)
        {
            var response = new ServiceResponse<SatelliteData>();

            if (data is null)
            {
                response.Success = false;
                response.Message = NothingToAdd;

                return response;
            }

            var operatorId = data.OperatorId;

            if (string.IsNullOrWhiteSpace(operatorId))
            {
                response.Success = false;
                response.Message = "Missing operator identifier";

                return response;
            }

            var operatorRecord = AppDbContext.Operators.FirstOrDefault(o => o.OperatorId == operatorId);

            if (operatorRecord is null)
            {
                response.Success = false;
                response.Message = NotFound;

                return response;
            }

            try
            {
                var satellite = Mapper.Map<Satellite>(data);

                satellite.Operator = operatorRecord;

                var result = AppDbContext.Set<Satellite>().Add(satellite);

                await AppDbContext.SaveChangesAsync();

                if (result.Entity is not EntityModelBase entityModelBase || entityModelBase.Id == 0)
                {
                    response.Success = false;
                    response.Message = RecordNotAdded;

                    return response;
                }

                response.RecordId = entityModelBase.Id;
                response.Result = Mapper.Map<SatelliteData>(result.Entity);
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }
    }
}