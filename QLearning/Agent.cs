using System;
using System.Collections.Generic;
using System.Linq;


namespace QLearning.Core
{
    public class Agent
    {
        public readonly Enviroment env;
        private readonly double learningRate;
        private readonly double exploreRate;

        // actual Q state
        public IState ActualState { get; set; }
        public List<IState> Q { get; private set; }

        public Agent(Enviroment env, double learningRate = .5, double exploreRate = .3)
        {
            this.env = env;
            this.learningRate = learningRate;
            this.exploreRate = exploreRate;
            Q = new List<IState>();
        }

        public void MakeMove()
        {
            var nextState = NextMove();
            Learn(nextState);
            ActualState = nextState;
        }
        private IState NextMove()
        {
            var random = new Random();
            IState move = null;
            if (random.NextDouble() < exploreRate)
            {
                while (move == null)
                {
                    move =(Direction) (random.Next(4)) switch
                    {
                        Direction.Down => ActualState.DownNeightbor,
                        Direction.Up => ActualState.UpNeightbor,
                        Direction.Left => ActualState.LeftNeightbor,
                        _ => ActualState.RightNeightbor,
                    };
                }
                return move;
            }
            var possibilities = ActualState.Actions.Where(x => x.Value >= 0).ToList();

            return possibilities.First(p => p.Value == possibilities.Max(x => x.Value)).Key;

        }
        private void Learn(IState nextState)
        {
            ActualState.Actions[nextState] =
                env.States.Find(s => s.Index == ActualState.Index)?.Actions.First(a => a.Key.Index == nextState.Index).Value ?? -1.0
                + learningRate * nextState.Actions.Values.Max();
        }
    }
}
