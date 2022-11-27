// -----------------------------------------------------------------------
//  <copyright file="IEntityServiceBase.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace NeuraspaceTest.Contracts.Services
{
    public interface IEntityServiceBase<TRequest, TEntity>
        where TRequest : class
        where TEntity : class
    {
        /// <summary>
        ///     Adds record to the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<IServiceResponse<TRequest>> AddAsync(TRequest request);

        /// <summary>
        ///     Deletes a record by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<IServiceResponse<int>> DeleteAsync(int id);

        /// <summary>
        ///     Gets record by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<IServiceResponse<TRequest>> GetAsync(int id);

        /// <summary>
        ///     Gets a list of SLAs
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IServiceResponse<List<TRequest>> GetList(Func<TEntity, bool> query = null);

        /// <summary>
        ///     Adds record to the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<IServiceResponse<TRequest>> UpdateAsync(int id, TRequest request);
    }
}