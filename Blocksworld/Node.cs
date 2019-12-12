using System;
using System.Collections.Generic;
using System.Text;

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

        /// <summary> List of sub-nodes </summary>
        public List<Node> children;
        public int depth;
        public Node parent;
    

        /// <summary>
        /// Defines the Node and its properties
        /// <summary> 
        public Node(char[,] startState, (int x, int y) agentPos, Node parent, int depth)
        {
            this.state = startState;
            this.agent = agentPos;
            this.parent = parent;
            this.depth = depth;
        }    

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();
            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                // Choose a random node in the list
                randomIndex = r.Next(0, inputList.Count); 
                // Add it to the new, random list
                randomList.Add(inputList[randomIndex]); 
                // Remove to avoid duplicates
                inputList.RemoveAt(randomIndex); 
            }
            // Return the new random list
            return randomList; 
        }

        /// <summary>
        /// Returns a list of children from a node
        /// <summary> 
        public List<Node> GetChildren() 
        {
            List<string> directions = new List<string>() {"Up", "Down", "Left", "Right"};
            List<Node> children = new List<Node>();
            foreach (var direction in directions)
            {
                // Move direction of this node
                var provisionalMove = this.MoveAgent(direction, out var nextMove);
                if (provisionalMove != null)
                {
                    // Creates a new state which is added to a new node, which is added to children
                    Node newN = new Node(provisionalMove, nextMove, this, this.depth + 1);
                    children.Add(newN);
                }  
            }
            return ShuffleList(children);
        }

        public void Print2DArray(char[,] array)
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
        /// Moves the agent in the given direction, modifying this instance's state.
        /// </summary>
        public char[,] MoveAgent(string direction, out (int x, int y) nextMove)
        {
            char[,] moved;
            switch (direction)
            { 
                case "Up":
                    (int x, int y) up = (agent.x - 1, agent.y);
                    nextMove = (agent.x - 1, agent.y);
                    if (LegalMove(up))
                    {
                        moved = Swap(up, state);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case "Down":
                    (int x, int y) down = (agent.x + 1, agent.y);
                    nextMove = (agent.x + 1, agent.y);
                    if (LegalMove(down))
                    {
                        moved = Swap(down, state);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case "Left":
                    (int x, int y) left = (agent.x, agent.y-1);
                    nextMove = (agent.x, agent.y - 1);
                    if (LegalMove(left))
                    {
                        moved = Swap(left, state);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case "Right":
                    (int x, int y) right = (agent.x, agent.y + 1);
                    nextMove = (agent.x, agent.y +1 );
                    if (LegalMove(right))
                    {
                        moved = Swap(right, state);
                    }
                    else
                    {
                        return null;
                    }
                    break;

                default:
                    throw new ArgumentException($"Unrecognised direction: {direction}.");
            }
            return moved;
        }

        /// <summary>
        /// Creates a new state, where the agent is moved to the given position.
        /// </summary>
        private char[,] Swap((int x, int y) newPos, char[,] board)
        {
            // create a copy of the state
            char[,] copy = (char[,])board.Clone();
            
            // perform swap in copy
            var oldPos = agent;
            copy[oldPos.x, oldPos.y] = board[newPos.x, newPos.y];
            copy[newPos.x, newPos.y] = board[agent.x, agent.y];            
            // return new node made from that state
            return copy;
        }

        /// <summary>
        /// Determines if the current state is the goal state
        /// </summary>
        public bool goalStateReached(char[,] goalState)
        {
            for (int i = 0; i < width; i++) 
            { 
                for (int j = 0; j < height; j++)
                {
                    if (state[i, j] != 'A' && goalState[i, j] == 'A')
                        return false;
                    if (state[i,j] != 'B' && goalState[i,j] == 'B')
                        return false;
                    if (state[i,j] != 'C' && goalState[i,j] == 'C')
                        return false;
                }
            }
            return true;
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

        // Get heuristic by finding MD for A, B, C
        public int ManhattanDistance(char[,] goalState)
        {
            (int x, int y)[] positionsEnd = GetPositions(goalState);
            (int x, int y)[] positionsCurrent = GetPositions(this.state);

            int distance = 0;

            for (int i = 0; i < 3; i++)
                distance += (Math.Abs(positionsCurrent[i].x - positionsEnd[i].x) + Math.Abs(positionsCurrent[i].y - positionsEnd[i].y));
            return distance;
        }

        public (int x, int y)[] GetPositions(char[,] state)
        {
            (int x, int y)[] positions = new (int x, int y)[3];
            for (int i = 0; i < width; i++) 
            { 
                for (int j = 0; j < height; j++)
                {
                    if (state[i,j] == 'A')
                    {
                        positions[0] = (i,j);
                    }

                    if (state[i,j] == 'B')
                    {
                        positions[1] = (i,j);    
                    }

                    if (state[i,j] == 'C')
                    {
                        positions[2] = (i,j);
                    }
                }
            }
            return positions;
        }

        public static (int x, int y) GetAgentPos(char[,] currentState)
        {
            (int x, int y) agentPos = (0,0);
            for (int i = 0; i < currentState.GetLength(0); i++) 
            { 
                for (int j = 0; j < currentState.GetLength(1); j++)
                {
                    if (currentState[i,j] == '*')
                    {
                        agentPos = (i,j);
                    }
                }
            }
            return agentPos;
        }

        public string BuildHashCode() {
            StringBuilder sb = new StringBuilder();
            // adds then creates a string so is more memory efficient
            for (int i = 0; i < this.state.GetLength(0); i++) 
            { 
                for (int j = 0; j < this.state.GetLength(1); j++)
                {
                    sb.Append(this.state[i,j]);
                }
            }
            return sb.ToString();
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

        // public static List<Node> GetChildren(List<Node> moves, string direction) 
        // {
        //     // If the agent can't move it will return the old list
        //     if (!(moves[moves.Count - 1].MoveAgent(direction).Length < 2))
        //     {
        //         // creates new node from last element in list with the next move
        //         Node newN = new Node(moves[moves.Count - 1].MoveAgent(direction), moves[moves.Count - 1].nextMove);
        //         moves.Add(newN);
        //     }

        //     return moves;

        // }


