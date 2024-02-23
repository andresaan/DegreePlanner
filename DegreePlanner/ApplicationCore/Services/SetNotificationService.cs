using Plugin.LocalNotification;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class SetNotificationService : ISetNotificationService
    {
        public async Task SetNotification(string title, string description, DateTime time)
        {
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = title,
                Description = description,
                Schedule =
                {
                    NotifyTime = time
                }
            };

            await LocalNotificationCenter.Current.Show(notification);
        }
    }
}
