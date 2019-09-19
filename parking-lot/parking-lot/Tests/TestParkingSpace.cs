using NUnit.Framework;
using parking_lot.Models;

namespace parking_lot.Tests
{
    public class TestParkingSpace
    {
        ParkingSpace parkingSpace; 

        [SetUp]
        public void Setup()
        {
            parkingSpace = new ParkingSpace();
        }

        [Test]
        public void Test_CreateSlot()
        {
            parkingSpace.CreateSlot(SlotSize.L, 5);

            Assert.AreEqual(5, parkingSpace.GetFreeSlotCount(SlotSize.L));
        }

        [Test]
        public void Test_Park_Vehical()
        {
            Test_CreateSlot();

            var car = new Car("KA05", SlotSize.L, Color.Blue);

            var isPark = parkingSpace.Park(car);

            Assert.IsTrue(isPark);
        }

        [Test]
        public void Test_UnPark_Vehical()
        {
            Test_CreateSlot();
            Test_Park_Vehical();

            parkingSpace.UnPark("KA05");

            Assert.AreEqual(5, parkingSpace.GetFreeSlotCount(SlotSize.L));
        }

        [Test]
        public void Test_GetSlotsByColor()
        {
            Test_CreateSlot();
            Test_Park_Vehical();

            var slots = parkingSpace.GetSlotsByVehicalColor(Color.Blue);

            Assert.AreEqual(1, slots.Length);
        }
    }
}
