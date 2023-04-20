using AppDiv.CRVS.Utility.Config;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AppDiv.CRVS.Utility.Services
{
    public class MailKitService : IMailService
    {
        private readonly SMTPServerConfiguration _config;
        private readonly object _locker = new object();
        private readonly Dictionary<int, DateTime> _messageDeliveryTime = new Dictionary<int, DateTime>();

        public MailKitService(IOptions<SMTPServerConfiguration> config) => _config = config.Value;

        public bool TrySetDelivered(int messageId)
        {

            DateTime deliveredTime;
            if (!_messageDeliveryTime.TryGetValue(messageId, out deliveredTime) || deliveredTime.AddSeconds(_config.TIMEOUT_IN_MS) <= DateTime.UtcNow)
            {
                lock (_locker)
                {
                    if (!_messageDeliveryTime.TryGetValue(messageId, out deliveredTime) || deliveredTime.AddSeconds(_config.TIMEOUT_IN_MS) <= DateTime.UtcNow)
                    {
                        _messageDeliveryTime[messageId] = DateTime.UtcNow;
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> SendAsync(string textMessage, string subject, string senderMailAddress, IEnumerable<string> receiversMailAddress, CancellationToken cancellationToken)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("", senderMailAddress);
            message.From.Add(from);

            if (receiversMailAddress.Any())
            {
                message.To.AddRange(from mailAddress in receiversMailAddress select new MailboxAddress("", mailAddress));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<div>{textMessage}</div>"; 
                bodyBuilder.TextBody = textMessage;

                //The ToMessageBody() method creates a message body with HTML/Text content and attachments.
                message.Body = bodyBuilder.ToMessageBody();

                //Connect and authenticate with the SMTP server
                using (SmtpClient client = new SmtpClient())
                {
                    client.Timeout = this._config.TIMEOUT_IN_MS;
                    await client.ConnectAsync(this._config.SMTP_HOST_ADDRESS, port: this._config.USE_SSL ? this._config.PORT_SSL_OR_TLS : this._config.PORT_NON_SSL_OR_TLS, this._config.USE_SSL, cancellationToken);
                    await client.AuthenticateAsync(this._config.USER_NAME, this._config.SECRET);
                    await client.SendAsync(message, cancellationToken);
                    await client.DisconnectAsync(true, cancellationToken);
                }
                return true;
            }
            else throw new ArgumentNullException("receiversMailAddress");
        }

        public async Task<bool> SendAsync(string body, string subject, string senderMailAddress, string receiver, CancellationToken cancellationToken)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("", senderMailAddress);
            message.From.Add(from);

            if (receiver.Any())
            {
                message.To.Add(new MailboxAddress("", receiver));
                //message.To.AddRange(from mailAddress in receivers select new MailboxAddress("", mailAddress));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<div>{body}</div>";
                bodyBuilder.TextBody = body;

                //The ToMessageBody() method creates a message body with HTML/Text content and attachments.
                message.Body = bodyBuilder.ToMessageBody();

                //Connect and authenticate with the SMTP server
                using (SmtpClient client = new SmtpClient())
                {
                    client.Timeout = this._config.TIMEOUT_IN_MS;
                    await client.ConnectAsync(this._config.SMTP_HOST_ADDRESS, port: this._config.USE_SSL ? this._config.PORT_SSL_OR_TLS : this._config.PORT_NON_SSL_OR_TLS, this._config.USE_SSL, cancellationToken);
                    await client.AuthenticateAsync(this._config.USER_NAME, this._config.SECRET);
                    await client.SendAsync(message, cancellationToken);
                    await client.DisconnectAsync(true, cancellationToken);
                }
                return true;
            }
            else throw new ArgumentNullException("receiversMailAddress");
        }

        public async Task<bool> SendAsync(string textMessage, string subject, string fileName, byte[] attachment, string senderMailAddress, IEnumerable<string> receiversMailAddress, CancellationToken cancellationToken)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("", senderMailAddress);
            message.From.Add(from);

            if (receiversMailAddress.Any())
            {
                message.To.AddRange(from mailAddress in receiversMailAddress select new MailboxAddress("", mailAddress));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<div>{textMessage}</div>";
                bodyBuilder.TextBody = textMessage;
                bodyBuilder.Attachments.Add(fileName, attachment);
                // If you find that MimeKit does not properly auto-detect the mime-type based on the
                // filename, you can specify a mime-type like this:
                //bodyBuilder.Attachments.Add ("textMessage", attachment, "mime-type of the file");

                //The ToMessageBody() method creates a message body with HTML/Text content and attachments.
                message.Body = bodyBuilder.ToMessageBody();

                //Connect and authenticate with the SMTP server
                using (SmtpClient client = new SmtpClient())
                {
                    client.Timeout = this._config.TIMEOUT_IN_MS;
                    await client.ConnectAsync(this._config.SMTP_HOST_ADDRESS, port: this._config.USE_SSL ? this._config.PORT_SSL_OR_TLS : this._config.PORT_NON_SSL_OR_TLS, this._config.USE_SSL, cancellationToken);
                    await client.AuthenticateAsync(this._config.USER_NAME, this._config.SECRET);
                    await client.SendAsync(message, cancellationToken);
                    await client.DisconnectAsync(true, cancellationToken);
                }
                return true;
            }
            else throw new ArgumentNullException("receiversMailAddress");
        }

        public async Task<bool> SendAsync(string textMessage, string subject, string senderMailAddress, IEnumerable<string> receiversMailAddress, IEnumerable<string> carbonCopyReceiversAddress, CancellationToken cancellationToken)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("", senderMailAddress);
            message.From.Add(from);

            if (carbonCopyReceiversAddress.Any())
            {
                message.Cc.AddRange(from mailAddress in carbonCopyReceiversAddress select new MailboxAddress("", mailAddress));
            }

            if (receiversMailAddress.Any())
            {
                message.To.AddRange(from mailAddress in receiversMailAddress select new MailboxAddress("", mailAddress));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<div>{textMessage}</div>";
                bodyBuilder.TextBody = textMessage;

                //The ToMessageBody() method creates a message body with HTML/Text content and attachments.
                message.Body = bodyBuilder.ToMessageBody();

                //Connect and authenticate with the SMTP server
                using (SmtpClient client = new SmtpClient())
                {
                    client.Timeout = this._config.TIMEOUT_IN_MS;
                    await client.ConnectAsync(this._config.SMTP_HOST_ADDRESS, port: this._config.USE_SSL ? this._config.PORT_SSL_OR_TLS : this._config.PORT_NON_SSL_OR_TLS, this._config.USE_SSL, cancellationToken);
                    await client.AuthenticateAsync(this._config.USER_NAME, this._config.SECRET);
                    await client.SendAsync(message, cancellationToken);
                    await client.DisconnectAsync(true, cancellationToken);
                }
                return true;
            }
            else throw new ArgumentNullException("receiversMailAddress");
        }

        public async Task<bool> SendAsync(string textMessage, string subject, string fileName, byte[] attachment, string senderMailAddress, IEnumerable<string> receiversMailAddress, IEnumerable<string> carbonCopyReceiversAddress, CancellationToken cancellationToken)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("", senderMailAddress);
            message.From.Add(from);

            if (carbonCopyReceiversAddress.Any())
            {
                message.Cc.AddRange(from mailAddress in carbonCopyReceiversAddress select new MailboxAddress("", mailAddress));
            }

            if (receiversMailAddress.Any())
            {
                message.To.AddRange(from mailAddress in receiversMailAddress select new MailboxAddress("", mailAddress));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<div>{textMessage}</div>";
                bodyBuilder.TextBody = textMessage;
                bodyBuilder.Attachments.Add(fileName, attachment);
                // If you find that MimeKit does not properly auto-detect the mime-type based on the
                // filename, you can specify a mime-type like this:
                //bodyBuilder.Attachments.Add ("textMessage", attachment, "mime-type of the file");

                //The ToMessageBody() method creates a message body with HTML/Text content and attachments.
                message.Body = bodyBuilder.ToMessageBody();

                //Connect and authenticate with the SMTP server
                using (SmtpClient client = new SmtpClient())
                {
                    client.Timeout = this._config.TIMEOUT_IN_MS;
                    await client.ConnectAsync(this._config.SMTP_HOST_ADDRESS, port: this._config.USE_SSL ? this._config.PORT_SSL_OR_TLS : this._config.PORT_NON_SSL_OR_TLS, this._config.USE_SSL, cancellationToken);
                    await client.AuthenticateAsync(this._config.USER_NAME, this._config.SECRET);
                    await client.SendAsync(message, cancellationToken);
                    await client.DisconnectAsync(true, cancellationToken);
                }
                return true;
            }
            else throw new ArgumentNullException("receiversMailAddress");
        }

        public async Task<bool> SendAsync(MimeMessage message, CancellationToken cancellationToken)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Timeout = this._config.TIMEOUT_IN_MS;
                await client.ConnectAsync(this._config.SMTP_HOST_ADDRESS, port: this._config.USE_SSL ? this._config.PORT_SSL_OR_TLS : this._config.PORT_NON_SSL_OR_TLS, this._config.USE_SSL, cancellationToken);
                await client.AuthenticateAsync(this._config.USER_NAME, this._config.SECRET, cancellationToken);
                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
            return true;
        }


    }
}
