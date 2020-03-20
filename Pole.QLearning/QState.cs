

namespace Pole.QLearning
{
    public class QState
    {


        public int Index { get;private set; }
        public bool InRange { get; private set; }
        public bool InAngleRange { get; private set; }
        public double Qvalue { get; set; }
        /// <summary>
        /// True => right | False => left
        /// </summary>
        public bool Direction { get; private set; }
        public double Reward { get; set; }

        public QState(int index, bool inRange, bool inAngleRange, bool direction)
        {
            Index = index;
            InRange = inRange;
            InAngleRange = inAngleRange;
            Direction = direction;
        }

    }
}