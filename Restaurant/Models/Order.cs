using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Order
    {
        [KeyAttribute]
        [ColumnAttribute(name: "OrderId")]
        public int orderId { get; set; }
        [ColumnAttribute(name: "CartId")]
        public int cartId { get; set; }
        [ColumnAttribute(name: "IsShow")]
        public string isShow { get; set; }
    }
}
