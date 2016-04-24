
namespace Teetris
{
    public class Cell
    {
        public int color;
        public bool active;
        public bool occupied;

        public Cell (int col, bool act, bool occ)
        {
            color = col;
            active = act;
            occupied = occ;
        }
    }

    public struct Tuple
    {
        public int x;
        public int y;

        public Tuple(int X,int Y)
        {
            x = X;
            y = Y;
        }
    }
}
