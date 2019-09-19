using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using parking_lot.Models;

namespace parking_lot.Tests
{
    public class TestParkingALot
    {
        parking_lot.Models.ParkingaLot manager;

        [SetUp]
        public void Setup()
        {
            manager = new Models.ParkingaLot();
        }

        [Test]
        public void Test_Park_Vehical()
        {
            var car = new Car("KA05", SlotSize.L, Color.Blue);

            manager.Park(car);

            var list = manager.GetParkedVehicalsByColor(Color.Blue);

            Assert.AreEqual(1, list.Length);
        }

        [Test]
        public void Test_UnPark_Vehical()
        {
            var car = new Car("KA05", SlotSize.L, Color.Blue);

            manager.Park(car);

            var list = manager.GetParkedVehicalsByColor(Color.Blue);

            Assert.AreEqual(1, list.Length);

            manager.UnPark(car);

            list = manager.GetParkedVehicalsByColor(Color.Blue);

            Assert.AreEqual(0, list.Length);
        }
    }
}
