namespace lab4
{
    public class Point : IEquatable<Point>, ICloneable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Point()
        {
            X = 0; Y = 0;
        }
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(Point other)
        {
            return other.X * this.Y == other.Y * this.X;
        }
        public bool IsInSegment(Point point1, Point point2)
        {
            if (point1 is null || point2 is null )
            {
                return false;
            }
            if (this.X > point1.X && this.X > point2.X)
                return false;
            if (this.X < point1.X && this.X < point2.X)
                return false;
            if (this.Y > point1.Y && this.Y > point2.Y)
                return false;
            if (this.Y < point1.Y && this.Y < point2.Y)
                return false;
            if((this.X - point1.X) * (point2.Y - point1.Y) == (this.Y - point1.Y) * (point2.X - point1.X))
                return true;
            return false;
        }
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y;
        }
        public static bool operator !=(Point point1, Point point2)
        {
            return point1.X != point2.X || point1.Y != point2.Y;
        }
        public static Point operator -(Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }
        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }
        public static Point operator +(Point point1, Vector point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }
        public static Point operator *(Point point1, int x)
        {
            return new Point(point1.X * x, point1.Y * x);
        }
        public static Point operator *(Point point1, float x)
        {
            return new Point(point1.X * x, point1.Y * x);
        }
        public float DistTo(Point point)
        {
           var ans = (float)Math.Sqrt((point.X - this.X) * (point.X - this.X) + (point.Y - this.Y) * (point.Y - this.Y));
           return ans;
        }
        public override string ToString() => $"[{this.X + ", " + this.Y }]";
        public object Clone()
        {
            return new Point(X, Y);
        }    
    }
}
