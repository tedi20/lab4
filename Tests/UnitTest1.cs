using lab4;

namespace Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void CheckSelfCrossing()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var point4 = new Point();
            var point5 = new Point(3 , 0);
            var point6 = new Point(1 , 0);
            var point7 = new Point(4 , 1);

            Polyline polyline = new();
            polyline.Remove();
            Assert.That(!polyline.SelfCrossing);        
            polyline.Add(point1);
            Assert.That(!polyline.SelfCrossing);
            polyline.Add(point2);
            Assert.That(!polyline.SelfCrossing);
            polyline.Add(point3);
            Assert.That(!polyline.SelfCrossing);
            polyline.Add(point1);
            Assert.That(polyline.SelfCrossing);
            polyline.Add(point4);
            Assert.That(polyline.SelfCrossing);
            polyline.Remove();
            Assert.That(polyline.SelfCrossing);
            polyline.Remove();
            Assert.That(!polyline.SelfCrossing);
            polyline.Add(point5);
            Assert.That(!polyline.SelfCrossing);
            polyline.Add(point6);
            Assert.That(!polyline.SelfCrossing);
            polyline.Add(point7);
            Assert.That(polyline.SelfCrossing);
            polyline.Remove();
            polyline.Add(point5);
            Assert.That(polyline.SelfCrossing);
            polyline.Remove();
            point7 = new Point(2, 0);
            polyline.Add(point7);
            Assert.That(polyline.SelfCrossing);
        }
        [Test]
        public void CheckPolylineConstruct()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var polyline1 = new Polyline();
            var polyline2 = new Polyline(point1);
            var points = new[] { point1, point2, point3 };
            var emptyPoints = new Point[3];
            var polyline3 = new Polyline(points);
            Assert.That(polyline3.Vertices[0] == point1);
            Assert.That(polyline3.Vertices[1] == point2);
            Assert.That(polyline3.Vertices[2] == point3);
            polyline3 = new Polyline(emptyPoints);
            Assert.That(polyline3.Vertices.Length == 0);
        }

        [Test]
        
        public void CheckPassThrough()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var point4 = new Point((float)1.5, (float)2.5);
            var point5 = new Point(2, 6);
            var points = new[] { point1, point2, point3 };
            var polyline = new Polyline(points);
            Assert.That(polyline.PassThrough(point1));
            Assert.That(polyline.PassThrough(point2));
            Assert.That(polyline.PassThrough(point3));
            Assert.That(polyline.PassThrough(point4));
            
            Assert.That(!polyline.PassThrough(point5));
        }
        [Test]
                
        public void CheckForeach()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var points = new[] { point1, point2, point3 };
            var polyline = new Polyline(points);
            var i = 0;
            foreach (var point in polyline)
            {
                Assert.That(point == points[i]);
                i++;
            }
        }
        [Test]
        
        public void CheckLenght()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var polyline = new Polyline();
            Assert.That(polyline.Lenght() == 0);
            polyline.Add(point1);
            Assert.That(polyline.Lenght() == 0);
            polyline.Add(point3);
            Assert.That(polyline.Lenght() == 1);
            polyline.Add(point2);
            Assert.That(polyline.Lenght() == 2);
            polyline.Add(point1);
            Assert.That(polyline.Lenght() == 2 + (float)Math.Sqrt(2));
        }
        [Test]
        
        public void CheckClone()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var polyline = new Polyline();
            var clonPolyline = (Polyline)polyline.Clone();
            Assert.That(polyline.Vertices.Length == clonPolyline.Vertices.Length);
            polyline.Add(point1);
            polyline.Add(point2);
            polyline.Add(point3);
            clonPolyline = (Polyline)polyline.Clone();
            Assert.That(polyline.Vertices.Length == clonPolyline.Vertices.Length);
            Assert.That(polyline.Vertices[1] == clonPolyline.Vertices[1]);
            Assert.That(polyline.SelfCrossing == clonPolyline.SelfCrossing);
            polyline.Add(point1);
            Assert.That(polyline.Vertices.Length != clonPolyline.Vertices.Length);
            Assert.That(polyline.Vertices[1] == clonPolyline.Vertices[1]);
            Assert.That(polyline.SelfCrossing != clonPolyline.SelfCrossing);
            clonPolyline = (Polyline)polyline.Clone();
            Assert.That(polyline.Vertices.Length == clonPolyline.Vertices.Length);
            Assert.That(polyline.Vertices[1] == clonPolyline.Vertices[1]);
            Assert.That(polyline.SelfCrossing == clonPolyline.SelfCrossing);
        }

        [Test]

        public void CheckEquals()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var point4 = new Point(3, 3);
            var polyline1 = new Polyline(); 
            var polyline2 = new Polyline();
            Assert.That(polyline1.Equals(polyline2));
            polyline2.Add(point1);
            Assert.That(!polyline1.Equals(polyline2));
            polyline1.Add(point1);
            Assert.That(polyline1.Equals(polyline2));
            polyline2.Remove();
            Assert.That(!polyline1.Equals(polyline2));
            polyline2.Add(point1);
            Assert.That(polyline1.Equals(polyline2));
            polyline2.Add(point2);
            Assert.That(!polyline1.Equals(polyline2));
            polyline1.Add(point3);
            Assert.That(!polyline1.Equals(polyline2));
            polyline2.Remove();
            Assert.That(!polyline1.Equals(polyline2));
            polyline2.Remove();
            polyline2.Remove();
            polyline2.Add(point2);
            polyline2.Add(point4);
            Assert.That(polyline1.Equals(polyline2));
        }

        [Test]
        public void CheckToString()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var polyline = new Polyline();
            Assert.That(polyline.ToString() == "{}");
            polyline.Add(point1);
            Assert.That(polyline.ToString() == "{[1, 2]}");
            polyline.Add(point2);
            Assert.That(polyline.ToString() == "{[1, 2], [2, 3]}");
        }
        
        [Test]
        public void CheckOperator()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var point4 = new Point(3, 3);
            var point5 = new Point((float)1.5, 3);


            var polyline1 = new Polyline(new[] { point1, point3 });
            var polyline2 = new Polyline(new[] { point2, point4 });
            var polyline3 = new Polyline(new[] { point1, point3 });
            var polyline5 = new Polyline(new[] { point5, point4 });
            var vector = new Vector(1, 1);
            Assert.That(polyline1 == polyline3);
            Assert.That(polyline2 != polyline3);
            Assert.That(polyline2 == polyline1 + vector);
            Assert.That(polyline1 != polyline2 + vector);
            Assert.That(polyline5 == polyline1 * (float)1.5);
            
        }
        [Test]
        public void CheckVectorConstruct()
        {
            var vector1 = new Vector();
            var vector2 = new Vector(1, 2);
            Assert.That(vector1.X == 0 && vector1.Y == 0);
            Assert.That(vector2.X == 1 && vector2.Y == 2);
        }
        [Test]
        public void CheckSerializeAndDeserialize()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = new Point(2, 2);
            var polyline = new Polyline(new[]{point1, point2, point3});
            polyline.Serialize("s.xml");
            var polyline2 = Polyline.Deserialize("s.xml");
            Assert.That(polyline == polyline2);
        }
        [Test]

        public void CheckPointOperators()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(1, 2);
            var point3 = new Point();
            var point4 = new Point(2, 4);
            var point5 = new Point();
            Assert.That(point1 == point2);
            Assert.That(point1 != point3);
            Assert.That(point3 == point1 - point2);
            Assert.That(point4 == point1 + point2);
            var vector = new Vector(1, 2);
            Assert.That(point4 == point1 + vector);
            Assert.That(point5 == point3 * 54);
            Assert.That(point4 == point2 * 2);
            Assert.That(point4 != point1 * 3);
            Assert.That(point1 == point4 * (float)0.5);
        }
        [Test]

        public void CheckPointToString()
        {
            var point1 = new Point(1, 2);
            Assert.That(point1.ToString() == "[1, 2]");
            var point2 = new Point();
            Assert.That(point2.ToString() == "[0, 0]");
        }
        [Test]

        public void CheckPointIsInSegment()
        {
            Point? point1, point2, point3;
            point3 = new Point();
            point2 = null;
            point1 = new Point();
            Assert.That(!point1.IsInSegment(point2, point3));
            point3 = null;
            point2 = new Point();
            Assert.That(!point1.IsInSegment(point2, point3));
            point3 = new Point(1, 2);
            Assert.That(point1.IsInSegment(point2, point3));
            point1 = new Point(-1, -1);
            Assert.That(!point1.IsInSegment(point2, point3));
            point2 = new Point(-3, -4);
            Assert.That(point1.IsInSegment(point2, point3));
            
        }
        [Test]

        public void CheckPointEquals()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 4);
            Assert.That(point1.Equals(point2));
            var point3 = new Point((float)0.5, 1);
            Assert.That(point3.Equals(point2));
        }
        [Test]

        public void CheckPointClone()
        {
            var point1 = new Point(1, 2);
            var point2 = (Point)point1.Clone();
            Assert.That(point2 == point1);
            var point3 = new Point(1, 2);
            Assert.That(point2 == point1);
        }
        [Test]

        public void CheckHasCrossing()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(1, 4);
            var point3 = new Point(2, 2);
            var point4 = new Point(2, 4);
            // first if (A.X == B.X && C.X == D.X) 
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(1, 5);
            point4 = new Point(1, 8);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(1, 1);
            point4 = new Point(1, 0);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(1, 3);
            point4 = new Point(1, 8);
            Assert.That(Polyline.HasCrossing(point1, point2, point3, point4));
            // second if(A.X == B.X)
            point3 = new Point(1, 3);
            point4 = new Point(1, 0);
            Assert.That(Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 3);
            point4 = new Point(-1, 0);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(5, 3);
            point4 = new Point(3, 0);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 4);
            point4 = new Point(2, 5);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 3);
            point4 = new Point(2, 5);
            Assert.That(Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 3);
            point4 = new Point(2, (float)5.00001);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 3);
            point4 = new Point(2, 0);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 3);
            point4 = new Point(2, 1);
            Assert.That(Polyline.HasCrossing(point1, point2, point3, point4));
            point3 = new Point(0, 3);
            point4 = new Point(2, (float)0.99999);
            Assert.That(!Polyline.HasCrossing(point1, point2, point3, point4));
            // third if(C.X == D.X)
            point3 = new Point(1, 3);
            point4 = new Point(1, 0);
            Assert.That(Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 3);
            point4 = new Point(-1, 0);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(5, 3);
            point4 = new Point(3, 0);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 4);
            point4 = new Point(2, 5);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 3);
            point4 = new Point(2, 5);
            Assert.That(Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 3);
            point4 = new Point(2, (float)5.00001);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 3);
            point4 = new Point(2, 0);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 3);
            point4 = new Point(2, 1);
            Assert.That(Polyline.HasCrossing(point3, point4, point1, point2));
            point3 = new Point(0, 3);
            point4 = new Point(2, (float)0.99999);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            // tourth 
            point1 = new Point(1, 1); 
            point2 = new Point(5, 1);
            point3 = new Point(2, 2);
            point4 = new Point(5, 2);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point1 = new Point(1, 2); 
            point2 = new Point(3, 4);
            point3 = new Point(4, 8);
            point4 = new Point(8, 12);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point1 = new Point(1, 2); 
            point2 = new Point(2, 3);
            point3 = new Point(3, 4);
            point4 = new Point(4, 5);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point1 = new Point(1, -2); 
            point2 = new Point();
            point3 = new Point(-1, 2);
            point4 = new Point(-3, 6);
            Assert.That(!Polyline.HasCrossing(point3, point4, point1, point2));
            point1 = new Point(1, 2); 
            point2 = new Point(3, 4);
            point3 = new Point(2, 3);
            point4 = new Point(4, 5);
            Assert.That(Polyline.HasCrossing(point3, point4, point1, point2));
            point1 = new Point(1, 2); 
            point2 = new Point(2, 3);
            point3 = new Point(2, 3);
            point4 = new Point(4, 5);
            Assert.That(Polyline.HasCrossing(point3, point4, point1, point2));

        }
    }
}