﻿using MimeKit;

namespace AppDiv.SmartAgency.Utility.Services
{
    public interface IMailService
    {
        bool TrySetDelivered(int messageId);
        Task<bool> SendAsync(string textMessage, string subject, string senderMailAddress, IEnumerable<string> receiversMailAddress, CancellationToken cancellationToken);
        Task<bool> SendAsync(string body, string subject, string senderMailAddress, string receiver, CancellationToken cancellationToken);
        Task<bool> SendAsync(string textMessage, string subject, string fileName, byte[] attachment, string senderMailAddress, IEnumerable<string> receiversMailAddress, CancellationToken cancellationToken);
        Task<bool> SendAsync(string textMessage, string subject, string senderMailAddress, IEnumerable<string> receiversMailAddress, IEnumerable<string> carbonCopyReceiversAddress, CancellationToken cancellationToken);
        Task<bool> SendAsync(string textMessage, string subject, string fileName, byte[] attachment, string senderMailAddress, IEnumerable<string> receiversMailAddress, IEnumerable<string> carbonCopyReceiversAddress, CancellationToken cancellationToken);
        Task<bool> SendAsync(MimeMessage message, CancellationToken cancellationToken);
    }
}
