// -----------------------------------------------------------------------
//  <copyright file="ServiceResponse.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using NeuraspaceTest.Contracts;

namespace NeuraspaceTest.Services
{
    /// <summary>
    ///     Generic wrapper for web api service response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T> : IServiceResponse<T>
    {
        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        public string Error { get; set; } = null;

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        ///     Gets or sets the extra message.
        /// </summary>
        public string Message { get; set; } = null;

        /// <summary>
        ///     Gets or sets the record identifier.
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether call is success.
        /// </summary>
        public bool Success { get; set; } = true;
    }
}