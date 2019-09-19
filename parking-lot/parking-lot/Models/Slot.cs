
namespace parking_lot.Models
{
    public partial class Slot
    {
        public int slotId { get; }
        public int parkingSpaceId { get; }
        public SlotSize size { get; }
        public Vehical vehical { get; private set; }
        public bool isPremium { get; private set; }

        public Slot(int id, int spaceId, SlotSize size, bool isPremium = false)
        {
            this.parkingSpaceId = spaceId;
            this.slotId = id;
            this.size = size;
            this.isPremium = isPremium;
        }

        public void Park(Vehical vehical)
        {
            this.vehical = vehical;
        }

        public void UnPark()
        {
            this.vehical = null;
        }

        public void SetPremium(bool premium)
        {
            this.isPremium = premium;
        }
    }
}
