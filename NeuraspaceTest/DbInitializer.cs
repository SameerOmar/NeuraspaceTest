// -----------------------------------------------------------------------
//  <copyright file="DbInitializer.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using NeuraspaceTest.DataAccess;
using NeuraspaceTest.Models;

namespace NeuraspaceTest
{
    /// <summary>
    ///     Database initializer class
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        ///     Initializes the specified database context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Operators.Any())
            {
                return;
            }

            for (var op = 1; op <= 10; op++)
            {
                var operatorRecord = new Operator
                {
                    OperatorId = $"op-{op:0000}",
                    Name = $"Operator {op:0000}"
                };

                context.Operators.Add(operatorRecord);

                for (var sat = 1; sat <= 5; sat++)
                {
                    var satelliteId = op * 100 + sat;
                    var satelliteRecord = new Satellite
                    {
                        Operator = operatorRecord,
                        SatelliteId = $"sat-{satelliteId:0000}",
                        Name = $"Satellite {satelliteId:0000}"
                    };

                    context.Satellites.Add(satelliteRecord);

                    for (var msg = 1; msg <= 10; msg++)
                    {
                        var messageId = satelliteId * 10000 + msg;
                        var messageRecord = new CollisionEvent
                        {
                            Operator = operatorRecord,
                            Satellite = satelliteRecord,
                            ProbabilityOfCollision = GetRandomNumber(0, 1),
                            ChaserObjectId = "xyz",
                            EventId = $"evt-{messageId}",
                            CollisionDate = DateTime.Now.ToUniversalTime().AddDays(msg - 3),
                            MessageId = $"{messageId}"
                        };

                        context.CollisionEvents.Add(messageRecord);
                    }
                }
            }

            context.SaveChanges();
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}