using FrameMVC.Interfaces;

namespace FrameMVC.Patterns
{
    public class Notification : INotification
    {
        public string Name { get; }
        public object Body { get; set; }
        public string Type { get; set; }
        public Notification(string name, object body = null, string type = null)
        {
            Name = name;
            Body = body;
            Type = type;
        }
        public override string ToString()
        {
            var msg = "Notification Name: " + Name;
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            msg += "\nType:" + ((Type == null) ? "null" : Type);
            return msg;
        }

    }
}
