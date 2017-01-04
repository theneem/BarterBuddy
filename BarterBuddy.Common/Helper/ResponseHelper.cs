using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Common.Helper
{
    /// <summary>
    /// ResponseHelper entity
    /// </summary>
    public class ResponseHelper
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public Enums.ResponseCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the authentication code.
        /// </summary>
        /// <value>The authentication code.</value>
        public string AuthenticationCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public object Payload { get; set; }
    }
}
