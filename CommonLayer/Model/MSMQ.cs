using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQ
    {
        MessageQueue MessageQ = new MessageQueue();

        public void sendData2Queue(String token)
        {
            MessageQ.Path = @".\private$\token";
            if(!MessageQueue.Exists(MessageQ.Path))
            {               
                MessageQueue.Create(MessageQ.Path);
            }
            
            MessageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MessageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;  //Delegate
            MessageQ.Send(token);
            MessageQ.BeginReceive();
            MessageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = MessageQ.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "Fundoo Notes App Reset Link";
                string body = token;
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("hemsdemo10@gmail.com", "abgkmpmdeaspajbb"),
                    EnableSsl = true

                };
                SMTP.Send("hemsdemo10@gmail.com", "hemsdemo10@gmail.com", subject, body);
                // Process the logic be sending the message
                //Restart the asynchronous receive operation.
                MessageQ.BeginReceive();
            }
            catch (MessageQueueException qexception)
            {
            }
        }
    }
}
