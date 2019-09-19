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

        private readonly Dictionary<Color, Slot> colorMapedUsedSlots;
        private readonly Dictionary<string, Slot> licenseMapedUsedSlots;

        public ParkingSpace()
        {
            availableSlots = new Dictionary<SlotSize, Queue<Slot>>();
            licenseMapedUsedSlots = new Dictionary<string, Slot>();
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

        public bool IsFull(SlotSize size)
        {
            if (availableSlots.ContainsKey(size))
                return availableSlots[size].Count == 0;
            throw new System.Exception("Slot " + size + " Has not configured in Database");
        }

        public bool Park(Vehical vehical)
        {
            if (!IsFull(vehical.size))
            {
                var slot = GetFreeSlot(vehical.size);
                slot.Park(vehical);
                vehical.Park(slot);
                //colorMapedUsedSlots.Add()
                licenseMapedUsedSlots.Add(vehical.licenseNumber, slot);

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

        private Slot GetFreeSlot(SlotSize size) => availableSlots[size].Dequeue();
    }

}
