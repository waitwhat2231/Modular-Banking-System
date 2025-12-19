using Common.SharedClasses.Dtos.Users;
using Common.SharedClasses.Services;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;

namespace Modules.Users.Application.Services;

public class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    public async Task SendNotificationAsync(List<DeviceDto> devices, string title, string body, string type)
    {
        foreach (var device in devices)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = title,
                    Body = body,
                },
                Data = new Dictionary<string, string>()
                {
                    { "title", title ?? "" },
                    { "body", body ?? "" },
                    { "createdAt", DateTime.UtcNow.ToString() ?? "" },
                    { "type", nameof(type) }
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        Sound = "default",
                        ChannelId = "high_importance_channel",
                        ClickAction = "FLUTTER_NOTIFICATION_CLICK"
                    }
                },
                Token = device.FcmToken,
            };
            try
            {
                var messaging = FirebaseMessaging.DefaultInstance;
                var result = await messaging.SendAsync(message);
            }
            catch (FirebaseAdmin.Messaging.FirebaseMessagingException ex)
            {
                logger.LogInformation($"NOTIFICATION ERROR BY MAJD: {ex.Message}");
            }
        }
    }
}
