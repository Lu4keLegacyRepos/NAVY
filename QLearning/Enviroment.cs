using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning.Core
{
    public class Enviroment
    {
        /// <summary>
        /// R matrix
        /// </summary>
        public List<IState> States { get; set; }

        public Enviroment()
        {
            States = new List<IState>();
        }




    }
}
