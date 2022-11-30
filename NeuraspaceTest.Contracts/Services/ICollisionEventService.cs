// -----------------------------------------------------------------------
//  <copyright file="ICollisionEventService.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace NeuraspaceTest.Contracts.Services
{
    public interface ICollisionEventService<TRequest, TEntity> : IEntityServiceBase<TRequest, TEntity>
        where TRequest : class
        where TEntity : class
    {
        /// <summary>
        ///     Cancels the message asynchronous.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="operatorId">The operator identifier.</param>
        /// <returns></returns>
        Task<IServiceResponse<TRequest>> CancelMessageAsync(string messageId, string operatorId);

        /// <summary>
        ///     Gets the collision event warnings.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <param name="all">if set to true will retrieve all the warnings.</param>
        /// <returns></returns>
        IServiceResponse<List<TRequest>> GetCollisionEventWarnings(string operatorId, bool all);
    }
}