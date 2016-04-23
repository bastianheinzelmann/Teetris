using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Teetris
{
    class Block
    {
        private Texture2D BlockTex;
        private Rectangle Rect;
        private int Size;

        public void SetPosition(int x, int y)
        {
            Rect.X = x;
            Rect.Y = y;
        }

        public Block(Texture2D texture, int size, Rectangle rect)
        {
            BlockTex = texture;
            Rect = rect;
            Size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlockTex, Rect, Color.White);
        }
    }

}
