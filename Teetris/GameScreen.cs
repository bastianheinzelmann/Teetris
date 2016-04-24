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
        Texture2D BackGround;

        TetrisBox TheTetrisBox;

        public GameScreen(ContentManager contentManager, EventHandler theScreenEvent) : base(theScreenEvent)
        {         
            BlockTextures[0] = contentManager.Load<Texture2D>("yellowBlock");
            BlockTextures[1] = contentManager.Load<Texture2D>("blueBlock");
            BlockTextures[2] = contentManager.Load<Texture2D>("greenBlock");
            BlockTextures[3] = contentManager.Load<Texture2D>("redBlock");

            BackGround = contentManager.Load<Texture2D>("tetBack");

            TheTetrisBox = new TetrisBox(BlockTextures);
        }

        public override void Update(GameTime theTime)
        {
            TheTetrisBox.Update(theTime);

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround, new Rectangle(10, 6 + 2 * 32, 32 * 10, 32 * 20), Color.White);
            TheTetrisBox.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
