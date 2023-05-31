using System.Xml.Serialization;

namespace lab4
{
    public class Polyline: AbstractPolyline,  IEquatable<Polyline>
    {

        public Polyline() : base()
        {
        }
        public Polyline(Point point) : base(point)
        {
        }
        
        public Polyline(IEnumerable<Point> items) : base(items)
        {
        }
        public Polyline(params Point[] items) : this((IEnumerable<Point>) items)
        {
        }
        public void Serialize(string fileName)
        {
            var serializer = new XmlSerializer(typeof(Polyline));
            using var myFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
            serializer.Serialize(myFileStream, this);
        }

        public static Polyline Deserialize(string fileName)
        {
            var polylineSerialize = new XmlSerializer(typeof(Polyline));
            using var myFileStream = new FileStream(fileName, FileMode.Open);
            var polyline = polylineSerialize.Deserialize(myFileStream) as Polyline;
            return polyline!;
        }
        public override void Add(Point point)
        {
            if(Tail is null)
            {
                _vertices = new[] { point };
                Head = Tail = new Link {Value = point};
            }
            else
            {
                Array.Resize(ref _vertices, _vertices.Length + 1);
                _vertices[^1] = point;
                Tail.Next = new Link
                {
                    Value = point, Previous = Tail,
                    SelfCrossing = SelfCrossing
                };
                Tail = Tail.Next;
                if (SelfCrossing) return;
                for(var i = 0; i < _vertices.Length - 3; i++)
                {
                    if (!HasCrossing(_vertices[i], _vertices[i + 1], _vertices[^2],
                            _vertices[^1])) continue;
                    Tail.SelfCrossing = true;
                    break;
                }
                if(SelfCrossing) return;
                    for(var i = 0; i < _vertices.Length - 2; i++)
                {
                    if (!_vertices[^1].IsInSegment(_vertices[i], _vertices[i + 1])) continue;
                    Tail.SelfCrossing = true;
                    break;
                }
            }
        }
       
        public override void Remove()
        {
            if (Tail == null)
                return;
            Tail = Tail.Previous;
            Array.Resize(ref _vertices, _vertices.Length - 1);
            if (Tail is not null)
            {
                Tail.Next = null;
            }
            else
            {
                Head = null;
            }
        }
        public override float Lenght()
        {
            if (Head is null)
                return 0;
            float answer = 0;
            var current = Head;
            while(current.Next != null)
            {
                answer += current.Value!.DistTo(current.Next.Value!);
                current = current.Next;
            }
            return answer;
        }
        public bool Equals(Polyline? polyline)
        {
            if(Head == null || polyline?.Head == null)
                return polyline?.Head == null && Head == null;
            
            var current = Head;
            var curDif = Head.Value! - polyline.Head.Value!;
            foreach (var l in polyline)
            {
                if(current is null)
                    return false;
                var dif = current.Value! - l;
                if (dif != curDif)
                    return false;
                current = current.Next;
            }
            return current == null;
        }
       

        public static Polyline operator +(Polyline polyline , Vector vector)
        {
            var tempPolyline = new Polyline();
            foreach (var l in polyline)
            {
                tempPolyline.Add(l + vector);
            }
            return tempPolyline;
        }
        public static Polyline operator *(Polyline polyline, float number)
        {
            var tempPolyline = new Polyline();
            foreach (var l in polyline)
            {
                tempPolyline.Add(l * number);
            }
            return tempPolyline;
        }
        public static bool operator ==(Polyline polyline1, Polyline polyline2)
        { 
            var current = polyline2.Head;
            foreach (var l in polyline1)
            {
                if (current is null)
                    return false;
                if (current.Value! != l)
                    return false;
                current = current.Next;
            }
            return current == null; 
        }
        public static bool operator !=(Polyline polyline1, Polyline polyline2)
        {
            var current = polyline2.Head;
            foreach (var l in polyline1)
            {
                if (current is null)
                    return true;
                if (current.Value! != l)
                    return true;
                current = current.Next;
            }
            return current != null;
        }
    }
}