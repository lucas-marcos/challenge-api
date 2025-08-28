using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly List<Notification> _notifications = new();

    public void AddNotification(string message, string type = "Error")
    {
        _notifications.Add(new Notification
        {
            Message = message,
            Type = type,
            Timestamp = DateTime.UtcNow
        });
    }

    public bool HasNotifications()
    {
        return _notifications.Any();
    }

    public List<Notification> GetNotifications()
    {
        return _notifications.ToList();
    }

    public void ClearNotifications()
    {
        _notifications.Clear();
    }
}
