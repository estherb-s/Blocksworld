using System;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
namespace Blocksworld
{
    public class Search
    {
        // default parameter depth, if u dont pass in the parameter depth then its automatically 0
        // Will find solution
        public static void DFS(Node root, char[,] goalState)
        {
            int nodesExpanded = 0;
            Stack<Node> fringe = new Stack<Node>();
            fringe.Push(root);
            while (fringe.Count > 0)
            {
                Node current = fringe.Pop();                                
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("DFS Nodes expanded: {0}", nodesExpanded);
                    break;
                }
                // Expand node if solution is not found
                nodesExpanded++;
                List<Node> successors = current.GetChildren();
                // add successors to stack 
                foreach (var succN in successors)
                {
                    fringe.Push(succN);
                }
            }
        }

        public static void DFGS(Node root, char[,] goalState)
        {
            int nodesExpanded = 0;
            Stack<Node> fringe = new Stack<Node>();
            fringe.Push(root);
            HashSet<string> storedNodes = new HashSet<string>();
            storedNodes.Add(root.BuildHashCode());
            while (fringe.Count > 0)
            {
                Node current = fringe.Pop();
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("DFGS Nodes expanded: {0}", nodesExpanded);
                    break;
                }
                // Expand node if solution is not found
                nodesExpanded++;
                List<Node> successors = current.GetChildren();
                foreach (var succN in successors)
                {                    
                    // If storedNodes doesn't already contain succN add it to the fringe
                    if (!storedNodes.Contains(succN.BuildHashCode()))
                    {
                        fringe.Push(succN);
                        storedNodes.Add(succN.BuildHashCode());                        
                    }  
                }
            }
        }

        
        public static void BFGS(Node root, char[,] goalState)
        {
            int nodesExpanded = 0;
            Queue<Node> fringe = new Queue<Node>();
            fringe.Enqueue(root);
            HashSet<string> storedNodes = new HashSet<string>();
            storedNodes.Add(root.BuildHashCode());
            while (fringe.Count > 0)
            {
                Node current = fringe.Dequeue();
                // Expand node if solution is not found
                nodesExpanded++;
                // Console.WriteLine("Expanding node: {0}, \t At depth: {1}", nodesExpanded, current.depth);
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("BFGS Nodes expanded: {0}", nodesExpanded);
                    return;
                }
                else
                {
                    List<Node> successors = current.GetChildren();
                    // int noOfDup = 0;
                    // int noOfNew = 0;
                    // add successors to queue 
                    foreach (var succN in successors)
                    {   
                        // Checks storedNodes doesnt add duplicates nodes
                        if (!storedNodes.Contains(succN.BuildHashCode()))
                        {
                            // noOfNew++;
                            fringe.Enqueue(succN);
                            storedNodes.Add(succN.BuildHashCode());
                            // Console.WriteLine("not repeated"); 
                        }  
                        // else
                        // {
                        //     // noOfDup++;
                        //     Console.Write("xxxx");
                        // }  
                        // Console.WriteLine("Dups " + noOfDup);
                        // Console.WriteLine("New" + noOfNew);
                    }
                    // string builder memory effi
                }                
            }
            Console.WriteLine("No solution found");
        }


        public static void BFS(Node root, char[,] goalState)
        {
            int nodesExpanded = 0;
            Queue<Node> fringe = new Queue<Node>();
            fringe.Enqueue(root);            
            while (fringe.Count > 0)
            {
                Node current = fringe.Dequeue();
                Console.WriteLine("Depth: {0}", current.depth);
                Console.WriteLine("BFS Nodes expanded: {0}", nodesExpanded);
                current.Print2DArray(current.state);
                Console.WriteLine("------------");
                // Expand node if solution is not found
                nodesExpanded++;
                // Console.WriteLine("Expanding node: {0}, \t At depth: {1}", nodesExpanded, current.depth);
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("BFS Nodes expanded: {0}", nodesExpanded);
                    break;
                }
                else
                {
                    List<Node> successors = current.GetChildren();
                    // add successors to queue 
                    foreach (var succN in successors)
                    {
                        fringe.Enqueue(succN);                                             
                    }
                }                
            }
        }

        // Iterative deepening search 
        public static void IDS(Node root, char[,] goalState)
        {
            int maxDepth = 0;
            int nodesExpanded = 0;
            Stack<Node> fringe = new Stack<Node>();
            fringe.Push(root);
            while (fringe.Count > 0)
            {
                Node current = fringe.Pop();
                Console.WriteLine("Depth : {0}", current.depth);
                Console.WriteLine("IDS Nodes expanded: {0}", nodesExpanded);
                current.Print2DArray(current.state);
                Console.WriteLine("------------");        
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("IDS Nodes expanded: {0}", nodesExpanded);
                    break;
                }
                
                // Expand node if node depth is less than max depth
                else if (current.depth < maxDepth)
                {
                    // Expand node if solution is not found
                    nodesExpanded++;
                    List<Node> successors = current.GetChildren();
                    // add successors to stack 
                    foreach (var succN in successors)
                    {
                        fringe.Push(succN);
                    }  
                }  

                if (fringe.Count == 0)
                {
                    fringe.Push(root);
                    maxDepth++;
                }  
            }
        }

        public static void AGStar(Node root, char[,] goalState)
		{
			int nodesExpanded = 0;

            SimplePriorityQueue<Node,int> fringe = new SimplePriorityQueue<Node,int>();
            
            fringe.Enqueue(root, 0);
            HashSet<string> storedNodes = new HashSet<string>();
            storedNodes.Add(root.BuildHashCode());
            // int i = 0;
            // Create an array of nodes and for every iteration sort array by the smallest f value
            while (fringe.Count > 0)
            {
                Node current = fringe.Dequeue();
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    // return depth, search type and nodes expanded,
                    // Console.WriteLine("Depth : {0}" + current.depth);
                    Console.WriteLine("A* Graph Nodes expanded: {0}", nodesExpanded);
                    break;
                }
                else
                {
                    // Expand node if solution is not found
                    nodesExpanded++;
                    List<Node> successors = current.GetChildren();
                    // Add successors to queue 
                    foreach (var succN in successors)
                    {
                        int fCost = succN.depth + succN.ManhattanDistance(goalState);

                        if (!storedNodes.Contains(succN.BuildHashCode()))
                        {
                            // noOfNew++;
                            fringe.Enqueue(succN, fCost);
                            storedNodes.Add(succN.BuildHashCode());
                            // Console.WriteLine("not repeated"); 
                            
                        }  
                    }
                }
            }
		}

        public static void AStar(Node root, char[,] goalState)
		{
			int nodesExpanded = 0;

            SimplePriorityQueue<Node,int> fringe = new SimplePriorityQueue<Node,int>();            
            fringe.Enqueue(root, 0);            
            // int i = 0;
            // Create an array of nodes and for every iteration sort array by the smallest f value
            while (fringe.Count > 0)
            {
                Node current = fringe.Dequeue();
                Console.WriteLine("Depth : {0}", current.depth);
                Console.WriteLine("A* Nodes expanded: {0}", nodesExpanded);
                current.Print2DArray(current.state);
                Console.WriteLine("----------------");                
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    // return depth, search type and nodes expanded,
                    // Console.WriteLine("Depth : {0}" + current.depth);
                    Console.WriteLine("A* Nodes expanded: {0}", nodesExpanded);
                    break;
                }
                else
                {
                    // Expand node if solution is not found
                    nodesExpanded++;
                    List<Node> successors = current.GetChildren();
                    // Add successors to queue 
                    foreach (var succN in successors)
                    {
                        int fCost = succN.depth + succN.ManhattanDistance(goalState);
                        fringe.Enqueue(succN, fCost);
                    }
                    
                }
                
            }
		}
    }
}



// public static bool CheckStored( char[,] current, HashSet<char[,]> storedNodes){
        //     // If it finds a match - a stored node it will become true
        //     // bool check = false;
        //     foreach (var stored in storedNodes)
        //     {
        //         // if theres an element in the array there will only be one that matches therefore 
        //         if( ArrayCheck(current,stored))
        //         {
        //             return true;
        //         };                
        //     }
        //     return false;            
        // }
