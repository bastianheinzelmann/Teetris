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

        private int[,] box = new int[BoxSizeX, BoxSizeY];

        private Texture2D[] BlockTextures;

        private int BlockSize = 32; //Größe der Blöcke in Pixeln

        public TetrisBox(Texture2D[] blockTextures)
        {
            BlockTextures = blockTextures;
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

        }
    }
}
