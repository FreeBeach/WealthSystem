namespace FrameMVC.Interfaces
{
    public interface INotification
    {
        string Name { get; }
        object Body { get; set; }
        string Type { get; set; }
        string ToString();
    }
}
