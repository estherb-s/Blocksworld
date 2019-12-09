using System;
using System.Collections.Generic;
using System.Text;

namespace Blocksworld
{
    class Program
    {
        static char[,] depth1 = new char[,] { {'0','0', '0', '0'}, {'0','*', 'A', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '0'}};
        static char[,] depth2 = new char[,] { {'0','0', '0', '0'}, {'*','0', 'A', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '0'}};
        static char[,] depth3 = new char[,] { {'*','0', '0', '0'}, {'0','0', 'A', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '0'}};
        static char[,] depth4 = new char[,] { {'0','0', '0', '0'}, {'0','0', 'A', '0'}, {'0','B', '0', '0'}, {'*','C', '0', '0'}};
        static char[,] depth5 = new char[,] { {'0','0', '0', '0'}, {'A','0', '0', '0'}, {'0','B', '0', '0'}, {'0','C', '0', '*'}};
        static char[,] depth6 = new char[,] { {'0','0', '0', '0'}, {'0','0', 'A', '0'}, {'0','B', '0', '0'}, {'C','0', '*', '0'}};
        static char[,] depth7 = new char[,] { {'0','0', '0', '0'}, {'0','0', 'A', '0'}, {'B','0', '0', '0'}, {'0','C', '0', '*'}};
        static char[,] depth8 = new char[,] { {'0','0', '0', '0'}, {'0','A', '0', '0'}, {'0','C', '0', 'B'}, {'0','0', '0', '*'}};
        static char[,] depth9 = new char[,] { {'*','0', '0', '0'}, {'0','0', 'A', '0'}, {'B','0', '0', '0'}, {'0','0', 'C', '0'}};
        static char[,] depth10 = new char[,] { {'0','0', '0', '0'}, {'0','A', '0', '0'}, {'0','0', '0', 'B'}, {'C','0', '0', '1'}};
        static char[,] depth11 = new char[,] { {'0','A', '0', '0'}, {'0','0', '0', '0'}, {'0','B', 'C', '0'}, {'0','0', '0', '*'}};
        static char[,] depth12 = new char[,] { {'0','0', '0', '0'}, {'0','A', 'C', '0'}, {'B','0', '0', '0'}, {'0','0', '*', '0'}};
        static char[,] depth13 = new char[,] { {'A','0', '0', '0'}, {'0','0', '0', '0'}, {'0','B', 'C', '0'}, {'0','0', '0', '*'}};
        static char[,] depth14 = new char[,] { {'0','0', '0', '0'}, {'0','0', '0', '0'}, {'0','0', '0', '0'}, {'A','B', 'C', '*'}};
        

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

        static char[,] startState1 = new char[,] {
            {'0', '0', '0', '0'},
            {'0', 'A', '0', '0'},
            {'0', 'B', '0', '0'},
            {'0', '*', 'C', '0'}
            };
        static char[,] state { get; }

        static void Main(string[] args)
        {
            (int x, int y) startPos = (3,3); 
            // (int x, int y) startPos1 = (1,3); 
            Node initial = new Node(startState, startPos, null, 0);
        
            // Search.BFS(initial, goalState);
            // Search.DFS(initial, goalState);
            // Search.IDS(initial, goalState);
            Search.AStar(initial, goalState);

                  
        }     
    }
}


