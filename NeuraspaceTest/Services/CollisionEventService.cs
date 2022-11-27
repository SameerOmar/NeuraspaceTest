// -----------------------------------------------------------------------
//  <copyright file="CollisionEventService.cs" company="Excerya">
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
    ///     Collision Event Service class
    /// </summary>
    public class CollisionEventService : EntityServiceBase<CollisionEventData, CollisionEvent>,
        ICollisionEventService<CollisionEventData, CollisionEvent>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CollisionEventService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appDbContext">The application database context.</param>
        public CollisionEventService(ILogger<CollisionEventService> logger, IMapper mapper, AppDbContext appDbContext)
            : base(logger, mapper, appDbContext)
        {
        }

        /// <summary>
        ///     Validates the message.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="message">The message.</param>
        /// <param name="operatorId">The operator identifier.</param>
        /// <returns></returns>
        public bool ValidateMessage(IServiceResponse<CollisionEventData> response, CollisionEvent message,
            string operatorId)
        {
            if (message.Operator.OperatorId == operatorId)
            {
                response.Success = false;
                response.Message = "Message not belong to the operator";

                return false;
            }

            if (message.CollisionDate <= DateTime.Now)
            {
                response.Success = false;
                response.Message = "Message in the past";

                return false;
            }

            return true;
        }

        /// <summary>
        ///     Cancels the message asynchronous.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="operatorId">The operator identifier.</param>
        /// <returns></returns>
        public async Task<IServiceResponse<CollisionEventData>> CancelMessageAsync(string messageId,
            string operatorId)
        {
            var response = new ServiceResponse<CollisionEventData>();
            var message = AppDbContext.CollisionEvents.FirstOrDefault(m => m.MessageId == messageId);

            if (message == null)
            {
                response.Success = false;
                response.Message = NotFound;

                return response;
            }

            if (!ValidateMessage(response, message, operatorId))
            {
                return response;
            }

            try
            {
                message.Canceled = true;

                AppDbContext.Set<CollisionEvent>().Update(message);
                await AppDbContext.SaveChangesAsync();

                response.RecordId = message.Id;
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Gets the collision event warnings.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <param name="all"></param>
        /// <returns></returns>
        public async Task<IServiceResponse<List<CollisionEventData>>> GetCollisionEventWarnings(string operatorId,
            bool all = false)
        {
            var response = new ServiceResponse<List<CollisionEventData>>();

            try
            {
                var entities = AppDbContext.CollisionEvents
                    .Where(c => c.Operator.OperatorId == operatorId &&
                                c.CollisionDate > DateTime.Now.ToUniversalTime() &&
                                c.ProbabilityOfCollision >= 0.75)
                    .GroupBy(c => c.Satellite.SatelliteId,
                        (key, xs) => xs.OrderBy(c => c.CollisionDate).ThenByDescending(c => c.ProbabilityOfCollision)
                            .First());

                if (all)
                {
                    entities = AppDbContext.CollisionEvents
                        .Where(c => c.Operator.OperatorId == operatorId &&
                                    c.CollisionDate > DateTime.Now.ToUniversalTime() &&
                                    c.ProbabilityOfCollision >= 0.75)
                        .OrderBy(c => c.CollisionDate)
                        .ThenByDescending(c => c.ProbabilityOfCollision);
                }

                response.Result = Mapper.Map<List<CollisionEventData>>(entities.ToList());
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Adds data record to the database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<IServiceResponse<CollisionEventData>> AddAsync(CollisionEventData data)
        {
            var response = new ServiceResponse<CollisionEventData>();

            if (data is null)
            {
                response.Success = false;
                response.Message = NothingToAdd;

                return response;
            }

            if (AppDbContext.CollisionEvents.Any(m => m.MessageId == data.MessageId))
            {
                response.Success = false;
                response.Message = "Message already exist";

                return response;
            }

            if (data.ProbabilityOfCollision is not (>= 0 and <= 1))
            {
                response.Success = false;
                response.Message = "Probability of collision is out of the range";

                return response;
            }

            var operatorId = data.OperatorId;
            var satelliteId = data.SatelliteId;

            if (string.IsNullOrWhiteSpace(operatorId) || string.IsNullOrWhiteSpace(satelliteId))
            {
                response.Success = false;
                response.Message = "Missing operator/satellite identifier";

                return response;
            }

            var operatorRecord = AppDbContext.Operators.FirstOrDefault(o => o.OperatorId == operatorId);
            var satelliteRecord = AppDbContext.Satellites.FirstOrDefault(s =>
                s.SatelliteId == satelliteId && s.Operator.OperatorId == operatorId);

            if (operatorRecord is null || satelliteRecord is null)
            {
                response.Success = false;
                response.Message = NotFound;

                return response;
            }

            try
            {
                var collisionEvent = Mapper.Map<CollisionEvent>(data);

                collisionEvent.Operator = operatorRecord;
                collisionEvent.Satellite = satelliteRecord;

                var entryResult = AppDbContext.Set<CollisionEvent>().Add(collisionEvent);

                await AppDbContext.SaveChangesAsync();

                if (entryResult.Entity is not EntityModelBase entityModelBase || entityModelBase.Id == 0)
                {
                    response.Success = false;
                    response.Message = RecordNotAdded;

                    return response;
                }

                response.RecordId = entityModelBase.Id;
                response.Result = Mapper.Map<CollisionEventData>(entryResult.Entity);
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }
    }
}