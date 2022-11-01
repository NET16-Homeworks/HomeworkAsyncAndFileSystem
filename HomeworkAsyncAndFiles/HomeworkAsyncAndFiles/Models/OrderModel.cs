namespace HomeworkAsyncAndFiles.Models
{
    public class OrderModel
    {
        public int ProductNumber { get; set; }
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public double ProductPrice { get; set; }
        public DateTime DateOfCreating { get; set; }
        public int CustomerId { get; set; }
        public string CustomersFirstName { get; set; }
        public string CustomersLastName { get; set; }
        public string CustomersEmail { get; set; }
        public int CustomersAge { get; set; }
    }
}
