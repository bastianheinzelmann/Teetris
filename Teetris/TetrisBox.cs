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

        private const int BoxSizeY = 20; //Größe des Spielfeldes
        private const int BoxSizeX = 10;

        private Cell[,] Status = new Cell[BoxSizeX,BoxSizeY];


        private Texture2D[] BlockTextures;

        private int BlockSize = 32; //Größe der Blöcke in Pixel

        private int CoolDown = 0;

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

            Console.WriteLine(whichTetromino);

            switch(whichTetromino)
            {
                case 0:
                    {
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false);
                        break;
                    }

                case 1:
                    {
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false);
                        break;
                    }
                case 2:
                    {
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 3] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 3] = new Cell(color, true, false);
                        break;
                    }
                case 3:
                    {
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 3] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 3] = new Cell(color, true, false);
                        break;
                    }
                case 4:
                    {
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 3] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 4] = new Cell(color, true, false);
                        break;
                    }
                case 5:
                    {
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 1] = new Cell(color, true, false);
                        Status[BoxSizeX / 2 - 1, BoxSizeY - 2] = new Cell(color, true, false);
                        Status[BoxSizeX / 2, BoxSizeY - 2] = new Cell(color, true, false);
                        break;
                    }
            }

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
            for (int i = 0; i < Status.GetLength(0); i++)
            {
                for (int j = 0; j < Status.GetLength(1); j++)
                {
                    if (Status[i,j] != null)
                    {
                        if(Status[i, j].active && j <= 0)
                        {
                            deactivateAll();
                        }

                        if (Status[i, j].active)
                        {
                            Cell tempCell = Status[i, j];
                            Status[i, j] = null;
                            Status[i, j - 1] = tempCell;
                        }
                    }
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            CoolDown += gameTime.ElapsedGameTime.Milliseconds;
            if(CoolDown >= 500)
            {
                CheckActiveCells();
                CoolDown = 0;
            }          
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            for (int i = 0; i < Status.GetLength(0); i++)
            {
                for (int j = 0; j < Status.GetLength(1); j++)
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
