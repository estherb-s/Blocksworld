using System;
using System.Collections.Generic;

namespace Blocksworld
{
    public class Node
    {
        /// <summary> state of the node </summary>
        public char[,] state { get; }
        /// <summary> width of array </summary>
        private int width => state.GetLength(0);
        /// <summary> height of array </summary>
        private int height => state.GetLength(1);
        /// <summary> position of agent </summary>
        public (int x, int y) agent { get; }

        public (int x, int y) nextMove { get; set;}
        public List<Node> moves;

        static char[,] startState = new char[,] 
        {
            {'0', '0','0', '0'},
            {'0', '0', '0', '0'},
            {'0', '0', '0', '0'},
            {'A', 'B', 'C', '*'}
        };

        /// <summary>
        /// Defines the Node and its properties
        /// <summary> 
        public Node(char[,] startState, (int x, int y) agentPos)
        {
            this.state = startState;
            this.agent = agentPos;
            Console.WriteLine(state);
            // print the board
            // Print2DArray(startState);
            
            // Print2DArray(startState);
        }    

        public static void Print2DArray(char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                Console.Write(array[i,j] + "\t");
                }
                Console.Write("\n");
            }  
        }

        /// <summary>
        /// Determines whether a move to this position would be legal.
        /// Move only legal if it remains within the board and is adjacent to the current agent pos.
        /// </summary>
        public bool LegalMove((int x, int y) pos)
        {
            // pos outside bounds of board
            if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
            {
                return false;
            }
            // pos not adjacent to current pos
            if (Math.Abs(agent.x - pos.x) + Math.Abs(agent.y - pos.y) != 1)
            {
                return false;
            }
            return true;
        }

     

        /// <summary>
        /// Moves the agent in the given direction, modifying this instances state.
        /// </summary>
        public char[,] MoveAgent(string direction)
        {
            char[,] moved = new char[1,1];
            switch (direction)
            { 
                case "up":
                    (int x, int y) up = (agent.x - 1, agent.y);
                    nextMove = (agent.x - 1, agent.y);
                    if (LegalMove(up))
                    {
                        moved = Swap(up, state);
                    }
                    else
                    {
                        moved = new char[1,1];
                        Console.WriteLine("cannot move up!");
                    }
                    break;
                case "down":
                    (int x, int y) down = (agent.x + 1, agent.y);
                    nextMove = (agent.x + 1, agent.y);
                    if (LegalMove(down))
                    {
                        moved = Swap(down, state);
                    }
                    else
                    {
                        moved = new char[1,1];
                        Console.WriteLine("cannot move down!");
                    }
                    break;
                case "left":
                    (int x, int y) left = (agent.x, agent.y-1);
                    nextMove = (agent.x, agent.y - 1);
                    if (LegalMove(left))
                    {
                        moved = Swap(left, state);
                    }
                    else
                    {
                        moved = new char[1,1];
                        Console.WriteLine("cannot move left!");
                    }
                    break;
                case "right":
                    (int x, int y) right = (agent.x, agent.y + 1);
                    nextMove = (agent.x, agent.y +1 );
                    if (LegalMove(right))
                    {
                        moved = Swap(right, state);
                    }
                    else
                    {
                        moved = new char[1,1];
                        Console.WriteLine("cannot move right!");
                    }

                    break;
                default:
                    moved = new char[1,1];
                    break;
            }

            try
            {
                Print2DArray(moved);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return moved;
        }

        /// <summary>
        /// Creates a new Node, where the agent is moved to the given position.
        /// was private before
        /// </summary>
        private char[,] Swap((int x, int y) newPos, char[,] board)
        {
            // create a copy of the state
            char[,] copy = (char[,])board.Clone();
            
            // perform swap in copy
            var oldPos = agent;
            copy[oldPos.x, oldPos.y] = board[newPos.x, newPos.y];
            copy[newPos.x, newPos.y] = board[agent.x, agent.y];
            // update position of agent
            // agent  = (newPos.x, newPos.y);
            // return new node made from that state
            return copy;
        }

        // /// <summary> Returns all valid moves from this state </summary>
        // public List<Node> AllValidMoves()
        // {
        //     var positions = Positions();
        //     // list all possible moves
        //     // check which are legal
        //     // perform moves, and return them
        //     List<Node> children = new List<Node>();
        //     foreach ((int x, int y) pos in positions)
        //     {
        //         if (LegalMove(pos)) 
        //         {
        //             Node child = Swap(pos);
        //             children.Add(child);
        //         }
        //     }
        //     return children;    
        // }

        public bool goalStateReached(char[,] goalState)
        {
            for (int i = 0; i < width; i++) 
            { 
                for (int j = 0; j < height; i++)
                {
                    if (state[i, j] != 'A' && goalState[i, j] == 'A')
                        return false;
                    if (state[i,j] != 'B' && goalState[i,j] == 'B')
                        return false;
                    if (state[i,j] != 'C' && goalState[i,j] == 'C')
                        return false;
                }
            }
            return false;
        }

    }
}


   /// <summary>
        /// List of positions agent can move to 
        /// <summary> 
        // public List<(int x, int y)> Positions()
        // {
        //     List<(int x, int y)>  pos = new List<(int x, int y)>();
        //     // left
        //     pos.Add((agent.x - 1, agent.y));
        //     // right
        //     pos.Add((agent.x + 1, agent.y));
        //     // down
        //     pos.Add((agent.x, agent.y - 1));
        //     // up
        //     pos.Add((agent.x, agent.y + 1));
        //     return pos;

        // } 


