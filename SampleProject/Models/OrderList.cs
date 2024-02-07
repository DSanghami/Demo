using Microsoft.AspNetCore.Mvc.Rendering;

namespace SampleProject.Models
{
    public class OrderList
    {
        public int  Id { get; set; }
       
        public string Products { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int Total { get; set; }
    }


}

