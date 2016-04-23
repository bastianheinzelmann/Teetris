using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Teetris
{
    class TetrisBox
    {
        private const int BoxSizeY = 22; //Größe des Spielfeldes
        private const int BoxSizeX = 10;

        private int[,] Field = new int[BoxSizeX, BoxSizeY];
        //private Block[,] GameBox = new Block[BoxSizeX, BoxSizeY];

        private Texture2D[] BlockTextures;

        private int BlockSize = 32; //Größe der Blöcke in Pixeln

        public TetrisBox(Texture2D[] blockTextures)
        {
            BlockTextures = blockTextures;
            Field[0, 0] = 1;
        }

        public void CreateTetrominos()
        {

        }

        public void HolyShit()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < Field.GetLength(0); i++)
            {
                for(int j = 0; j < Field.GetLength(1); j++)
                {
                    switch (Field[i,j])
                    {
                        case 1:
                            {
                                spriteBatch.Draw(BlockTextures[0], new Rectangle(i,j, 32,32), Color.White);
                                break;
                            }
                        case 2:
                            {
                                spriteBatch.Draw(BlockTextures[1], new Rectangle(i, j, 32, 32), Color.White);
                                break;
                            }
                        case 3:
                            {
                                spriteBatch.Draw(BlockTextures[2], new Rectangle(i, j, 32, 32), Color.White);
                                break;
                            }
                        case 4:
                            {
                                spriteBatch.Draw(BlockTextures[3], new Rectangle(i, j, 32, 32), Color.White);
                                break;
                            }
                    }
                }
            }

            //foreach(Block block in GameBox)
            //{
            //    if(block != null)
            //        block.Draw(spriteBatch);
            //}
        }
    }
}
