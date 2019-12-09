//public void swapAgent(int px, int py, int newPx, int newPy)
//{
//    if (legalMove(px, py))
//    {
//        // Copy first position's element
//        int temp = state[px, py];
//        // Assign pos to second element
//        state[px, py] = state[newPx, newPy];
//        // Assign pos to first element
//        state[newPx, newPy] = temp;

//        // Update pos of agent
//        newPx = px;
//        newPy = py;
//    }
//}

//case UP:
//if(agent.getYPos() == 0) return false;
//else{
//int newYPos = agent.getYPos() - 1;
//Block old = blocks[agent.getXPos()][newYPos];
//blocks[agent.getXPos()][agent.getYPos()] = old;
//old.setYPos(agent.getYPos());
//blocks[agent.getXPos()][newYPos] = agent;
//agent.setYPos(newYPos);



//public State moveAgent(String direction)
//{
    // switch (direction)
    //         { 
    //             case "up":
    //                 // swap(agentPos, agentPos+1)
    //                 // var newPy = aY + 1;
    //                 // temp = (aX, aY);
    //                 // // Assign pos to second element
    //                 // (aX, aY) = (aX, newPy);
    //                 // // Assign pos to first element
    //                 // (px, newPy) = temp;
    //                 // // Update pos of agent
    //                 // newPy = py;
    //                 break;


    //             case "down":
    //                 newPy = py - 1;
    //                 temp = state[px, py];
    //                 state[px, py] = state[px, newPy];
    //                 state[px, newPy] = temp;
    //                 newPy = py;
    //                 break;

    //             case "left":
    //                 newPx = px - 1;
    //                 temp = state[px, py];
    //                 state[px, py] = state[newPx, py];
    //                 state[newPx, py] = temp;
    //                 newPx = px;
    //                 break;

    //             case "right":
    //                 newPx = px + 1;
    //                 swap(agentPos, agentPos +1)
    //                 temp = state[px, py];
    //                 state[px, py] = state[newPx, py];
    //                 // state[newPx, py] = temp;
    //                 newPx = px;
    //                 break;
    //         }
//}

// public static void states()
//         {
//             var goalState = new char[,] {
//             {'0','0', '0', '0'},
//             {'0', 'A', '0', '0'},
//             {'0', 'B', '0','0'},
//             {'0', 'C', '0', '*'}
//             };

//             var startState = new char[,] {
//             {'0', '0','0', '0'},
//             {'0', '0', '0', '0'},
//             {'0', '0', '0', '0'},
//             {'A', 'B', 'C', '*'}
//             };

//             var startState1 = new char[,] {
//             {'0', '0', '0', '0'},
//             {'0', 'A', '0', '0'},
//             {'0', 'B', '0', '0'},
//             {'0', '*', '0', 'C'}
//             };
//         }


// public struct Position
//         {
//             public int x, y;
//             public Position(int agentx, int agenty)
//             {
//                 x = agentx;
//                 y = agenty;
//             }

//         }

/// <summary>
/// Moves the agent in the given direction, modifying this instances state.
/// </summary>
// public void MoveAgent(string direction, List<(int x, int y)> positions)
// {
//     foreach ((int x, int y) pos in positions)
//     {
//         if (isLegal(pos)) 
//         {
//             Swap(pos);
//         }
//     }
// }


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

// public static List<Node> Moves(List<Node> moves, string direction) 
        // {
        //     // If the agent can't move it will return the old list
        //     if (!(moves[moves.Count - 1].MoveAgent(direction).Length < 2))
        //     {
        //         // creates new node ffrom last element in list with the next move
        //         // Node newN = new Node(moves[moves.Count - 1].MoveAgent(direction), moves[moves.Count - 1].nextMove);
        //         // moves.Add(newN);
        //     }

        //     return moves;

        // }