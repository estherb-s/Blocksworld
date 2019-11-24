using System;
using System.Collections.Generic;
using System.Text;

namespace Blocksworld
{
    class Program
    {
           static char[,] goalState = new char[,] {
            {'0','0', '0', '0'},
            {'0', 'A', '0', '0'},
            {'0', 'B', '0','0'},
            {'0', 'C', '0', '*'}
            };
        static char[,] startState = new char[,] {
            {'0', '0','0', '0'},
            {'0', '0', '0', '0'},
            {'0', '0', '0', '0'},
            {'A', 'B', 'C', '*'}
            };

        // static char[,] startState1 = new char[,] {
        //     {'0', '0', '0', '0'},
        //     {'0', 'A', '0', '0'},
        //     {'0', 'B', '0', '0'},
        //     {'0', '*', '0', 'C'}
        //     };
        static char[,] state { get; }

        static void Main(string[] args)
        {
            (int x, int y) startPos = (3,3); 
            Node initial = new Node(startState, startPos);
            List<Node> moves = new List<Node>();
            
            moves.Add(initial);
            
            moves = Moves(moves,"down");
            moves = Moves(moves,"up");
            
            // moves = Moves(moves,"right");

            // moves.Add(nextNode);
            // initial.MoveAgent("up");
            // initial.MoveAgent("right");
            // MoveAgent("down");

            // Console.WriteLine();

            // create a copy of the state
            // char[,] copy = (char[,])startState.Clone();
            // foreach (var element in copy) 
            // {
            //     Console.WriteLine(element);
            // }

        }

        public static List<Node> Moves(List<Node> moves, string direction) 
        {
            // If the agent can't move it will return the old list
            if (!(moves[moves.Count - 1].MoveAgent(direction).Length < 2))
            {
                // creates new node ffrom last element in list with the next move
                Node newN = new Node(moves[moves.Count - 1].MoveAgent(direction), moves[moves.Count - 1].nextMove);
                moves.Add(newN);
            }

            return moves;

        }
    }
}


// Search search = new Search();
