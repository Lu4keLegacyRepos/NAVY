using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Pole.QLearning
{
    public class Enviroment
    {
        private readonly int dimension;

        public ConcurrentDictionary<QState, List<QAction>> R { get; private set; }

        public Enviroment(int dimension)
        {
            this.dimension = dimension;
            R = new ConcurrentDictionary<QState, List<QAction>>();
            InitializeEnviroment();
        }


        private void InitializeEnviroment()
        {
            //double[,] rewards = new double[8, 8]
            //{
            //    {-1,.1,0,.1,.1,1,.1,1 },
            //    {-1,-1,-1,0,0,1,0,1 },
            //    {0,.1,-1,.1,.1,1,.1,1 },
            //    {-1,0,0,-1,0,1,0,1 },
            //    {-1,0,-1,0,-1,1,0,1 },
            //    {-1,-1,-1,-1,-1,1,-1,1 },
            //    {-1,0,-1,0,0,1,-1,1 },
            //    {-1,-1,-1,-1,-1,1,-1,1 }

            //};
            double[,] rewards = new double[8, 8]
{
                {-1,0,0,0,0,0,0,1 },
                {-1,-1,-1,0,-1,0,-1,1 },
                {-1,-1,-1,0,-1,0,0,1 },
                {-1,-1,-1,-1,0,-1,-1,1 },
                {-1,-1,-1,0,-1,0,-1,1 },
                {-1,-1,-1,0,-1,-1,-1,1 },
                {-1,-1,-1,0,-1,-1,-1,1 },
                {-1,-1,-1,-1,-1,-1,-1,1 }

};
            var states = new List<QState>()
            {
                new QState(0,false,false,false),
                new QState(1,true,false,false),
                new QState(2,false,true,false),
                new QState(3,true,true,false),
                new QState(4,false,false,true),
                new QState(5,true,false,true),
                new QState(6,false,true,true),
                new QState(7,true,true,true)
            };

            for (int i = 0; i < 8; i++) //check rewards [,]
            {
                var tmpList = new List<QAction>();
                for (int j = 0; j < 8; j++)
                {
                    states[j].Reward = rewards[i, j];
                    tmpList.Add(states[j].ToQAction());
                }
                R.TryAdd(states[i], tmpList);
            }


        }


    }
}
