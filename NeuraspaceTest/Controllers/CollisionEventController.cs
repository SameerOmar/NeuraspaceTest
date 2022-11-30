// -----------------------------------------------------------------------
//  <copyright file="CollisionEventController.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using NeuraspaceTest.Contracts.Services;
using NeuraspaceTest.Models;
using NeuraspaceTest.Models.DataTransferModels;

namespace NeuraspaceTest.Controllers
{
    /// <summary>
    ///     Collision Event Controller class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CollisionEventController : EntityControllerBase<CollisionEventData, CollisionEvent>
    {
        #region

        private readonly ICollisionEventService<CollisionEventData, CollisionEvent> _collisionEventService;

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="CollisionEventController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="entityService">The entity service.</param>
        public CollisionEventController(ILogger<CollisionEventController> logger,
            ICollisionEventService<CollisionEventData, CollisionEvent> entityService) : base(logger, entityService)
        {
            _collisionEventService = entityService;
        }

        /// <summary>
        ///     Cancels a Collision event message.
        /// </summary>
        /// <param name="messageId">The message id to be canceled.</param>
        /// <returns></returns>
        [HttpDelete("{messageId}")]
        public async Task<IActionResult> CancelCollisionEvent(string messageId)
        {
            if (string.IsNullOrWhiteSpace(messageId) || !ValidateOperatorHeader())
            {
                return BadRequest();
            }

            var operatorId = Request.Headers["operator_id"].FirstOrDefault();
            var result = await _collisionEventService.CancelMessageAsync(messageId, operatorId!);

            return result.Success ? Ok() : Problem(result.Message);
        }

        /// <summary>
        ///     Gets a collision events list.
        /// </summary>
        /// <param name="all"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CollisionEventData>> GetCollisionEvents(bool all = false)
        {
            if (!ValidateOperatorHeader())
            {
                return BadRequest();
            }

            var operatorId = Request.Headers["operator_id"].FirstOrDefault();

            if (all)
            {
                return GetEntities(query => query.Operator.OperatorId == operatorId);
            }

            return GetEntities(query =>
                query.Operator.OperatorId == operatorId && query.CollisionDate >= DateTime.Now.ToUniversalTime());
        }

        /// <summary>
        ///     Gets a list of collision events warnings.
        /// </summary>
        /// <param name="all"></param>
        /// <returns></returns>
        [HttpGet("Warnings")]
        public ActionResult<IEnumerable<CollisionEventData>> GetCollisionEventWarnings(bool all = false)
        {
            if (!ValidateOperatorHeader())
            {
                return BadRequest();
            }

            var operatorId = Request.Headers["operator_id"].FirstOrDefault();
            var result = _collisionEventService.GetCollisionEventWarnings(operatorId, all);

            if (result.Success)
            {
                return result.Result;
            }

            return Problem(result.Message);
        }

        /// <summary>
        ///     Adds a collision event.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CollisionEventData>> PostCollisionEvent(CollisionEventData data)
        {
            if (!ValidateOperatorHeader(data))
            {
                return BadRequest();
            }

            return await AddEntityAsync(data);
        }

        /// <summary>
        ///     Validates the operator header.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private bool ValidateOperatorHeader(CollisionEventData data = null)
        {
            var operatorId = Request.Headers["operator_id"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(operatorId))
            {
                return false;
            }

            if (data is not null && data.OperatorId != operatorId)
            {
                return false;
            }

            return true;
        }
    }
}