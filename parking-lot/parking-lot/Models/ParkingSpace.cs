using parking_lot.Models.Exception;
using System.Collections.Generic;

namespace parking_lot.Models
{
    public struct ParkingSpaceStatus
    {
        public int slotNo;
        public string licenseNumber;
        public string color;
    }

    public sealed class ParkingSpace
    {
        private readonly Dictionary<SlotSize, Queue<Slot>> availableSlots;
        private readonly Dictionary<Color, Queue<Slot>> colorMapedUsedSlots;
        private readonly Dictionary<string, Slot> licenseMapedUsedSlots;

        public int Id
        {
            get
            {
                return this.GetHashCode();
            }
        }

        public ParkingSpace()
        {
            availableSlots = new Dictionary<SlotSize, Queue<Slot>>();
            licenseMapedUsedSlots = new Dictionary<string, Slot>();
            colorMapedUsedSlots = new Dictionary<Color, Queue<Slot>>();
        }

        public void CreateSlot(SlotSize size, int count)
        {
            if (!availableSlots.ContainsKey(size))
            {
                var queue = new Queue<Slot>(count);
                for (int i = 0; i < count; i++)
                    queue.Enqueue(new Slot(i + 1, Id, size));
                availableSlots.Add(size, queue);
            }
        }

        public Slot GetParkedSlot(string license)
        {
            licenseMapedUsedSlots.TryGetValue(license, out Slot slot);
            return slot;
        }

        public bool Park(Vehical vehical)
        {
            try
            {
                if (!IsFull(vehical.size))
                {
                    ParkVehical(vehical);
                    return true;
                }
            }
            catch(ParkingException ex) { System.Console.WriteLine(ex.Message); }

            return false;
        }

        public void UnPark(string license)
        {
            UnPark(GetParkedSlot(license));
        }

        public Slot[] GetSlotsByVehicalColor(Color color)
        {
            if(colorMapedUsedSlots.ContainsKey(color))
            {
                var queue = colorMapedUsedSlots[color];
                if(queue.Count > 0)
                {
                    return queue.ToArray();
                }
            }

            return null;
        }

        public ParkingSpaceStatus[] GetStatus()
        {
            if(licenseMapedUsedSlots.Count > 0)
            {
                int i = 0;
                var slots = new ParkingSpaceStatus[licenseMapedUsedSlots.Count];
                foreach(var slot in licenseMapedUsedSlots.Values)
                {
                    slots[i++] = new ParkingSpaceStatus()
                    {
                        licenseNumber = slot.vehical.licenseNumber,
                        slotNo = slot.slotId,
                        color = slot.vehical.color.ToString()
                    };
                }

                return slots;
            }
            return null;
        }

        public int GetFreeSlotCount(SlotSize size) => availableSlots.ContainsKey(size) ?
            availableSlots[size].Count : 0;

        private void UnPark(Slot slot)
        {
            if (slot != null)
            {
                licenseMapedUsedSlots.Remove(slot.vehical.licenseNumber);
                colorMapedUsedSlots.Remove(slot.vehical.color);
                slot.vehical.UnPark();
                slot.UnPark();
                availableSlots[slot.size].Enqueue(slot);
            }
        }

        private void ParkVehical(Vehical vehical)
        {
            var slot = GetFreeSlot(vehical.size);
            slot.Park(vehical);
            vehical.Park(slot);

            AddToLicenseMaped(vehical.licenseNumber, slot);
            AddToColorMaped(vehical.color, slot);
        }

        private Slot GetFreeSlot(SlotSize size) => availableSlots[size].Dequeue();

        private bool IsFull(SlotSize size)
        {
            if (availableSlots.ContainsKey(size))
                return availableSlots[size].Count == 0;
            throw new System.Exception("Slot " + size + " Has not configured in Database");
        }

        private void AddToColorMaped(Color color, Slot slot)
        {
            Queue<Slot> queue = null;

            if (colorMapedUsedSlots.ContainsKey(color))
                queue = colorMapedUsedSlots[color];
            else
            {
                queue = new Queue<Slot>();
                colorMapedUsedSlots.Add(color, queue);
            }
            queue.Enqueue(slot);
        }

        private void AddToLicenseMaped(string license, Slot slot)
        {
            if (licenseMapedUsedSlots.ContainsKey(license))
                throw new ParkingException("Vehical " + license + " is Parked already");
            licenseMapedUsedSlots.Add(license, slot);
        }
    }
}
