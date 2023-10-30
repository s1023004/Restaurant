using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    [TableAttribute(name:"Menu")]
    public class Menu
    {
        [KeyAttribute]
        [ColumnAttribute(name:"MenuID")]
        [RequiredAttribute]
        public int menuId { get; set; }
        [ColumnAttribute(name: "MenuName")]
        public String menuName { get; set; }
        [ColumnAttribute(name: "Price")]
        public int price { get; set; }
        [ColumnAttribute(name: "Image")]
        public String? image { get; set; }

    }

    public class MenuWithCount
    {
        public int menuId { get; set; }
        public String menuName { get; set; }
        public int price { get; set; }
        public String? image { get; set; }
        public int count { get; set; }

    }
}
