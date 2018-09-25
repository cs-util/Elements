using Hypar.Elements;
using Hypar.Geometry;
using System;
using System.Linq;
using System.IO;
using Xunit;

namespace Hypar.Tests
{
    public class FloorTests
    {
        [Fact]
        public void Example()
        {
            var p = Polygon.Rectangle(Vector3.Origin, 5, 5);
            var p1 = Polygon.Rectangle(new Vector3(3,2,0), 3, 1);
            var profile = new Profile(p, p1);
            var floor = new Floor(profile, 0.0, 0.2);
            var model = new Model();
            model.AddElement(floor);
            model.SaveGlb("floor.glb");
        }

        [Fact]
        public void Construct()
        {
            var p = Polygon.Rectangle();
            var profile = new Profile(p);
            var floor = new Floor(profile, 1.0, 0.2);
            Assert.Equal(1.0, floor.Elevation);
            Assert.Equal(0.2, floor.Thickness);
            Assert.Equal(floor.Transform.Origin.Z, 1.0);
        }

        [Fact]
        public void ZeroThickness_ThrowsException()
        {
            var model = new Model();
            var poly = Polygon.Rectangle(width:20, height:20);
            Assert.Throws<ArgumentOutOfRangeException>(()=> new Floor(new Profile(poly), 0.0, 0.0));
        }

        [Fact]
        public void Area()
        {
            // A floor with two holes punched in it.
            var p1 = Polygon.Rectangle(new Vector3(1,1,0), 1, 1);
            var p2 = Polygon.Rectangle(new Vector3(3,3,0), 1, 1);
            var profile = new Profile(Polygon.Rectangle(Vector3.Origin, 10, 10), new[]{p1,p2});
            var floor = new Floor(profile, 0.0, 0.2, BuiltInMaterials.Concrete);
            Assert.Equal(100.0-2.0, floor.Area());
        }
    }
}