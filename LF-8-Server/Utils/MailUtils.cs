using MailKit.Net.Smtp;
using MimeKit;

namespace LF_8_Server.Utils
{
	internal class MailUtils
	{
		public static void SendMail(string subject, string body, double retryAmount = 0)
		{
			try
			{
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Leon", "mistercoolertyper@mail.wagnerraid.com"));
                message.To.Add(new MailboxAddress("Leon Wagner", "leonwagner09@outlook.de"));
                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.wagnerraid.com", 587, false);
                    client.Authenticate("mistercoolertyper@mail.wagnerraid.com", "Leon231207");

                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine("Mail sent");
			} catch
			{
				Console.WriteLine("failed to send mail");
				if (retryAmount < 5)
				{
					Thread.Sleep(2000);
					SendMail(subject, body, retryAmount + 1);
				}
			}
		}
	}
}
