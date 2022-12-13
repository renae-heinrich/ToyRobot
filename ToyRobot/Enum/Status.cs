using System.ComponentModel;

namespace ToyRobot.Enum
{
    public enum Status
    {
        [Description("Error")]
        Error,
        [Description("OK")]
        Ok,
        [Description("Occupied")]
        Occupied,
    }
}

