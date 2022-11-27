// -----------------------------------------------------------------------
//  <copyright file="OperatorController.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using NeuraspaceTest.Contracts.Services;
using NeuraspaceTest.DataTransferModels;
using NeuraspaceTest.Models;

namespace NeuraspaceTest.Controllers
{
    /// <summary>
    ///     Operator Controller class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : EntityControllerBase<OperatorData, Operator>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OperatorController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="entityService">The entity service.</param>
        public OperatorController(ILogger<OperatorController> logger,
            IOperatorService<OperatorData, Operator> entityService) : base(logger, entityService)
        {
        }

        /// <summary>
        ///     Gets the operator by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OperatorData>> GetOperator(int id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        ///     Gets the operators.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperatorData>>> GetOperators()
        {
            return await GetEntitiesAsync();
        }

        /// <summary>
        ///     Adds the operator.
        /// </summary>
        /// <param name="operatorData">The operator request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OperatorData>> PostOperator(OperatorData operatorData)
        {
            return await AddEntityAsync(operatorData);
        }

        /// <summary>
        ///     Updates the operator.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="operatorData">The operator request.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperator(int id, OperatorData operatorData)
        {
            return await UpdateEntityAsync(id, operatorData);
        }
    }
}