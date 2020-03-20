using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace QLearning.Core
{
    public class Enviroment
    {
        private readonly int dimension;
        private readonly IList<(int X, int Y)> obstacles;
        private readonly (int X, int Y) cheese;

        public ConcurrentDictionary<QState, List<QAction>> R { get; private set; }

        public Enviroment(int dimension, IList<(int X, int Y)> obstacles, (int X, int Y) cheese)
        {
            this.dimension = dimension;
            this.obstacles = obstacles;
            this.cheese = cheese;
            R = new ConcurrentDictionary<QState, List<QAction>>();
            GenerateEnvFor(dimension, dimension, obstacles, cheese);
        }

        public void UpdateRectangles()
        {
            foreach (var rect in R.Keys)
            {
                rect.UiItem.Fill = rect.Type switch
                {
                    StateType.Cheese => Brushes.Yellow,
                    StateType.Clean => Brushes.White,
                    _ => Brushes.Black
                };
                if (rect.Active)
                {
                    rect.UiItem.Fill = Brushes.Green;
                }
            }
        }

        private IList<(int X, int Y)> GetCoord(int numOfRows, int numOfCols)
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

        private void GenerateEnvFor(int numOfRows, int numOfCols, IList<(int X, int Y)> obstacles, (int X, int Y) cheese)
        {
            int stateIndex = 0;
            var statePool = new List<QState>();

            var possibleCoords = GetCoord(numOfRows, numOfCols);

            for (int s = 0; s < numOfRows * numOfCols; s++)
            {
                statePool.Add(new QState() { Index = stateIndex++, Active = false, Type = StateType.Clean, Coordinations = possibleCoords[s] });
            }

            foreach (var s in statePool.Where(a => obstacles.Contains(a.Coordinations)))
            {
                s.Type = StateType.Obstacle;
            }
            statePool.First(a => cheese == a.Coordinations).Type = StateType.Cheese;

            foreach (var s in statePool)
            {
                var neigh = GetNeightbors(statePool, s, numOfRows, numOfCols);
                var actions = neigh.Select(a => new QAction() { QStateIndex = a.Index, Value = (double)a.Type }).ToList();
                R.TryAdd(s, actions);
                
            }

        }
        private List<QState> GetNeightbors(List<QState> stateQueue, QState state, int numOfRows, int numOfCols)
        {
            var tmpCoord = new List<(int X, int Y)>()
            {
                (state.Coordinations.X + 1, state.Coordinations.Y),
                (state.Coordinations.X - 1, state.Coordinations.Y),
                (state.Coordinations.X, state.Coordinations.Y + 1),
                (state.Coordinations.X, state.Coordinations.Y - 1)
            };
            tmpCoord = tmpCoord.Where(a => a.X >= 0 && a.Y >= 0).ToList();
            tmpCoord = tmpCoord.Where(a => a.X < numOfCols && a.Y < numOfRows).ToList();

            var rtn = new List<QState>();
            foreach (var item in tmpCoord)
            {
                var tmp = stateQueue.First(x => x.Coordinations == item);
                if (tmp != null /*&& tmp.Type!=StateType.Obstacle*/)
                {
                    rtn.Add(tmp);
                }

            }
            return rtn;
        }
    }
}
