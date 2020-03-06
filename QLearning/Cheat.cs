using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace QLearning
{
    class Cheat
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting training...");

            Agent agent = new Agent(0.01, 0.3, 0.9);

            //1000 iterations of gameplay
            for (int i = 0; i < 1000; i++)
            {
                //Play the game
                var Game = new Game();
                while (true)
                {
                    //Pick a move
                    string state = Game.state();
                    var action = agent.PickMove(state);

                    //Move
                    (string nextState, bool gameOver, double reward) = Game.Move(action);

                    //Learn
                    agent.Learn(state, action, reward, nextState);

                    if (gameOver)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Training done...");
            var name = Console.ReadLine();
            //1000 iterations of gameplay
            for (int i = 0; i < 1000; i++)
            {
                //Play the game
                var Game = new Game();
                while (true)
                {
                    //Pick a move
                    string state = Game.state();
                    var action = agent.PickMove(state);

                    //Move
                    (string nextState, bool gameOver, double reward) = Game.Move(action);

                    //Learn
                    //agent.Learn(state, action, reward, nextState);

                    //Show game
                    Console.Clear();
                    Console.Write(Game.Print());
                    Console.Write("\n");
                    Console.Write(agent.PrintBrain());

                    var tname = Console.ReadLine();
                    if (gameOver)
                    {
                        Console.Write("Game Over");
                        break;
                    }
                }


            }



            Console.WriteLine("Done");
        }
    }

    public class Game
    {
        public List<List<char>> Board { get; set; }

        public Point Position = new Point(0, 0);

        public Game()
        {
            Position = new Point(0, 0);
            Board = new List<List<char>>()
            {
                new List<char>() { 'S', ' ', ' ', ' ', ' ' },
                new List<char>() { '#', ' ', '#', '#', ' ' },
                new List<char>() { ' ', ' ', ' ', ' ', '#' },
                new List<char>() { ' ', '#', '#', '#', ' ' },
                new List<char>() { ' ', ' ', ' ', ' ', 'F' },
            };
        }

        public string state()
        {
            string stateString = "";
            Board.ForEach(row => row.ForEach(val => stateString = stateString + val));
            return stateString;
        }

        public (string state, bool gameOver, double reward) Move(int move)
        {
            var newPos = new Point(Position.X, Position.Y);
            if (move == 0)
                newPos.X--;
            else if (move == 1)
                newPos.Y++;
            else if (move == 2)
                newPos.X++;
            else if (move == 3)
                newPos.Y--;

            if (newPos.X < 0 || newPos.X > 4 || newPos.Y < 0 || newPos.Y > 4)
                return (state(), false, -1);

            if (Board[newPos.X][newPos.Y] == '#')
                return (state(), true, -100);

            if (Board[newPos.X][newPos.Y] == 'F')
                return (state(), true, 100);

            Board[Position.X][Position.Y] = ' ';
            Position = newPos;
            Board[Position.X][Position.Y] = 'S';
            return (state(), false, 0);
        }

        public string Print()
        {
            string stateString = "";
            Board.ForEach(row => {
                row.ForEach(val => stateString = stateString + val);
                stateString = stateString + '\n';
            });
            return stateString;
        }
    }

    public class Agent
    {
        public Dictionary<string, List<double>> QTable { get; set; }
        public double LearningRate { get; set; }
        public double ExploreRate { get; set; }
        public double DiscountFactor { get; set; }

        public Agent(double learningRate = 1, double exploreRate = 0.3, double discountFactor = 0.01)
        {
            QTable = new Dictionary<string, List<double>>();
            LearningRate = learningRate;
            ExploreRate = exploreRate;
            DiscountFactor = discountFactor;
        }

        public int PickMove(string state)
        {
            var random = new Random();

            //Sometimes make a random move
            if ((random.NextDouble() < ExploreRate) || (!QTable.ContainsKey(state)))
                return random.Next(4);

            var currentOptionsQVals = QTable[state];
            var validActionList = new List<int>();
            var maxQVal = currentOptionsQVals.Max();

            //Get a list of all optimal values
            for (int i = 0; i < currentOptionsQVals.Count(); i++)
            {
                if (currentOptionsQVals[i] == maxQVal)
                    validActionList.Add(i);
            };

            //pick one at random
            int action = validActionList[random.Next(validActionList.Count())];
            return action;

        }

        public void Learn(string state, int action, double reward, string nextState)
        {

            if (!QTable.ContainsKey(state))
                QTable.Add(state, new List<double>() { 0, 0, 0, 0 });

            if (!QTable.ContainsKey(nextState))
                QTable.Add(nextState, new List<double>() { 0, 0, 0, 0 });

            var currentQ = QTable[state][action];
            var newQ = reward + DiscountFactor * QTable[nextState].Max();
            QTable[state][action] += LearningRate * (newQ - currentQ);

        }

        public string PrintBrain()
        {
            return "";// String.Join(", ", QTable);
        }
    }
}
