
namespace parking_lot.Models
{
    public interface IVehical
    {
        float GetParkingCost();
    }

    public abstract class Vehical
    {
        public string licenseNumber { get; }
        public SlotSize size { get; }
        public Color color { get; }
        public Slot slot { get; private set; }

        public Vehical(string license, SlotSize size, Color color)
        {
            this.licenseNumber = license;
            this.size = size;
            this.color = color;
        }

        public void Park(Slot slot)
        {
            this.slot = slot;
        }

        public void UnPark()
        {
            this.slot = null;
        }
    }
}
