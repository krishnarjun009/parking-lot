using parking_lot.Models.Exception;
using System;
using System.Collections.Generic;

namespace parking_lot.Models
{
    public class ParkingaLot
    {
        private readonly Dictionary<int, ParkingSpace> parkingSpaces;

        public ParkingaLot()
        {
            parkingSpaces = new Dictionary<int, ParkingSpace>();
            Init(ParkingLotConfig.GetParkingSpaces());
        }

        public void Park(Vehical vehical)
        {
            var space = TryPark(vehical);
            if (space != null)
            {
                Console.WriteLine("Vehical Parked at " + space.Id + "-" + vehical.slot.size + "" + vehical.slot.slotId);
            }
        }

        public void UnPark(Vehical vehical)
        {
            if (vehical != null && vehical.slot != null)
            {
                var space = GetSpace(vehical.slot.parkingSpaceId);
                space.UnPark(vehical.licenseNumber);
                Console.WriteLine("Unparked from " + space.Id + "-" + vehical.slot.size + "" + vehical.slot.slotId);
            }
        }

        public string[] GetParkedVehicalsByColor(Color color)
        {
            var list = new List<string>();
            var values = parkingSpaces.Values;
            foreach (var space in values)
            {
                var slots = space.GetSlotsByVehicalColor(color);
                if (slots != null)
                {
                    foreach (var slot in slots)
                        list.Add(slot.vehical.licenseNumber);
                }
            }
            return list.ToArray();
        }

        public int[] GetParkedSlotsByVehicalColor(Color color)
        {
            var list = new List<int>();
            var values = parkingSpaces.Values;
            foreach (var space in values)
            {
                var slots = space.GetSlotsByVehicalColor(color);
                if (slots != null)
                {
                    foreach (var slot in slots)
                        list.Add(slot.slotId);
                }
            }
            return list.ToArray();
        }

        public string GetParkedVehicalSlotNumber(string license)
        {
            var values = parkingSpaces.Values;
            foreach (var space in values)
            {
                var slot = space.GetParkedSlot(license);
                if (slot != null)
                    return GetParkingId(slot.parkingSpaceId, slot.slotId, slot.size);
            }

            throw new ParkingException("License " + license + " Not found in the parking");
        }

        private ParkingSpace TryPark(Vehical vehical)
        {
            foreach(var space in parkingSpaces.Values)
            {
                if (space.Park(vehical))
                    return space;
            }

            return null;
        }

        private void Init(ParkingSpace[] parkingSpace)
        {
            if (parkingSpace == null)
                return;
            foreach (var space in parkingSpace)
                parkingSpaces.Add(space.Id, space);
        }

        private string GetParkingId(int spaceId, int slotId, SlotSize size)
        {
            return spaceId + "-" + size + "" + slotId;
        }

        private ParkingSpace GetSpace(int id)
        {
            parkingSpaces.TryGetValue(id, out ParkingSpace space);
            return space;
        }
    }
}
