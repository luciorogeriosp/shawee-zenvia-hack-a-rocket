using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Tropical.Infrastructure.Util;

namespace Tropical.Infrastructure.Mail
{
    public abstract class SendMail
    {
        #region Properties

        protected string Subject { get; set; }
        protected string Body { get; set; }
        protected List<ModeloEmail> EmailTo { get; set; }

        private readonly string emailFrom = ParameterCollection.GetParamValue("emailFrom");
        private readonly string emailFromName = ParameterCollection.GetParamValue("emailFromName");
        private readonly string smtpUsername = ParameterCollection.GetParamValue("emailSmtpUser");
        private readonly string smtpPassword = ParameterCollection.GetParamValue("emailSmtpPass");
        private readonly string smtpHost = ParameterCollection.GetParamValue("emailSmtp");
        private readonly int smtpPort = int.Parse(ParameterCollection.GetParamValue("emailsmtpPort"));

        #endregion

        #region Constructor

        public SendMail()
        {
            this.EmailTo = new List<ModeloEmail>();
        }

        #endregion

        #region Private Metodos

        protected void EnviaSincrono()
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailFrom, emailFromName);
            message.Subject = Subject;
            message.Body = Body;

            for (int i = 0; i < EmailTo.Count; i++)
			{
			    message.To.Add(new MailAddress(EmailTo[i].Endereco, EmailTo[i].Nome));
			}

            SmtpClient smtp = new SmtpClient
            {
                Host = smtpHost,
                Port = smtpPort
            };            

            smtp.Send(message);

        }

        protected void EnviaAssincrono()
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailFrom, emailFromName);
            message.Subject = Subject;
            message.Body = Body;

            SmtpClient smtp = new SmtpClient
            {
                Host = smtpHost,
                Port = smtpPort
            };

            for (int i = 0; i < EmailTo.Count; i++)
            {
                smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                message.To.Add(new MailAddress(this.EmailTo[i].Endereco, this.EmailTo[i].Nome));
                smtp.SendAsync(message, this.EmailTo[i]);
            }
        }

        private void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SmtpClient smtpClient = (SmtpClient)sender;
            ModeloEmail userAsyncState = (ModeloEmail)e.UserState;
            smtpClient.SendCompleted -= smtp_SendCompleted;

            if (e.Error != null)
            {
                string endereco = userAsyncState.Endereco;
            }
        }

        #endregion
    }
}
