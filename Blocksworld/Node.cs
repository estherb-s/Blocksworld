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

        /// <summary> position of A </summary>
        public (int x, int y) posA { get; }
        /// <summary> position of B </summary>
        public (int x, int y) posB { get; }
        /// <summary> position of C </summary>
        public (int x, int y) posC { get; }
        /// <summary> List of sub-nodes </summary>
        public List<Node> children;
        public int depth;
        public Node parent;

        public int pathCost;
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
        public Node(char[,] startState, (int x, int y) agentPos, Node parent, int depth)
        {
            this.state = startState;
            this.agent = agentPos;
            this.posA = posA;
            this.posB = posB;
            this.posC = posC;
            this.parent = parent;
            this.depth = depth;
            // this.pathCost = pathCost;
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
        /// Moves the agent in the given direction, modifying this instances state.
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
                        // Console.WriteLine("cannot move up!");
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
                        // Console.WriteLine("cannot move down!");
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
                        // Console.WriteLine("cannot move left!");
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
                        // Console.WriteLine("cannot move right!");
                        return null;
                    }
                    break;

                default:
                    throw new ArgumentException($"Unrecognised direction: {direction}.");
            }

            // Print2DArray(moved);
            return moved;
        }

        /// <summary>
        /// Creates a new Node, where the agent is moved to the given position.
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

        public (int x, int y) GetAgentPos(char[,] state)
        {
            (int x, int y) agentPos = (0,0);
            for (int i = 0; i < width; i++) 
            { 
                for (int j = 0; j < height; j++)
                {
                    if (state[i,j] == '*')
                    {
                        agentPos = (i,j);
                    }
                }
            }
            return agentPos;
        }

        // public List<Node> NodePath(Node node)
        // {
        //     List<Node> path = new List<Node>();
        //     Node currentNode = node;
        //     while (currentNode.parent != null)
        //     {
        //         path.Add(currentNode);
        //         currentNode = currentNode.parent;  
        //     }
        //     path.Reverse();
        //     return path;
        // }

        public char[,] getState()
        {
            return state;
        }

        // public (int x, int y) getPosA()
        // {
        //     for (int i = 0; i < width; i++) 
        //     { 
        //         for (int j = 0; j < height; j++)
        //         {
        //             if (state[i, j] == 'A') { (int x, int y) posA = (i,j); }
        //         }
        //     }
        //     return posA;
        // }

        // public (int x, int y) getPosB()
        // {
        //     for (int i = 0; i < width; i++) 
        //     { 
        //         for (int j = 0; j < height; j++)
        //         {
        //             if (state[i, j] == 'B') { (int x, int y) posB = (i,j); }
        //         }
        //     }
        //     return posB;
        // }

        // public (int x, int y) getPosC()
        // {
        //     for (int i = 0; i < width; i++) 
        //     { 
        //         for (int j = 0; j < height; j++)
        //         {
        //             if (state[i, j] == 'C') { (int x, int y) posC = (i,j); }
        //         }
        //     }
        //     return posC;
        // }



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


