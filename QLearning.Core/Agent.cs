using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace QLearning.Core
{
    /// <summary>
    /// entita vyuzivající metodiku Q-learningu
    /// </summary>
    public class Agent
    {
        private readonly Enviroment env;
        private readonly double learningRate;
        private readonly double exploreRate;
        public ConcurrentDictionary<QState, List<QAction>> Q { get; private set; }
        public QState ActualState { get; private set; }
        private Random random;

        public Agent(Enviroment env, double learningRate = .9, double exploreRate = .1)
        {
            this.learningRate = learningRate;
            this.exploreRate = exploreRate;
            this.env = env;
            random = new Random();
            Q = new ConcurrentDictionary<QState, List<QAction>>();

            foreach (var state in env.R)
            {
                Q.TryAdd(state.Key, InitActions());
            }
            RandomAgentState();
        }
        /// <summary>
        /// nastaveni herniho pole a Q matice
        /// </summary>
        public void RandomAgentState()
        {
            foreach (var q in Q.Keys.Where(k => k.Active))
            {
                q.Active = false;
            }

            var cntQClean = Q.Count(x => x.Key.Type == StateType.Clean);
            ActualState = Q.Where(x => x.Key.Type == StateType.Clean).ToList()[random.Next(cntQClean)].Key;
            ActualState.Active = true;
        }

        /// <summary>
        /// vygenerovani listu akci pro stavy 
        /// </summary>
        /// <returns></returns>
        private List<QAction> InitActions()
        {
            var rtn = new List<QAction>();
            foreach (var r in env.R)
            {
                rtn.Add(new QAction() { QStateIndex = r.Key.Index });
            }
            return rtn;
        }
        public string PrintMemory()
        {
            string console = "";
            var mem = Q.OrderBy(x => x.Key.Index);
            foreach (var m in mem)
            {
                var line = m.Value.OrderBy(x => x.QStateIndex);
                foreach (var l in line)
                {
                    console += $" {(int)l.Value}";
                }
                console += "\n";
            }
            return console;
        }

        /// <summary>
        /// pohyb agenta s poctem episod Q-learningu
        /// </summary>
        /// <param name="episodesCnt"></param>
        /// <returns></returns>
        public int AutoMove(int episodesCnt = 1)
        {
            int cnt = 0;
            for (int i = 0; i < episodesCnt; i++)
            {
                cnt = 0;
                RandomAgentState();
                while (ActualState.Type != StateType.Cheese)
                {
                    MakeMove();
                    cnt++;
                }
            }
            return cnt;
        }

        /// <summary>
        /// jedna zmen stavu agenta s sanci na exploraci
        /// </summary>
        public void MakeMove()
        {
            QState nextState;
            // Exploration
            if (random.NextDouble() < exploreRate)
            {
                var nextStateIndex = env.R[ActualState][random.Next(env.R[ActualState].Count)].QStateIndex;
                nextState = Q.First(q => q.Key.Index == nextStateIndex).Key;
            }
            else
            {
                var possibleIndexes = env.R[ActualState].Select(x => x.QStateIndex);
                var possibilities = Q[ActualState].Where(p => possibleIndexes.Any(x => x == p.QStateIndex)).ToList();

                possibilities = possibilities.Where(p => p.Value == possibilities.Max(x => x.Value)).ToList();
                var rnd = random.Next(possibilities.Count);
                nextState = Q.First(q => q.Key.Index == possibilities[rnd].QStateIndex).Key;
            }

            Learn(nextState);
        }

        /// <summary>
        ///  Q-learn na zaklade prechodu stavu
        /// </summary>
        /// <param name="nextState"></param>
        private void Learn(QState nextState)
        {
            if (!Q.ContainsKey(nextState))
            {
                throw new System.Exception("nextState must exist inside Q ");
            }

            if (!Q[ActualState].Exists(action => action.QStateIndex == nextState.Index))
            {
                Q[ActualState].Add(new QAction() { QStateIndex = nextState.Index });

            }
            var Rval = env.R[ActualState].Exists(a => a.QStateIndex == nextState.Index) ?
                    env.R[ActualState].First(a => a.QStateIndex == nextState.Index).Value :
                    -1;

            Q[ActualState].First(action => action.QStateIndex == nextState.Index).Value = Rval + learningRate * Q[nextState].Max(a => a.Value);
            ActualState = nextState;
            foreach (var q in Q.Keys.Where(k => k.Active))
            {
                q.Active = false;
            }
            ActualState.Active = true;
        }

    }

}
