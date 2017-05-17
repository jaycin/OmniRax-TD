using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace OmniLibrary.EnemyEngine
{
    public class aStar
    {
        MovementGrid Grid;
        
        //bool pathFound;
        public aStar(MovementGrid grid)
        {
            Grid = grid;
            
            
        }
        public Vector2 FindNextRectangle(int id)
        {
            int upperbound = 0, lowerbound = 0;
            //List<float> distance;
            //distance = new List<float>();
            //foreach(Rectangle r in Grid.lstSpawn)
            //{
            //    Vector2 target = new Vector2(Grid.SourceList[id].X+Grid.SourceList[id].Width/2,Grid.SourceList[id].Y+Grid.SourceList[id].Height/2);
            //    Vector2 origin= new Vector2(r.X +r.Width/2,r.Y+r.Height/2);
            //    distance.Add(Vector2.Distance(origin,target));
            //}
            //foreach(Rectangle r in Grid.lstSpawn)
            //{
            //    Vector2 target = new Vector2(Grid.SourceList[id].X+Grid.SourceList[id].Width/2,Grid.SourceList[id].Y+Grid.SourceList[id].Height/2);
            //    Vector2 origin= new Vector2(r.X +r.Width/2,r.Y+r.Height/2);
            //    if(Vector2.Distance(origin,target)==distance.Min())
            //    {
            //        return target;
            //    }
                

            //}
            // return Vector2.Zero;
            while (id > 32)
            {
                id -= 32;
                upperbound++;
            }
            lowerbound = id;
            int[] directionId = new int[8];

            // the 8 posible directions
            directionId[0] = lowerbound * (upperbound - 1);
            directionId[6] = lowerbound * (upperbound + 1);
            directionId[3] = (lowerbound - 1) * (upperbound - 1);
            directionId[4] = (lowerbound - 1) * upperbound;
            directionId[5] = (lowerbound - 1) * (upperbound + 1);
            directionId[7] = (lowerbound + 1) * upperbound;
            directionId[1] = (lowerbound + 1) * (upperbound + 1);
            directionId[2] = lowerbound + 1 * (upperbound - 1);
            float ShortestDirection = 10000;
            int ShortestId = 0;
            for (int i = 0; i < directionId.Length; i++)
            {
                if (directionId[i] > 1023 || directionId[i] < 0)
                {

                }
                else
                {
                    if (Grid.SquareList[directionId[i]].IsWalkable)
                    {
                        if (Grid.SquareList[directionId[i]].EndDistance < ShortestDirection)
                        {
                            ShortestDirection = Grid.SquareList[directionId[i]].EndDistance;
                            ShortestId = id;

                        }
                    }
                }
            }
           
            return new Vector2((int)Grid.SquareList[ShortestId].Origin.X, (int)Grid.SquareList[ShortestId].Origin.Y);
                
    
                

               

                
            




            

        }
        private enum status { ready, waiting, done ,notPath};
        public Queue<int> FindPath(int start , int end,bool useAll)
        {
            Queue<int> walkPath = new Queue<int>();
            int iPath = 50;

            int iRows = 32, iCols = 32;
            int iMax = iRows * iCols;
            int iFront = 0, iRear = 0;
            int[] path;
            int[] origin ;
            if (!useAll)
            {
                path = new int[iMax];
                origin = new int[iMax];

            }
            else
            {
                path = new int[iMax];
                origin = new int[iMax];
            }
            int[,] Maze=new int[iRows, iCols];
            int[,] MazeStatus = new int[iRows, iCols];
            //sets all walkable paths to ready
            if (!useAll)
            {
                for (int i = 0; i < iRows; i++)
                {
                    for (int j = 0; j < iCols; j++)
                    {

                        if (Grid.SquareList[j + (i * 32)].IsPath)
                        {
                            MazeStatus[i, j] = (int)status.ready;
                            Maze[i, j] = (int)status.ready;
                        }
                        else
                            MazeStatus[i, j] = (int)status.notPath;
                        Maze[i, j] = (int)status.notPath;
                    }
                }
            }
            else
            {
                for (int i = 0; i < iRows; i++)
                {
                    for (int j = 0; j < iCols; j++)
                    {

                       
                            MazeStatus[i, j] = (int)status.ready;
                            Maze[i, j] = (int)status.ready;
                        
                        
                    }
                }
            }
            path[iRear] = start;
            origin[iRear] = -1;
            iRear++;
            int iCurrent, iLeft, iRight, iTop, iDown, iRightDown,iRightUp, iLeftUp, iLeftDown;
            while (iFront != iRear)//while Q not empty
            {
                if (path[iFront] == end)//maze is solved
                    break;

                iCurrent = path[iFront];

                iLeft = iCurrent - 1;

                if (iRear < path.Length)
                {
                    if (iLeft >= 0 && iLeft / iCols == iCurrent / iCols)//left node exists
                    {
                        if (GetStatus(MazeStatus, iLeft) == (int)status.ready)
                        {
                            path[iRear] = iLeft; //add to Q;
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iLeft, (int)status.waiting);
                            iRear++;
                        }
                    }
                    iRight = iCurrent + 1;
                    if (iRight < iMax && iRight / iCols == iCurrent / iCols)//right Node exists
                    {
                        if (GetStatus(MazeStatus, iRight) == (int)status.ready)
                        {
                            path[iRear] = iRight;//add to Q
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iRight, (int)status.waiting);
                            iRear++;
                        }
                    }
                    iTop = iCurrent - iCols;
                    if (iTop >= 0 && iRear <= path.Length)
                    {
                        if (GetStatus(MazeStatus, iTop) == (int)status.ready)//top node is open
                        {
                            path[iRear] = iTop;
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iTop, (int)status.waiting);
                            iRear++;
                        }
                    }
                    iDown = iCurrent + iCols;
                    if (iDown < iMax && iRear <= path.Length)//if bottom node exists
                    {
                        if (GetStatus(MazeStatus, iDown) == (int)status.ready)
                        {
                            path[iRear] = iDown;
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iDown, (int)status.waiting);
                            iRear++;
                        }
                    }
                    iRightDown = iCurrent + iCols + 1;
                    if (iRightDown < iMax && iRightDown >= 0 && iRightDown / iCols == iCurrent / iCols + 1 )//right down node exists
                    {
                        if (GetStatus(MazeStatus, iRightDown) == (int)status.ready)//right down node is path
                        {
                            path[iRear] = iRightDown;//add to Q
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iRightDown, (int)status.waiting);
                            iRear++;
                        }
                    }
                    iRightUp = iCurrent - iCols + 1;
                    if (iRightUp >= 0 && iRightUp < iMax && iRightUp / iCols == iCurrent / iCols - 1)//if upper right node exists
                    {
                        if (GetStatus(MazeStatus, iRightUp) == (int)status.ready)// if node is path
                        {
                            path[iRear] = iRightUp;//add to Q
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iRightUp, (int)status.waiting);
                            iRear++;
                        }
                    }
                    iLeftDown = iCurrent + iCols - 1;
                    if (iLeftDown < iMax && iLeftDown >= 0 && iLeftDown / iCols == iCurrent / iCols + 1)//node exists
                    {
                        if (GetStatus(MazeStatus, iLeftDown) == (int)status.ready)
                        {
                            path[iRear] = iLeftDown;
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iLeftDown, (int)status.waiting);
                            iRear++;


                        }
                    }
                    iLeftUp = iCurrent - iCols - 1;
                    if (iLeftUp >= 0 && iLeftUp < iMax && iLeftUp / iCols == iCurrent / iCols - 1)
                    {
                        if (GetStatus(MazeStatus, iLeftUp) == (int)status.ready && iRear != 1024)
                        {
                            path[iRear] = iLeftUp;
                            origin[iRear] = iCurrent;
                            SetStatus(MazeStatus, iLeftDown, (int)status.waiting);
                            iRear++;
                        }
                    }
                   
                }
                //change status to current node to processed
                SetStatus(MazeStatus, iCurrent, (int)status.done);
                iFront++;
            }
            //create an array(maze) for solution
            int[,] mazeSolved = new int[iRows, iCols];
            for (int i = 0; i < iRows; i++)
            {
                for (int j = 0; j < iCols; j++)
                {
                    mazeSolved[i, j] = Maze[i, j];
                }
            }
            iCurrent = end;
            for (int i = iFront; i >= 0; i--)
            {
                if (path[i] == iCurrent)
                {
                    iCurrent = origin[i];
                    if (iCurrent == -1)
                    {
                        break;
                    }
                    SetStatus(mazeSolved, iCurrent, iPath);
                }
            }
            
            for (int i = 0; i < iRows; i++)
            {
                for (int j = 0; j < iCols; j++)
                {
                    if (mazeSolved[i, j] == iPath)
                    {
                        walkPath.Enqueue( j + i * iRows);
                    }
                }   
            }
            return walkPath;
        }
        public void SetStatus(int[,] Maze, int nodeId, int newVal)
        {
            int iCols = Maze.GetLength(1);
            Maze[nodeId/iCols,nodeId-nodeId/iCols*iCols] = newVal;

        }
        public int GetStatus(int[,] Maze, int nodeId)
        {
            int iCols = Maze.GetLength(1);
            return Maze[nodeId / iCols, nodeId - nodeId / iCols * iCols];
        }
        
    }

}
