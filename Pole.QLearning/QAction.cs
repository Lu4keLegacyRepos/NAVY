namespace Pole.QLearning
{
    public class QAction : QState
    {
        
        public QAction(int index, bool inRange, bool inAngleRange, bool direction)
            : base(index, inRange, inAngleRange, direction)
        {
        }

    }
}