namespace OrderDetailService.Model
{
    public class Order
    {
        public int orderId { get; set; }
        public int orderAmount { get; set; }
        public string orderDate { get; set; }
    }
    public class UserResponse
    {
        public string error { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string Email { get; set; }
    }
}
