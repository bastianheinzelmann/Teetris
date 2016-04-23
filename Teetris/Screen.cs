using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Teetris
{
    class Screen
    {
        protected EventHandler ScreenEvent;

        public Screen (EventHandler theScreenEvent)
        {
            ScreenEvent = theScreenEvent;
        }

        public virtual void Update(GameTime theTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void LoadContent()
        {

        }
    }
}
