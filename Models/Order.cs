namespace FastFoodOrderingApp_BackEnd.Models
{
    public class Order
    {

        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        //public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    
    }
}
