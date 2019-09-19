
namespace parking_lot.Models
{
    public class Slot
    {
        public int slotId;
        public int parkingSpaceId;
        public SlotSize size;
        private Vehical vehical;

        public Slot(int id, int spaceId, SlotSize size)
        {
            this.parkingSpaceId = spaceId;
            this.slotId = id;
            this.size = size;
        }

        public void Park(Vehical vehical)
        {
            this.vehical = vehical;
        }

        public void UnPark()
        {
            this.vehical = null;
        }
    }
}
