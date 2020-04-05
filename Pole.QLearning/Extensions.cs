using System;
using System.Collections.Generic;
using System.Text;

namespace Pole.QLearning
{
    public static class Extensions
    {
        public static QAction ToQAction(this QState state)
        {
            return new QAction(state.Index, state.InRange, state.InAngleRange, state.BetterAngle) { Reward = state.Reward, Qvalue=state.Qvalue };
        }
    }
}
