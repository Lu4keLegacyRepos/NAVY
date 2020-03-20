using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pole.QLearning
{
    public class Agent
    {
        private readonly Enviroment env;
        private readonly (double minX, double maxX) xRange;
        private readonly (double minDegree, double maxDegree) degreeRange;
        private readonly double learningRate;
        private readonly double exploreRate;
        public ConcurrentDictionary<QState, List<QAction>> Q { get; private set; }
        private Random random;

        public Agent(Enviroment env,(double minX, double maxX) xRange, (double minDegree, double maxDegree) degreeRange, double learningRate = .9, double exploreRate = .1)
        {
            this.learningRate = learningRate;
            this.exploreRate = exploreRate;
            this.env = env;
            this.xRange = xRange;
            this.degreeRange = degreeRange;
            random = new Random();
            Q = env.R;

            // init Q to zeros
            foreach (var state in Q)
            {
                foreach (var action in state.Value)
                {
                    action.Qvalue = 0;
                }
            }

        }
        /// <summary>
        /// true -> right,  false -> left
        /// </summary>
        /// <param name="actualX"></param>
        /// <param name="actualAngle"></param>
        /// <param name="withLearn"></param>
        /// <returns></returns>
        public bool MakeMove(double actualX, double actualAngle, bool lastMove,bool withLearn=true)
        {
            var inRange = actualX < xRange.maxX && actualX > xRange.minX;
            var inAngle = actualAngle < degreeRange.maxDegree && actualAngle > degreeRange.minDegree;
            var actualState = Q.First(s => s.Key.InAngleRange == inAngle && s.Key.InRange == inRange && s.Key.Direction==lastMove);
            QState nextState;
            // Exploration
            if (random.NextDouble() < exploreRate)
            {
                var nextStateIndex = actualState.Value[random.Next(actualState.Value.Count)].Index;
                nextState = Q.First(q => q.Key.Index == nextStateIndex).Key;
            }
            else // Exploit
            {
                var possibilities = actualState.Value.Where(v => v.Qvalue == actualState.Value.Max(x => x.Qvalue)).ToList();
                var rnd = random.Next(possibilities.Count);
                nextState = Q.First(q => q.Key.Index == possibilities[rnd].Index).Key;
            }
            if (withLearn)
            {
                Learn(actualState, nextState);
            }

            return nextState.Direction;
            
        }

        private void Learn(KeyValuePair<QState, List<QAction>> actualState, QState nextState)
        {
            if (!Q.ContainsKey(nextState))
            {
                throw new System.Exception("nextState must exist inside Q ");
            }

            var newQ = actualState.Value[nextState.Index].Reward + learningRate * nextState.Qvalue;
            actualState.Value[nextState.Index].Qvalue = newQ;

        }

    }
}
