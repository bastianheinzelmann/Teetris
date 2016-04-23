using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Teetris
{


    class GameScreen : Screen
    {
        Texture2D[] BlockTextures = new Texture2D[4];

        TetrisBox TheTetrisBox;

        public GameScreen(ContentManager contentManager, EventHandler theScreenEvent) : base(theScreenEvent)
        {         
            BlockTextures[0] = contentManager.Load<Texture2D>("yellowBlock");
            BlockTextures[1] = contentManager.Load<Texture2D>("blueBlock");
            BlockTextures[2] = contentManager.Load<Texture2D>("greenBlock");
            BlockTextures[3] = contentManager.Load<Texture2D>("redBlock");

            TheTetrisBox = new TetrisBox(BlockTextures);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            TheTetrisBox.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
