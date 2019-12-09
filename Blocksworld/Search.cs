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
                // Console.WriteLine("Expanding node: {0}, \t At depth: {1}", nodesExpanded, current.depth);
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("DFS Nodes expanded: {0}", nodesExpanded);
                    // var pathToNode = current.NodePath(current);
                    // Console.WriteLine("Path:", pathToNode);
                    // return nodes expanded, depth
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

        
        // Wont find it so narrow down search move initial closer to goal state
        public static void BFS(Node root, char[,] goalState)
        {
            int nodesExpanded = 0;
            Queue<Node> fringe = new Queue<Node>();
            fringe.Enqueue(root);
            while (fringe.Count > 0)
            {
                Node current = fringe.Dequeue();
                // Console.WriteLine("Expanding node: {0}, \t At depth: {1}", nodesExpanded, current.depth);
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    // Console.WriteLine("BFS Nodes expanded: {0}", nodesExpanded);
                    // return nodes expanded, depth, and search type
                    break;
                }
                // Expand node if solution is not found
                nodesExpanded++;
                List<Node> successors = current.GetChildren();
                // add successors to queue 
                foreach (var succN in successors)
                {
                    fringe.Enqueue(succN);
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
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    Console.WriteLine("IDS Nodes expanded: {0}", nodesExpanded);
                    // return nodes expanded, depth, and search type
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
                // current.Print2DArray(current.state);
                // Console.WriteLine("--------");
                // Check if goal is found
                if (current.goalStateReached(goalState))
                {
                    // return nodes expanded, depth, and search type
                    Console.WriteLine("A* Nodes expanded: {0}", nodesExpanded);
                    Console.WriteLine("Depth : " + current.depth);
                    break;
                }
                else
                {
                    // Expand node if solution is not found
                    nodesExpanded++;
                    List<Node> successors = current.GetChildren();
                    // add successors to queue 
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


// Depth limited search
        // public static void DLS(Node root, char[,] goalState)
        // {
            
        // }