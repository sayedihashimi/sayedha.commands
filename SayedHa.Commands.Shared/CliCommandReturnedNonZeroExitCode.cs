using System;
namespace SayedHa.Commands.Shared {

    [Serializable]
    public class CliCommandReturnedNonZeroExitCodeException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CliCommandReturnedNonZeroExitCodeException"/> class
        /// </summary>
        public CliCommandReturnedNonZeroExitCodeException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CliCommandReturnedNonZeroExitCodeException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public CliCommandReturnedNonZeroExitCodeException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CliCommandReturnedNonZeroExitCodeException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public CliCommandReturnedNonZeroExitCodeException(string message, System.Exception inner) : base(message, inner) {
        }
    }
}
