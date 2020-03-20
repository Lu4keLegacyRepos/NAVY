using QLearning.Core;
using System.Collections.Generic;
using System.Linq;

namespace QLearning.Game
{
    public static class Extensions
    {
        public static void GenerateEnvFor(this Enviroment env, int numOfRows, int numOfCols, IList<(int X, int Y)> obstacles, (int X, int Y) cheese)
        {
            int stateIndex = 0;
            var statePool = new List<IState>();

            var possibleCoords = GetCoord(numOfRows, numOfCols);

            for (int s = 0; s < numOfRows * numOfCols; s++)
            {
                statePool.Add(new State() { Index = stateIndex++, Type = StateType.Clean, Coordinations = possibleCoords[s] });
            }

            foreach (var s in statePool.Where(a => obstacles.Contains(((State)a).Coordinations)))
            {
                ((State)s).Type = StateType.Obstacle;
            }
            ((State)statePool.First(a => cheese == ((State)a).Coordinations)).Type = StateType.Cheese;

            foreach (var s in statePool)
            {
                var neigh = statePool.GetNeightbors((State)s, numOfRows, numOfCols);
                foreach ((State State, Direction Direction) n in neigh)
                {
                    s.Actions.Add(n.State, (double)n.State.Type);
                    switch (n.Direction)
                    {
                        case Direction.Down:
                            s.DownNeightbor = n.State;
                            break;
                        case Direction.Up:
                            s.UpNeightbor = n.State;
                            break;
                        case Direction.Left:
                            s.LeftNeightbor = n.State;
                            break;
                        case Direction.Right:
                            s.RightNeightbor = n.State;
                            break;

                    };
                }
            }
            env.States.AddRange(statePool);

        }

        public static void InitializeQMatrix(this Agent agent)
        {
            var agentStates = agent.env.States.Select(s => new State() { Index = ((State)s).Index, 
                LeftNeightbor=s.LeftNeightbor,
                RightNeightbor=s.RightNeightbor,
                UpNeightbor=s.UpNeightbor,
                DownNeightbor=s.DownNeightbor }).ToList();

            for (int i=0;i<agentStates.Count;i++)
            {
                foreach (var s in agentStates)
                {
                    agentStates[i].Actions.Add(s, 0);
                }
            }

            agent.Q.AddRange(agentStates);
            agent.ActualState = agent.Q.Find(x => x.Index == 0);

        }
        public static IList<(State State, Direction Direction)> GetNeightbors(this List<IState> stateQueue, State state, int numOfRows, int numOfCols)
        {
            var tmpCoord = new List<((int X, int Y) Coord, Direction Direction)>()
            {
                ((state.Coordinations.X+1, state.Coordinations.Y),Direction.Right),
                ((state.Coordinations.X-1, state.Coordinations.Y),Direction.Left),
                ((state.Coordinations.X, state.Coordinations.Y+1),Direction.Up),
                ((state.Coordinations.X, state.Coordinations.Y - 1), Direction.Down)
            };
            tmpCoord = tmpCoord.Where(a => a.Coord.X >= 0 && a.Coord.Y >= 0).ToList();
            tmpCoord = tmpCoord.Where(a => a.Coord.X < numOfCols && a.Coord.Y < numOfRows).ToList();
            var rtn = new List<(State State, Direction Sirection)>();
            foreach (var item in tmpCoord)
            {
                var tmp = stateQueue.First(x => ((State)x).Coordinations == item.Coord);
                if (tmp != null)
                {
                    rtn.Add(((State)tmp, item.Direction));
                }

            }
            return rtn;
        }
        private static IList<(int X, int Y)> GetCoord(int numOfRows, int numOfCols)
        {
            var rtn = new List<(int X, int Y)>();
            for (int x = 0; x < numOfRows; x++)
            {
                for (int y = 0; y < numOfCols; y++)
                {
                    rtn.Add((x, y));
                }

            }
            return rtn;
        }
    }
}
