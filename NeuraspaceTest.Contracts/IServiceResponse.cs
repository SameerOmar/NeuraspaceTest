// -----------------------------------------------------------------------
//  <copyright file="IServiceResponse.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace NeuraspaceTest.Contracts
{
    public interface IServiceResponse<T>
    {
        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        string Error { get; set; }

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        ///     Gets or sets the extra message.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     Gets or sets the record identifier.
        /// </summary>
        int RecordId { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        T Result { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether call is success.
        /// </summary>
        bool Success { get; set; }
    }
}