namespace WinFormShogi
{
    public struct Fugou
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Fugou(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Fugou operator +(Fugou v1, Fugou v2)
        {
            return new Fugou(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Fugou operator -(Fugou v1, Fugou v2)
        {
            return new Fugou(v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}
