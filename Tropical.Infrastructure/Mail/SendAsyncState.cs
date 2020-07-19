using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tropical.Infrastructure.Mail
{
    /// <summary>
    /// User defined async state for SendEmailAsync method
    /// </summary>
    public class SendAsyncState
    {

        /// <summary>
        /// Contains all info that you need while handling message result
        /// </summary>
        public ModeloEmail EmailMessageInfo { get; private set; }


        public SendAsyncState(ModeloEmail emailMessageInfo)
        {
            EmailMessageInfo = emailMessageInfo;
        }
    }
}
