// -----------------------------------------------------------------------
//  <copyright file="CollisionEventServiceTests.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NeuraspaceTest;
using NeuraspaceTest.DataAccess;
using NeuraspaceTest.Models;
using NeuraspaceTest.Models.DataTransferModels;
using NeuraspaceTest.Services;

namespace UnitTests
{
    [TestClass]
    public class CollisionEventServiceTests
    {
        #region

        private AppDbContext _appDbContext;
        private Operator _testOperator;
        private Satellite _testSatellite;

        #endregion

        [TestMethod]
        public void AddMessageTest_AddDuplicateMessage_NotSuccess()
        {
            // Arrange
            var service = GetService();
            var messageData = GetMessageData();

            service.AddAsync(messageData).Wait();

            // Act
            var response = service.AddAsync(messageData).Result;

            // Assert
            Assert.IsTrue(response != null);
            Assert.IsFalse(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Message));
        }

        [TestMethod]
        public void AddMessageTest_NotValidProbabilityOfCollision_NotSuccess()
        {
            // Arrange
            var service = GetService();
            var messageData = GetMessageData();

            messageData.ProbabilityOfCollision = 1.5;

            // Act
            var response = service.AddAsync(messageData).Result;

            // Assert
            Assert.IsTrue(response != null);
            Assert.IsFalse(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Message));
        }

        [TestMethod]
        public void AddMessageTest_ValidMessage_Success()
        {
            // Arrange
            var service = GetService();
            var messageData = GetMessageData();

            // Act
            var response = service.AddAsync(messageData).Result;

            // Assert
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.Message));
        }

        /// <summary>
        ///     Cleanups test pre-required data.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.Dispose();
        }

        /// <summary>
        ///     Prepares the test pre-requirements.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ApplicationDatabase")
                .Options;

            _appDbContext = new AppDbContext(options);

            _testOperator = new Operator
            {
                OperatorId = "op-Test001",
                Name = "Test Operator 001"
            };

            _testSatellite = new Satellite
            {
                Operator = _testOperator,
                SatelliteId = "sat-Test001",
                Name = "Test Sat 001"
            };

            _appDbContext.Operators.Add(_testOperator);
            _appDbContext.Satellites.Add(_testSatellite);
            _appDbContext.SaveChanges();
        }

        [TestMethod]
        public void ValidateMessageTest_DateInThePast_ReturnsFalse()
        {
            // Arrange
            var response = new ServiceResponse<CollisionEventData>();
            var service = GetService();
            var message = GetValidMessage();

            message.CollisionDate = DateTime.Now.ToUniversalTime().AddDays(-1);

            // Act
            var result = service.ValidateMessage(response, message, message.Operator.OperatorId);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Message));
        }

        [TestMethod]
        public void ValidateMessageTest_NotFromOperator_NotSuccess()
        {
            // Arrange
            var response = new ServiceResponse<CollisionEventData>();
            var service = GetService();
            var message = GetValidMessage();

            // Act
            var result = service.ValidateMessage(response, message, "op-0003");

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Message));
        }

        [TestMethod]
        public void ValidateMessageTest_ValidMessage_Success()
        {
            // Arrange
            var response = new ServiceResponse<CollisionEventData>();
            var service = GetService();
            var message = GetValidMessage();

            // Act
            var result = service.ValidateMessage(response, message, message.Operator.OperatorId);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.Message));
        }

        private CollisionEventData GetMessageData()
        {
            return new CollisionEventData
            {
                OperatorId = _testOperator.OperatorId,
                SatelliteId = _testSatellite.SatelliteId,
                CollisionDate = DateTime.Now.ToUniversalTime().AddDays(1),
                ProbabilityOfCollision = 0.7,
                ChaserObjectId = "00000",
                EventId = "111111",
                MessageId = "222222"
            };
        }

        private CollisionEventService GetService()
        {
            var logger = new Mock<ILogger<CollisionEventService>>();
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperConfiguration>(); });
            var mapper = configuration.CreateMapper();

            return new CollisionEventService(logger.Object, mapper, _appDbContext);
        }

        private CollisionEvent GetValidMessage()
        {
            return new CollisionEvent
            {
                Operator = _testOperator,
                Satellite = _testSatellite,
                CollisionDate = DateTime.Now.ToUniversalTime().AddDays(1),
                ProbabilityOfCollision = 0.7,
                ChaserObjectId = "00000",
                EventId = "111111",
                MessageId = "222222"
            };
        }
    }
}