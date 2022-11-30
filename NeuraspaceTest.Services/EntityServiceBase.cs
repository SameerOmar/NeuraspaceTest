// -----------------------------------------------------------------------
//  <copyright file="EntityServiceBase.cs" company="Excerya">
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

namespace NeuraspaceTest.Services
{
    /// <summary>
    ///     Entity Service base class.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="NeuraspaceTest.Contracts.Services.IEntityServiceBase&lt;TRequest, TEntity&gt;" />
    public abstract class EntityServiceBase<TRequest, TEntity> : IEntityServiceBase<TRequest, TEntity>
        where TRequest : class
        where TEntity : class
    {
        #region

        /// <summary>
        ///     The application database context
        /// </summary>
        protected readonly AppDbContext AppDbContext;

        /// <summary>
        ///     The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        ///     The mapper
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        ///     The not found constant string.
        /// </summary>
        protected const string NotFound = "Not found";

        /// <summary>
        ///     The nothing to add constant string.
        /// </summary>
        protected const string NothingToAdd = "Nothing to add";

        /// <summary>
        ///     The nothing to delete constant string.
        /// </summary>
        protected const string NothingToDelete = "Nothing to delete";

        /// <summary>
        ///     The nothing to get constant string.
        /// </summary>
        protected const string NothingToGet = "Nothing to get";

        /// <summary>
        ///     The nothing to modify constant string.
        /// </summary>
        protected const string NothingToModify = "Nothing to modify";

        /// <summary>
        ///     The not updateable constant string.
        /// </summary>
        protected const string NotUpdateable = "Not updateable";

        /// <summary>
        ///     The record not added constant string.
        /// </summary>
        protected const string RecordNotAdded = "Record not added";

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityServiceBase{TRequest, TEntity}" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appDbContext">The application database context.</param>
        protected EntityServiceBase(ILogger logger, IMapper mapper, AppDbContext appDbContext)
        {
            Logger = logger;
            Mapper = mapper;
            AppDbContext = appDbContext;
        }

        /// <summary>
        ///     Adds request record to the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<IServiceResponse<TRequest>> AddAsync(TRequest request)
        {
            var response = new ServiceResponse<TRequest>();

            if (request is null)
            {
                response.Success = false;
                response.Message = NothingToAdd;

                return response;
            }

            try
            {
                var entity = Mapper.Map<TEntity>(request);
                var entryResult = AppDbContext.Set<TEntity>().Add(entity);

                await AppDbContext.SaveChangesAsync();

                if (entryResult.Entity is not EntityModelBase entityModelBase || entityModelBase.Id == 0)
                {
                    response.Success = false;
                    response.Message = RecordNotAdded;

                    return response;
                }

                response.RecordId = entityModelBase.Id;
                response.Result = Mapper.Map<TRequest>(entryResult.Entity);
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Deletes a record by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<IServiceResponse<int>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<int>();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = NothingToDelete;

                return response;
            }

            try
            {
                var entity = await AppDbContext.Set<TEntity>().FindAsync(id);

                if (entity is null)
                {
                    response.Success = false;
                    response.Message = NotFound;

                    return response;
                }

                AppDbContext.Set<TEntity>().Remove(entity);

                response.Result = await AppDbContext.SaveChangesAsync();
                response.RecordId = id;
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Gets a record by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<IServiceResponse<TRequest>> GetAsync(int id)
        {
            var response = new ServiceResponse<TRequest>();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = NothingToGet;

                return response;
            }

            try
            {
                var entity = await AppDbContext.Set<TEntity>().FindAsync(id);

                if (entity is null)
                {
                    response.Success = false;
                    response.Message = NotFound;

                    return response;
                }

                response.Result = Mapper.Map<TRequest>(entity);
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Gets a list of records
        /// </summary>
        /// <returns></returns>
        public virtual IServiceResponse<List<TRequest>> GetList(Func<TEntity, bool> query = null)
        {
            var response = new ServiceResponse<List<TRequest>>();

            try
            {
                var entities = query is null ? AppDbContext.Set<TEntity>() : AppDbContext.Set<TEntity>().Where(query);

                response.Result = Mapper.Map<List<TRequest>>(entities.ToList());
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Updates a record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<IServiceResponse<TRequest>> UpdateAsync(int id, TRequest request)
        {
            var response = new ServiceResponse<TRequest>();

            if (request is null || id <= 0)
            {
                response.Success = false;
                response.Message = id <= 0 ? NotUpdateable : NothingToModify;

                return response;
            }

            try
            {
                var entity = Mapper.Map<TEntity>(request);

                AppDbContext.Set<TEntity>().Update(entity);

                await AppDbContext.SaveChangesAsync();

                response.RecordId = id;
                response.Result = Mapper.Map<TRequest>(entity);
            }
            catch (Exception exception)
            {
                HandleServiceResponseException(response, exception);
            }

            return response;
        }

        /// <summary>
        ///     Handle the IServiceResponse exception data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="exception"></param>
        protected void HandleServiceResponseException<T>(IServiceResponse<T> response, Exception exception)
        {
            response.Success = false;
            response.Message = exception.Message;
            response.Exception = exception;

            Logger.LogError(exception, exception.Message);
        }
    }
}