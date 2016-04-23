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
        public GameScreen(ContentManager contentManager, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //hier werden dann die Assets reingeladen
        }
    }
}
