using System;
namespace SayedHa.Commands.Shared.Exceptions {

    [Serializable]
    public class RecursionLimitReachedException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecursionLimitReachedException"/> class
        /// </summary>
        public RecursionLimitReachedException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecursionLimitReachedException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public RecursionLimitReachedException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecursionLimitReachedException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public RecursionLimitReachedException(string message, System.Exception inner) : base(message, inner) {
        }
    }
}
