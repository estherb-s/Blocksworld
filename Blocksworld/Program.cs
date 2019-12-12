using System;
using System.Collections.Generic;
using System.Text;

namespace Blocksworld
{
    class Program
    {
        public static Dictionary<int, char[,]> Depths() 
        {
            Dictionary<int, char[,]> optimalDepths = new Dictionary<int, char[,]>
            {
                // {1, new char[,] { {'0','0', '0', '0'}, {'0','*', 'A', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '0'}} },
                // {2, new char[,] { {'0','0', '0', '0'}, {'*','0', 'A', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '0'}} },
                {3, new char[,] { {'*','0', '0', '0'}, {'0','0', 'A', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '0'}} },
                // {4, new char[,] { {'0','0', '0', '0'}, {'0','0', 'A', '0'}, {'0','B', '0', '0'}, {'*','C', '0', '0'}} },
                // {5, new char[,] { {'0','0', '0', '0'}, {'A','0', '0', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '*'}} },
                // {6, new char[,] { {'0','0', '0', '0'}, {'0','0', 'A', '0'}, {'0','B', '0', '0'}, {'C','0', '*', '0'}} }, 
                // {7, new char[,] { {'0','0', '0', '0'}, {'0','0', 'A', '0'}, {'B','0', '0', '0'}, {'0','C', '0', '*'}} },
                // {8, new char[,] { {'0','0', '0', '0'}, {'0','A', '0', '0'}, {'0','C', '0', 'B'}, {'0','0', '0', '*'}} },
                // {9, new char[,] { {'*','0', '0', '0'}, {'0','0', 'A', '0'}, {'B','0', '0', '0'}, {'0','0', 'C', '0'}} },
                // {10, new char[,] { {'0','0', '0', '0'}, {'0','A', '0', '0'}, {'0','0', '0', 'B'}, {'C','0', '0', '*'}} },
                // {11, new char[,] { {'0','A', '0', '0'}, {'0','0', '0', '0'}, {'0','B', 'C', '0'}, {'0','0', '0', '*'}} },
                // {12, new char[,] { {'0','0', '0', '0'}, {'0','A', 'C', '0'}, {'B','0', '0', '0'}, {'0','0', '*', '0'}} },
                // {13, new char[,] { {'A','0', '0', '0'}, {'0','0', '0', '0'}, {'0','B', 'C', '0'}, {'0','0', '0', '*'}} },
                // {14, new char[,] { {'0','0', '0', '0'}, {'0','0', '0', '0'}, {'0','0', '0', '0'}, {'A','B', 'C', '*'}} }
            };            
            return optimalDepths;
        }
    
        static char[,] goalState = new char[,] {
            {'0','0', '0', '0'},
            {'0', 'A', '0', '0'},
            {'0', 'B', '0','0'},
            {'0', 'C', '0', '*'}
            };

        static void Main(string[] args)
        {
            var optDepths = Depths();
            foreach (var item in optDepths)
            {
                (int x, int y) startPos = Node.GetAgentPos(item.Value); 
                Node initial = new Node(item.Value, startPos, null, 0);
                Console.WriteLine("Optimal Depth: {0}", item.Key);
                // Search.BFS(initial, goalState);
                // Search.BFGS(initial, goalState);
                Search.DFS(initial, goalState);
                // Search.DFGS(initial, goalState);
                // Search.IDS(initial, goalState);
                // Search.AStar(initial, goalState);
                // Search.AGStar(initial, goalState);

            };              
        }     
    }
}


   // static char[,] startState = new char[,] {
        //     {'0', '0','0', '0'},
        //     {'0', '0', '0', '0'},
        //     {'0', '0', '0', '0'},
        //     {'A', 'B', 'C', '*'}
        //     };
        // // static char[,] state { get; }

