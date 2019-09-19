namespace parking_lot.Models.Exception
{
    public class ParkingException : System.Exception
    {
        public ParkingException(string message) : base(message) { }
    }
}
