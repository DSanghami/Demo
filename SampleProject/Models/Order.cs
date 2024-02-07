namespace SampleProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public string Address { get; set; }
        public int Total { get; set; }

        public bool IsDeleted { get; set; }
    }
}
