﻿using System;
namespace SayedHa.Commands.Shared.Extensions {

    [Serializable]
    public class FileExistsException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FileExistsException"/> class
        /// </summary>
        public FileExistsException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FileExistsException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public FileExistsException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FileExistsException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public FileExistsException(string message, System.Exception inner) : base(message, inner) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FileExistsException"/> class
        /// </summary>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected FileExistsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
        }
    }
}
