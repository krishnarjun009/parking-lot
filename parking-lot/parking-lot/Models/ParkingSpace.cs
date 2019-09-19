using System.Collections.Generic;

namespace parking_lot.Models
{
    public sealed class ParkingSpace
    {
        public int Id
        {
            get
            {
                return this.GetHashCode();
            }
        }

        private readonly Dictionary<SlotSize, Queue<Slot>> availableSlots;

        private readonly Dictionary<Color, Queue<Slot>> colorMapedUsedSlots;
        private readonly Dictionary<string, Slot> licenseMapedUsedSlots;

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
            if(licenseMapedUsedSlots.ContainsKey(license))
            {
                return licenseMapedUsedSlots[license];
            }
            return null;
        }

        public bool Park(Vehical vehical)
        {
            if (!IsFull(vehical.size))
            {
                var slot = GetFreeSlot(vehical.size);
                slot.Park(vehical);
                vehical.Park(slot);

                AddToLicenseMaped(vehical.licenseNumber, slot);
                AddToColorMaped(vehical.color, slot);
                return true;
            }

            return false;
        }

        public void UnPark(string license)
        {
            var slot = licenseMapedUsedSlots[license];
            slot.UnPark();

            licenseMapedUsedSlots.Remove(license);
            availableSlots[slot.size].Enqueue(slot);
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
                queue.Enqueue(slot);
                colorMapedUsedSlots.Add(color, queue);
            }
        }

        private void AddToLicenseMaped(string license, Slot slot)
        {
            licenseMapedUsedSlots.Add(license, slot);
        }

    }

}
