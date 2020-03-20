using System.Windows.Shapes;

namespace QLearning.Core
{
    public class QState
    {
        public int Index { get; set; }
        public Rectangle UiItem { get; set; }
        public (int X, int Y) Coordinations { get; set; }
        public StateType Type { get; set; }
        public bool Active { get; set; }
    }
}