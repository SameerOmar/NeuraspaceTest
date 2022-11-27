// -----------------------------------------------------------------------
//  <copyright file="SatelliteController.cs" company="Excerya">
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
    ///     Satellite Controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SatelliteController : EntityControllerBase<SatelliteData, Satellite>
    {
        #region

        private readonly ISatelliteService<SatelliteData, Satellite> _satelliteService;

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="SatelliteController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="entityService">The entity service.</param>
        public SatelliteController(ILogger<SatelliteController> logger,
            ISatelliteService<SatelliteData, Satellite> entityService) : base(logger, entityService)
        {
            _satelliteService = entityService;
        }

        /// <summary>
        ///     Gets the satellite by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SatelliteData>> GetSatellite(int id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        ///     Gets the satellites.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SatelliteData>>> GetSatellites()
        {
            return await GetEntitiesAsync();
        }

        /// <summary>
        ///     Adds the satellite.
        /// </summary>
        /// <param name="satelliteData">The satellite data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<SatelliteData>> PostSatellite(SatelliteData satelliteData)
        {
            if (!ValidateOperatorHeader(satelliteData))
            {
                return BadRequest();
            }

            return await AddEntityAsync(satelliteData);
        }

        /// <summary>
        ///     Updates the satellite.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="satelliteData">The satellite data.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSatellite(int id, SatelliteData satelliteData)
        {
            if (!ValidateOperatorHeader(satelliteData))
            {
                return BadRequest();
            }

            return await UpdateEntityAsync(id, satelliteData);
        }

        /// <summary>
        ///     Validates the operator header.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private bool ValidateOperatorHeader(SatelliteData data = null)
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