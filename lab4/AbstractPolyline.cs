using System.Collections;
using System.Runtime.Serialization;

namespace lab4
{
    public abstract class AbstractPolyline : IMyPolyline, ICloneable, IEnumerable<Point>
    {
        protected  class Link
        {
            public Point? Value { get; set; }
            
            public Link? Next { get; set; }
            
            public Link? Previous { get; set;}
            public bool SelfCrossing { get; set; }
        }
        protected Link? Head;
        protected Link? Tail;
        [DataMember]
        protected Point[] _vertices;
        protected AbstractPolyline()
        {
            Head = null;
            Tail = null;
            _vertices = Array.Empty<Point>();
        }

        protected AbstractPolyline(IEnumerable<Point> items)
        {
            _vertices = Array.Empty<Point>(); 
            foreach (var item in items)
            {
                if(item is not null)
                    Add(item);
            }
        }

        protected AbstractPolyline(Point point)
        {
            _vertices = new Point[1];
            _vertices[0] = point;
            Head = Tail = new Link { Value = point };
        }
        public static bool HasCrossing(Point a, Point b, Point c, Point d)
        {
            float k1, k2, b1, b2, x, y;
            if (Math.Abs(a.X - b.X) < 0.000001 && Math.Abs(c.X - d.X) < 0.000001)
            {
                if (Math.Abs(a.X - c.X) > 0.000001)
                    return false;
                if(Math.Min(a.Y, b.Y) > Math.Max(c.Y, d.Y))
                    return false;
                if(Math.Min(c.Y, d.Y) > Math.Max(a.Y, b.Y))
                    return false;
                return true;
            }
            if(Math.Abs(a.X - b.X) < 0.000001)
            {
                if(Math.Min(c.X, d.X) > a.X)
                    return false;
                if (Math.Max(c.X, d.X) < a.X)
                    return false;
                k2 = (c.Y - d.Y) / (c.X - d.X);
                b2 = c.Y - k2 * c.X;
                y = k2 * a.X + b2;
                Console.WriteLine(y);
                if(y > Math.Max(a.Y, b.Y))
                    return false;
                if (y < Math.Min(a.Y, b.Y))
                    return false;
                return true;
            }
            if (Math.Abs(c.X - d.X) < 0.000001)
            {
                if(Math.Min(a.X, b.X) > c.X)
                    return false;
                if (Math.Max(a.X, b.X) < c.X)
                    return false;
                k1 = (a.Y - b.Y) / (a.X - b.X);
                b1 = a.Y - k1 * a.X;
                y = k1 * c.X + b1;
                Console.WriteLine(y);
                if(y > Math.Max(c.Y, d.Y))
                    return false;
                if (y < Math.Min(c.Y, d.Y))
                    return false;
                return true;
            }
            k1 = (a.Y - b.Y) / (a.X - b.X);
            b1 = a.Y - k1 * a.X;
            k2 = (c.Y - d.Y) / (c.X - d.X);
            b2 = c.Y - k2 * c.X;
            if (Math.Abs(k1 - k2) < 0.000001)
            {
                if (Math.Abs(b1 - b2) > 0.000001)
                    return false;
                if(Math.Min(c.X, d.X) > Math.Max(a.X, b.X))
                    return false;
                if(Math.Min(a.X, b.X) > Math.Max(c.X, d.X))
                    return false;
                return true;
            }
            x = (b2 - b1) / (k1 - k2);
            y = k1 * x + b1;
            if (x > Math.Max(a.X, b.X) || x < Math.Min(a.X, b.X))
                return false;
            if (x > Math.Max(c.X, d.X) || x < Math.Min(c.X, d.X))
                return false;
            return true;
        }
        public object Clone()
        {
            var clone = new Polyline();
            if(Head is not null)
            {
                var current = clone.Head = new Link { Value = Head.Value, SelfCrossing = Head.SelfCrossing};
                for(var link = Head.Next; link is not null; link = link.Next)
                {
                    current.Next = new Link { Value = link.Value , Previous = current, SelfCrossing = SelfCrossing};
                    current = current.Next;
                }
                clone.Tail = current;
            }
            clone._vertices = _vertices;
            return clone;
        }
        public IEnumerator<Point> GetEnumerator()
        {
            for (var current = Head; current is not null; current = current.Next)
            {
                yield return current.Value;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public bool PassThrough(Point point)
        {
            for(var current = Head; current?.Next != null; current = current.Next)
            {
                if (point.IsInSegment(current.Value!, current.Next.Value!))
                    return true;
            }
            return false;
        }
        public abstract void Add(Point point);
        public abstract void Remove();
        public abstract float Lenght();
        public Point[] Vertices { get { return _vertices; } }
        public bool SelfCrossing => Tail is not null && Tail.SelfCrossing;
        public override string ToString() => $"{{{string.Join(", ", this)}}}";
    }
}
