using System.ComponentModel;

namespace ToyRobot
{
    public enum GridStatus
    {
        [Description("Error")]
        Error,
        [Description("OK")]
        Ok,
        [Description("Occupied")]
        Occupied,
    }

    public class GridS
    {
        public GridStatus GridStatus { get; set; }
        public string Message { get; set; }
    }
}

