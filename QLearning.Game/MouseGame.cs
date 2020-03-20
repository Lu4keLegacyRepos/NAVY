using QLearning.Core;
using System;
using System.Collections.Generic;

namespace QLearning.Game
{
    public class MouseGame
    {
        public Enviroment Enviroment { get;private set; }
        public Agent Agent { get; private set; }
        public MouseGame(int dim = 30)
        {
            Enviroment = new Enviroment();

            var obstacles = new List<(int X, int Y)>();
            {
                var rnd = new Random();
                for (int i = 0; i < dim; i++)
                {
                    obstacles.Add((rnd.Next(dim), rnd.Next(dim)));
                }
            }

            Enviroment.GenerateEnvFor(dim, dim, obstacles, (dim - 1, dim - 1));

            Agent = new Agent(Enviroment);
            Agent.InitializeQMatrix();
        }

        public void Next()
        {
            Agent.MakeMove();
        }
    }
}
