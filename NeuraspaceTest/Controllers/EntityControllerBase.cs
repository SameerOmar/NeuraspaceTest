// -----------------------------------------------------------------------
//  <copyright file="EntityControllerBase.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using NeuraspaceTest.Contracts;
using NeuraspaceTest.Contracts.Services;

namespace NeuraspaceTest.Controllers
{
    /// <summary>
    ///     Entity Controller base class
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class EntityControllerBase<TRequest, TEntity> : ControllerBase
        where TRequest : class
        where TEntity : class
    {
        #region

        /// <summary>
        ///     The Entity Service.
        /// </summary>
        protected readonly IEntityServiceBase<TRequest, TEntity> EntityService;

        /// <summary>
        ///     The logger.
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        ///     Not found message constant.
        /// </summary>
        protected const string NotFoundMessage = "Not found";

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityControllerBase{TRequest, TEntity}" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="entityService">The entity service.</param>
        public EntityControllerBase(ILogger logger,
            IEntityServiceBase<TRequest, TEntity> entityService)
        {
            Logger = logger;
            EntityService = entityService;
        }

        /// <summary>
        ///     Adds the entity asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<ActionResult<TRequest>> AddEntityAsync(TRequest request)
        {
            var response = await EntityService.AddAsync(request);

            if (!response.Success)
            {
                return Problem(response.Message);
            }

            return response.Result;
        }

        /// <summary>
        ///     Deletes the entity asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<IActionResult> DeleteEntityAsync(int id)
        {
            var response = await EntityService.DeleteAsync(id);

            if (response.Success)
            {
                return Ok();
            }

            if (response.Message == NotFoundMessage)
            {
                return NotFound();
            }

            return Problem(response.Message);
        }

        /// <summary>
        ///     Gets the entities asynchronous.
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<ActionResult<IEnumerable<TRequest>>> GetEntitiesAsync(Func<TEntity, bool> query = null)
        {
            var response = EntityService.GetList(query);

            if (response.Success)
            {
                return response.Result;
            }

            return Problem(response.Message);
        }

        /// <summary>
        ///     Gets the entity asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<ActionResult<TRequest>> GetEntityAsync(int id)
        {
            var response = await EntityService.GetAsync(id);

            if (response.Success)
            {
                return response.Result;
            }

            if (response.Message == NotFoundMessage)
            {
                return NotFound();
            }

            return Problem(response.Message);
        }

        /// <summary>
        ///     Updates the entity asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<IActionResult> UpdateEntityAsync(int id, TRequest request)
        {
            if (id <= 0 || request is not IEntityRequestBase requestBase)
            {
                return BadRequest();
            }

            requestBase.Id = id;

            var response = await EntityService.UpdateAsync(id, request);

            if (response.Success)
            {
                return Ok();
            }

            if (response.Message == NotFoundMessage)
            {
                return NotFound();
            }

            return Problem(response.Message);
        }
    }
}