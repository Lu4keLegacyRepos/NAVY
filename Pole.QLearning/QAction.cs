namespace Pole.QLearning
{
    public class QAction : QState
    {
        
        public QAction(int index, bool inRange, bool inAngleRange, bool betterAngle)
            : base(index, inRange, inAngleRange, betterAngle)
        {
        }

    }
}