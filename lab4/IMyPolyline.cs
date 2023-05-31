namespace lab4
{
    public interface IMyPolyline
    {
        void Add(Point point);
        void Remove();
        float Lenght();
        bool PassThrough(Point point);
        Point[] Vertices { get;}
        bool SelfCrossing { get;}
    }
}
