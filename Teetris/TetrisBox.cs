using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Teetris
{
    class TetrisBox
    {
        private int ScreenResX = 480;   //Bildschirmauflösung
        private int ScreenResY = 720;

        private const int BoxSizeY = 22; //Größe des Spielfeldes
        private const int BoxSizeX = 10;

        private Cell[,] Status = new Cell[BoxSizeX,BoxSizeY];

        private Texture2D[] BlockTextures;

        private int BlockSize = 32; //Größe der Blöcke in Pixel

        private int HorizontalCoolDown = 0;
        private int CoolDown = 0;

        //Input
        KeyboardState kb;
        KeyboardState old_kb;

        private Vector2 CurrentTetroCenter;
        private Vector2[] CurrentBrickPos = new Vector2[4];

        private int CurrentTetromino;
        private int CurrentRotationState; //only important for I-Tetro, because its f****** symmetry

        //---------GamePlayStuff

        private bool landed = false;

        public TetrisBox(Texture2D[] blockTextures)
        {
            BlockTextures = blockTextures;
            //Field[0, 0] = 3;
            CreateTetrominos();
        }

        public void CreateTetrominos()
        {
            Random rnd = new Random();
            int whichTetromino = rnd.Next(0, 7);
            int color = rnd.Next(1, 5);

            whichTetromino = 4;
            CurrentTetromino = whichTetromino;

            //Console.WriteLine(whichTetromino);

            #region SwitchTetrominos

            switch (whichTetromino)
            {
                case 0:
                    {
                        //T
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2, BoxSizeY - 1);
                        Status[BoxSizeX / 2 + 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2 + 1, BoxSizeY - 2);
                        CurrentTetroCenter = new Vector2(BoxSizeX / 2, BoxSizeY - 2); new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 1);
                        break;
                    }

                case 1:
                    {
                        //O
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 1);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2, BoxSizeY - 1);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2, BoxSizeY - 2);
                        break;
                    }
                case 2:
                    {
                        //L
                        Status[BoxSizeX / 2 - 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 2, BoxSizeY - 2);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2, BoxSizeY - 1);
                        CurrentTetroCenter = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        break;
                    }
                case 3:
                    {
                        //L (inverted)
                        Status[BoxSizeX / 2 - 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 2, BoxSizeY - 1);
                        Status[BoxSizeX / 2 - 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2 - 2, BoxSizeY - 2);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2 , BoxSizeY - 2);
                        CurrentTetroCenter = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        break;
                    }
                case 4:
                    {
                        //I
                        Status[BoxSizeX / 2 - 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 2, BoxSizeY - 1);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 1);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2, BoxSizeY - 1);
                        Status[BoxSizeX / 2 + 1, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2 + 1, BoxSizeY - 1);
                        //CurrentTetroCenter = new Vector2(BoxSizeX / 2, BoxSizeY - 1); new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 1);
                        CurrentRotationState = 0;
                        break;
                    }
                case 5:
                    {
                        //Z
                        Status[BoxSizeX / 2 - 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 2, BoxSizeY - 1);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 1);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2, BoxSizeY - 2);
                        CurrentTetroCenter = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        break;
                    }
                case 6:
                    {
                        //Z (inverted)
                        Status[BoxSizeX / 2 - 2, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[0] = new Vector2(BoxSizeX / 2 - 2, BoxSizeY - 2);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false); CurrentBrickPos[1] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[2] = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 1);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false); CurrentBrickPos[3] = new Vector2(BoxSizeX / 2, BoxSizeY - 1);
                        CurrentTetroCenter = new Vector2(BoxSizeX / 2 - 1, BoxSizeY - 2);
                        break;
                    }
            }

            #endregion SwitchTetrominos

        }

        public void DrawCells(SpriteBatch spriteBatch, Texture2D texture, int x, int y)
        {
            spriteBatch.Draw(texture, new Rectangle(x * BlockSize + 10, ScreenResY - BlockSize - 10 - y * BlockSize, BlockSize, BlockSize), Color.White);
        }

        private void deactivateAll()
        {
            int counter = 0;

            foreach (Cell cell in Status)
            {
                if (cell != null)
                {
                    if (cell.active)
                    {
                        cell.active = false;
                        counter++;
                    }
                    if (counter >= 4)
                        return;
                }
            }
        }

        public void CheckActiveCells()
        {
            int brickCounter = 0;

            for (int y = 0; y < Status.GetLength(1); y++)
            {
                for (int x = 0; x < Status.GetLength(0); x++)
                {
                    if (Status[x,y] != null)
                    {

                        //Save positions of bricks in a Vector2
                        if (Status[x, y].active)
                        {
                            CurrentBrickPos[brickCounter] = new Vector2(x, y);
                            brickCounter++;
                        }

                        // hit the ground
                        if (Status[x, y].active && y <= 0)
                        {
                            deactivateAll();
                            landed = true;
                            return;
                        }

                        // land on other Block
                        if (y > 0)
                        {
                            if (Status[x, y - 1] != null)
                            {
                                if (Status[x, y].active && !Status[x, y - 1].active)
                                {
                                    deactivateAll();
                                    landed = true;
                                    return;
                                }
                            }
                        }
                        //------------------end                        
                    }
                }
            }

            for(int i = 0; i < 4; i++)
            {
                Cell tempCell = Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y];
                Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y] = null;
                Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y - 1] = tempCell;
                CurrentBrickPos[i].y--;
                //CurrentTetroCenter.y--;
            }

            CurrentTetroCenter.y--;
        }

        bool rotationCollisionCheck()
        {
            if (CurrentTetromino == 4)
                return true;

            int centreX = CurrentTetroCenter.x;
            int CentreY = CurrentTetroCenter.y;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (centreX + i < 0 || centreX + i > 9 || CentreY + j > 22 || CentreY + j < 0)
                        return false;
                

                    if (Status[centreX + i, CentreY + j] != null)
                        if (!Status[centreX + i, CentreY + j].active)
                            return false;
                }
            }

            return true;
        }

        bool IrotationCollisionCheck(int centreX, int centreY)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j > -4; j--)
                {
                    if (centreX + i < 0 || centreX + i > 9 || centreY + j > 22 || centreY + j < 0)
                        return false;


                    if (Status[centreX + i, centreY + j] != null)
                        if (!Status[centreX + i, centreY + j].active)
                            return false;
                }
            }

            return true;
        }

        void IRotation()
        {
            for (int i = 0; i < 4; i++)
            {
                Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y] = null;
            }
        }

        #region SortAlgos
        void sortBricksX()
        {
            int min;
            for (int i = 0; i < 4; i++)
            {
                min = i;
                for (int j = i; j < 4; j++)
                {
                    if (CurrentBrickPos[j].x < CurrentBrickPos[min].x)
                    {
                        min = j;
                    }
                }
                Vector2 temp = CurrentBrickPos[i];
                CurrentBrickPos[i] = CurrentBrickPos[min];
                CurrentBrickPos[min] = temp;   
            }
        }

        void sortBricksY()
        {
            int min;
            for (int i = 0; i < 4; i++)
            {
                min = i;
                for (int j = i; j < 4; j++)
                {
                    if (CurrentBrickPos[j].y < CurrentBrickPos[min].y)
                    {
                        min = j;
                    }
                }
                Vector2 temp = CurrentBrickPos[i];
                CurrentBrickPos[i] = CurrentBrickPos[min];
                CurrentBrickPos[min] = temp;
            }
        }
        #endregion SortAlgos


        public void Rotate()
        {
            foreach (Vector2 brick in CurrentBrickPos)
            {
                if (brick.y >= BoxSizeY - 2)
                    return;
            }

            if (!rotationCollisionCheck())
                return;

            Cell tempCell = Status[CurrentBrickPos[0].x, CurrentBrickPos[0].y];

            //if currentTetro is the O-Brick
            if (CurrentTetromino == 1)
                return;

            //if currentTetro is the I-Brick
            if (CurrentTetromino == 4)
            {
                #region IRotation
                switch (CurrentRotationState)
                {
                    case 0:
                        //transmission to state 1
                        sortBricksX();
                        if (!IrotationCollisionCheck(CurrentBrickPos[0].x, CurrentBrickPos[0].y + 1))
                            return;
                        IRotation();
                        CurrentBrickPos[0] += new Vector2(2, 1);
                        CurrentBrickPos[1] += new Vector2(1, 0);
                        CurrentBrickPos[2] += new Vector2(0, -1);
                        CurrentBrickPos[3] += new Vector2(-1, 2);

                        foreach (Vector2 brick in CurrentBrickPos)
                        {
                            Status[brick.x, brick.y] = tempCell;
                        }
                        CurrentRotationState++;
                        return;
                    //break;
                    case 1:
                        //transmission to state 2                       
                        sortBricksY();
                        bool flag = IrotationCollisionCheck(CurrentBrickPos[0].x - 2, CurrentBrickPos[0].y + 3);
                        Console.WriteLine(flag);
                        if (!IrotationCollisionCheck(CurrentBrickPos[0].x - 2, CurrentBrickPos[0].y + 3))
                            return;
                        IRotation();                      
                        CurrentBrickPos[0] += new Vector2(-2, 1);
                        CurrentBrickPos[1] += new Vector2(-1, 0);
                        CurrentBrickPos[2] += new Vector2(0, -1);
                        CurrentBrickPos[3] += new Vector2(1, -2);

                        foreach (Vector2 brick in CurrentBrickPos)
                        {
                            Status[brick.x, brick.y] = tempCell;
                        }
                        CurrentRotationState++;
                        return;

                    case 2:
                        //transmission to state 3
                        sortBricksX();
                        if (!IrotationCollisionCheck(CurrentBrickPos[0].x, CurrentBrickPos[0].y + 2))
                            return;
                        IRotation();
                        CurrentBrickPos[0] += new Vector2(1, 2);
                        CurrentBrickPos[1] += new Vector2(0, 1);
                        CurrentBrickPos[2] += new Vector2(-1, 0);
                        CurrentBrickPos[3] += new Vector2(-2, -1);
                        CurrentRotationState++;
                        foreach (Vector2 brick in CurrentBrickPos)
                        {
                            Status[brick.x, brick.y] = tempCell;
                        }
                        return;
                    case 3:
                        //transmission to state 4
                        sortBricksY();
                        if (!IrotationCollisionCheck(CurrentBrickPos[0].x - 1, CurrentBrickPos[0].y + 3))
                            return;
                        IRotation();
                        CurrentBrickPos[0] += new Vector2(-1, 2);
                        CurrentBrickPos[1] += new Vector2(0, 1);
                        CurrentBrickPos[2] += new Vector2(1, 0);
                        CurrentBrickPos[3] += new Vector2(2, -1);

                        foreach (Vector2 brick in CurrentBrickPos)
                        {
                            Status[brick.x, brick.y] = tempCell;
                        }
                        CurrentRotationState = 0;
                        return;
                }
                #endregion IRotation
            }



            for (int i = 0; i < 4; i++)
            {
                //Let's erase the old brickPos
                //Cell tempCell = Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y];
                Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y] = null;

                //Thats some weird unoptimized math :/
                CurrentBrickPos[i] = CurrentBrickPos[i] - CurrentTetroCenter;
                int xTemp = CurrentBrickPos[i].x;
                CurrentBrickPos[i].x = CurrentBrickPos[i].y;
                CurrentBrickPos[i].y = xTemp * -1;
                CurrentBrickPos[i] = CurrentBrickPos[i] + CurrentTetroCenter;
            }

            foreach (Vector2 brick in CurrentBrickPos)
            {
                Status[brick.x, brick.y] = tempCell;
            }

        }

        public void Move(int direction)
        {
            foreach(Vector2 brick in CurrentBrickPos)
            {
                if (brick.x == 0 && direction == -1)
                {
                    return;
                }

                if (brick.x == Status.GetLength(0) - 1 && direction == 1)
                {
                    return;
                }

                if(Status[brick.x + direction, brick.y] != null)
                {
                    if(!Status[brick.x + direction, brick.y].active)
                    {
                        return;
                    }
                }
            }


            HorizontalCoolDown = 0;
            Cell tempCell = Status[CurrentBrickPos[0].x, CurrentBrickPos[0].y];
            foreach (Vector2 brick in CurrentBrickPos)
            {
                Status[brick.x, brick.y] = null;
            }

            for (int i = 0; i < 4; i++)
            {
                CurrentBrickPos[i].x += direction;               
                Status[CurrentBrickPos[i].x, CurrentBrickPos[i].y] = tempCell;
            }

            //move Center
            CurrentTetroCenter.x += direction;
        }

        public void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();

            HorizontalCoolDown += gameTime.ElapsedGameTime.Milliseconds;
            CoolDown += gameTime.ElapsedGameTime.Milliseconds;

            if (CoolDown >= 500)
            {
                CheckActiveCells();
                CoolDown = 0;
            }

            // if the brick is landed set landed tor true, and the next brick is falling down
            if (landed)
            {
                CreateTetrominos();
                landed = false;
            }
            
            if(KeyPressed(Keys.Up))
            {
                Rotate();
            }

            if(KeyDown(Keys.Left) && HorizontalCoolDown >= 200)
            {
                Move(-1);
            }

            if(KeyDown(Keys.Right) && HorizontalCoolDown >= 200)
            {
                Move(1);
            }

            old_kb = kb;    
        }

        bool KeyPressed(Keys key) { return kb.IsKeyDown(key) && old_kb.IsKeyUp(key); }
        bool KeyDown(Keys key) { return kb.IsKeyDown(key); }

        public void Draw(SpriteBatch spriteBatch)
        {            
            for (int j = 0; j < Status.GetLength(1) - 2; j++)
            {
                for (int i = 0; i < Status.GetLength(0); i++)
                {
                    #region ColorSwitch
                    if(Status[i,j] != null)
                    {
                        switch (Status[i, j].color)
                        {
                            case 1:
                                {
                                    DrawCells(spriteBatch, BlockTextures[0], i, j);
                                    break;
                                }
                            case 2:
                                {
                                    DrawCells(spriteBatch, BlockTextures[1], i, j);
                                    break;
                                }
                            case 3:
                                {
                                    DrawCells(spriteBatch, BlockTextures[2], i, j);
                                    break;
                                }
                            case 4:
                                {
                                    DrawCells(spriteBatch, BlockTextures[3], i, j);
                                    break;
                                }
                        }
                    }
                    #endregion ColorSwitch
                }
            }
        }
    }
}
