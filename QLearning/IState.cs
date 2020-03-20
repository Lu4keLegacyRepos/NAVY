using System.Collections.Generic;

namespace QLearning
{
    public interface IState
    {
        Dictionary<IState, double> Actions { get;  }
        public int Index { get;  }

        IState LeftNeightbor { get; set; }
        IState RightNeightbor { get; set; }
        IState UpNeightbor { get; set; }
        IState DownNeightbor { get; set; }

    }
}