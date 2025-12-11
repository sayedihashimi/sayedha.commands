using System;
namespace SayedHa.Commands.Shared {

    [Serializable]
    public class MissingParameterException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MissingParameterException"/> class
        /// </summary>
        public MissingParameterException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MissingParameterException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public MissingParameterException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MissingParameterException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public MissingParameterException(string message, System.Exception inner) : base(message, inner) {
        }
    }
}
