using System.Collections.Generic;

namespace parking_lot.Models
{
    public class ParkingSpaceConfig
    {
        public ParkingSpace space { get; }

        public ParkingSpaceConfig(Dictionary<SlotSize, int> slotsCountMap)
        {
            this.space = new ParkingSpace();
            foreach(var slot in slotsCountMap)
            {
                this.space.CreateSlot(slot.Key, slot.Value);
            }
        }
    }

    public class ParkingLotConfig
    {
        private readonly static ParkingSpaceConfig[] configs = new ParkingSpaceConfig[]
        {
            new ParkingSpaceConfig(new Dictionary<SlotSize, int>()
            {
                {SlotSize.M, 2},
                {SlotSize.L, 4},
                {SlotSize.XL, 2},
                {SlotSize.S, 2},
            }),
        };

        public static ParkingSpace[] GetParkingSpaces()
        {
            int i = 0;
            var spaces = new ParkingSpace[configs.Length];
            foreach(var config in configs)
            {
                spaces[i++] = config.space;
            }
            return spaces;
        }
    }

}
