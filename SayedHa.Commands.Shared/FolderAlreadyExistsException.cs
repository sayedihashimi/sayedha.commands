using System;
namespace SayedHa.Commands.Shared.Exceptions{

    [Serializable]
    public class FolderAlreadyExistsException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FolderAlreadyExistsException"/> class
        /// </summary>
        public FolderAlreadyExistsException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FolderAlreadyExistsException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public FolderAlreadyExistsException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FolderAlreadyExistsException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public FolderAlreadyExistsException(string message, System.Exception inner) : base(message, inner) {
        }
    }
}
