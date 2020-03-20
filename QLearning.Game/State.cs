using System.Collections.Generic;

namespace QLearning.Game
{
    public class State : IState
    {
        public int Index { get; set; }
        public (int X, int Y) Coordinations { get; set; }
        public Dictionary<IState, double> Actions { get; private set; }
        public StateType Type { get; set; }
        public IState LeftNeightbor { get; set; }
        public IState RightNeightbor { get; set; }
        public IState UpNeightbor { get; set; }
        public IState DownNeightbor { get; set; }

        public State()
        {
            Actions = new Dictionary<IState, double>();
        }


    }
}
