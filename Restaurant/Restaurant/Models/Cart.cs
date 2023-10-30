using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    [TableAttribute(name: "Cart")]
    public class Cart
    {
        [ColumnAttribute(name: "CartId")]
        public int cartId { get; set; }
        [KeyAttribute]
        [ColumnAttribute(name: "MenuId")]      
        public int menuId { get; set; }
        [ColumnAttribute(name: "MenuCount")]
        public int menuCount { get; set; }
        [ColumnAttribute(name: "OrderFinish")]
        public string orderFinish { get; set; }
    }
}
