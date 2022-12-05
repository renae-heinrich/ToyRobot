using System.ComponentModel;

namespace ToyRobot
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

