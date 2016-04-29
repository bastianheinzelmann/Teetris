
namespace Teetris
{
    public class Cell
    {
        public int color;
        public bool active;
        public bool occupied;

        public Cell(int col, bool act, bool occ)
        {
            color = col;
            active = act;
            occupied = occ;
        }
    }

    public struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }
    }
}
