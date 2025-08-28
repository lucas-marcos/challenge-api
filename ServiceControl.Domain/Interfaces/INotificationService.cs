namespace ServiceControl.Domain.Interfaces;

public interface INotificationService
{
    void AddNotification(string message, string type = "Error");
    bool HasNotifications();
    List<Notification> GetNotifications();
    void ClearNotifications();
}

public class Notification
{
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Error";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
