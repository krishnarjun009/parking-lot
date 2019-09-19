using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parking_lot.Models
{
    public enum SlotSize
    {
        S,
        M,
        L,
        XL
    }

    public enum Color
    {
        White,
        Red,
        Blue,
        Green
    }

    public class ParkingaLot
    {
        private readonly Dictionary<int, ParkingSpace> parkingSpaces;

        public ParkingaLot()
        {
            var parkingSpacesAvailable = ParkingLotConfig.GetParkingSpaces();
            parkingSpaces = new Dictionary<int, ParkingSpace>();
            foreach (var space in parkingSpacesAvailable)
                parkingSpaces.Add(space.Id, space);
        }

        public void Park(Vehical vehical)
        {
            var space = TryPark(vehical);
            if(space != null)
            {
                Console.WriteLine("Vehical Parked a " + vehical.slot.slotId);
            }
        }

        public void UnPark(Vehical vehical)
        {
            var space = parkingSpaces[vehical.slot.parkingSpaceId];
            space.UnPark(vehical.licenseNumber);
            Console.WriteLine("Unparked from " + vehical.slot.slotId);
        }

        private ParkingSpace TryPark(Vehical vehical)
        {
            foreach(var space in parkingSpaces.Values)
            {
                if (space.Park(vehical))
                    return space;
            }

            throw new Exception("No parking space is available at this moment");
        }
    }
}
