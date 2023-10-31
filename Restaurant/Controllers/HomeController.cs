using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Restaurant.Models;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDB _dbContext;
        private readonly ILogger<HomeController> _logger;


        public HomeController(RestaurantDB _dbContext)
        {            
            this._dbContext = _dbContext;
            Console.WriteLine("注入的DbContext:" + this._dbContext.Database);
        }

        public IActionResult Index()
        {            
            List<Models.Menu> menus = null;
            menus = (from c in _dbContext.Menu
                     select c).ToList<Models.Menu>();
            int cartId = 0;
            if (ViewData["cartId"] != null)
            {
                cartId = Convert.ToInt32(ViewData["cartId"]);
            }
            else if(_dbContext.Cart.Where(c => c.orderFinish == "0").FirstOrDefault() != null)
            {
                cartId = _dbContext.Cart.Where(c => c.orderFinish == "0").FirstOrDefault().cartId;
            }
            else if(_dbContext.Cart.OrderByDescending(x => x.cartId).FirstOrDefault() != null)
            {
                ViewData["cartId"] = _dbContext.Cart.OrderByDescending(x => x.cartId).FirstOrDefault().cartId + 1;
                cartId = Convert.ToInt32(ViewData["cartId"]);
            }
            else if (_dbContext.Cart.FirstOrDefault() == null)
            {
                cartId = 1;
            }

            ViewData["cartId"] = cartId;
            return View(menus);
        }

        public IActionResult AddToCart(int cartId, int menuId, int menu_count)
        {
            List<string> message = new List<string>();
            var cart = _dbContext.Cart.Where(c => c.cartId == cartId && c.menuId == menuId).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart();
            }
            cart.cartId = cartId;
            cart.menuId = menuId;
            cart.menuCount = cart.menuCount + menu_count;
            cart.orderFinish = "0";
            _dbContext.Cart.Add(cart);
            try
            {
                if (_dbContext.Cart.Where(c => c.cartId == cartId && c.menuId == menuId).FirstOrDefault() != null)
                {
                    //_dbContext.Entry(cart).State = EntityState.Modified;
                    //_dbContext.SaveChanges();

#if Debug
                    using (var connection = new SqlConnection("Server=localhost;Database=Restaurant;User id=sa;password=1111;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True"))
                    {
                        connection.Open();
                        var query = "SET IDENTITY_INSERT dbo.Cart ON; Update dbo.Cart Set MenuCount = "+ cart.menuCount + " where CartId = " + cartId +" and MenuId = " + menuId + "; SET IDENTITY_INSERT dbo.Cart OFF;";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
#else

                    using (var connection = new SqlConnection("Server=tcp:rogerwebdb.database.windows.net,1433;Initial Catalog=Restaurant;Persist Security Info=False;User ID=s1023004;Password=!Qaz1023004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                    {
                        connection.Open();
                        var query = "SET IDENTITY_INSERT dbo.Cart ON; Update dbo.Cart Set MenuCount = "+ cart.menuCount + " where CartId = " + cartId +" and MenuId = " + menuId + "; SET IDENTITY_INSERT dbo.Cart OFF;";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
#endif
                }
                else
                {
#if Debug
                    using (var connection = new SqlConnection("Server=localhost;Database=Restaurant;User id=sa;password=1111;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True"))
                    {
                        connection.Open();
                        var query = "SET IDENTITY_INSERT dbo.Cart ON; INSERT INTO dbo.Cart (MenuId,MenuCount,CartId,OrderFinish) VALUES (" + menuId.ToString() + "," + menu_count.ToString() + "," + cartId.ToString() + ", 0); SET IDENTITY_INSERT dbo.Cart OFF;";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                
#else
                    using (var connection = new SqlConnection("Server=tcp:rogerwebdb.database.windows.net,1433;Initial Catalog=Restaurant;Persist Security Info=False;User ID=s1023004;Password=!Qaz1023004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                    {
                        connection.Open();
                        var query = "SET IDENTITY_INSERT dbo.Cart ON; INSERT INTO dbo.Cart (MenuId,MenuCount,CartId,OrderFinish) VALUES (" + menuId.ToString() + "," + menu_count.ToString() + "," + cartId.ToString() + ", 0); SET IDENTITY_INSERT dbo.Cart OFF;";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
#endif
                }
                message.Add($"購物車編號:{cartId}新增成功!");
                message.Add(cartId.ToString());
            }
            catch (Exception ex)
            {
                message.Add($"購物車編號:{cartId}新增失敗!失敗原因:" + ex.Message);
            }
            
            return Json(message);
        }

        public IActionResult AddToCart2(int cartId, int menuId, int menu_count)
        {
            List<string> message = new List<string>();
            var cart = _dbContext.Cart.Where(c => c.cartId == cartId && c.menuId == menuId).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart();
            }
            cart.cartId = cartId;
            cart.menuId = menuId;
            cart.menuCount = menu_count;
            cart.orderFinish = "0";
            _dbContext.Cart.Add(cart);
            try
            {
                if (_dbContext.Cart.Where(c => c.cartId == cartId && c.menuId == menuId).FirstOrDefault() != null)
                {
                    //_dbContext.Entry(cart).State = EntityState.Modified;
                    //_dbContext.SaveChanges();

#if Debug
                    using (var connection = new SqlConnection("Server=localhost;Database=Restaurant;User id=sa;password=1111;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True"))
                    {
                        connection.Open();
                        var query = "SET IDENTITY_INSERT dbo.Cart ON; Update dbo.Cart Set MenuCount = "+ cart.menuCount + " where CartId = " + cartId +" and MenuId = " + menuId + "; SET IDENTITY_INSERT dbo.Cart OFF;";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
#else

                    using (var connection = new SqlConnection("Server=tcp:rogerwebdb.database.windows.net,1433;Initial Catalog=Restaurant;Persist Security Info=False;User ID=s1023004;Password=!Qaz1023004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                    {
                        connection.Open();
                        var query = "SET IDENTITY_INSERT dbo.Cart ON; Update dbo.Cart Set MenuCount = " + cart.menuCount + " where CartId = " + cartId + " and MenuId = " + menuId + "; SET IDENTITY_INSERT dbo.Cart OFF;";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
#endif
                }         
            }
            catch (Exception ex)
            {
                message.Add($"購物車編號:{cartId}新增失敗!失敗原因:" + ex.Message);
            }

            return Json(message);
        }
        public IActionResult Cart()
        {
            int cartId = 0;
            List<Int32> menuIds = new List<Int32>();
            List<Models.Menu> menus = null;
            Dictionary<int, int> count = new Dictionary<int, int>();
            Dictionary<int, int> prices = new Dictionary<int, int>();
            int totalMoney = 0;
            if (_dbContext.Cart.Where(c => c.orderFinish == "0").FirstOrDefault() != null)
            {
                cartId = _dbContext.Cart.Where(c => c.orderFinish == "0").FirstOrDefault().cartId;
                menuIds = (from c in _dbContext.Cart
                           where c.cartId == cartId
                           select c.menuId).ToList<Int32>();
                menus = _dbContext.Menu.Where(x => menuIds.Contains(x.menuId)).ToList<Menu>();

                for(int i = 0; i < menus.Count; i++)
                {
                    prices.Add(menus[i].menuId, menus[i].price);
                }
                var cartMenus = _dbContext.Cart.Where(x => x.cartId == cartId).ToList<Cart>();                
                for (int i = 0; i < cartMenus.Count; i++)
                {
                    count.Add(cartMenus[i].menuId, cartMenus[i].menuCount);                    
                }
            }

            foreach(var key in count.Keys)
            {
                totalMoney += count[key] * prices[key];
            }
            //List<Models.Cart> cartMenus = new List<Models.Cart>();
           
            ViewData["count"] = count;
            ViewData["cartId"] = cartId;
            ViewData["totalMoney"] = totalMoney;

            return View(menus);
        }

        public IActionResult DeleteCart(int cartId,int menuId)
        {
            string message = "";
            try
            {
                var cart = _dbContext.Cart.Where(c => c.cartId == cartId && c.menuId == menuId).FirstOrDefault();
                _dbContext.Cart.Remove(cart);
                _dbContext.SaveChanges();
                message = "刪除成功";
            }
            catch(Exception ex)
            {
                message = "刪除失敗，原因:" + ex.Message;
            }
            
            return Json(message);
        }

        public IActionResult SubmitCart(int CartID)
        {
            string message = "";
            try
            {
                Order order = new Order();
                order.cartId = CartID;
                order.isShow = "1";
                _dbContext.Order.Add(order);
                _dbContext.SaveChanges();
#if Debug

                using (var connection = new SqlConnection("Server=localhost;Database=Restaurant;User id=sa;password=1111;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True"))
                {
                    connection.Open();
                    var query = "SET IDENTITY_INSERT dbo.Cart ON; Update dbo.Cart Set OrderFinish = '1' where CartId = " + CartID + "; SET IDENTITY_INSERT dbo.Cart OFF;";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
#else
                using (var connection = new SqlConnection("Server=tcp:rogerwebdb.database.windows.net,1433;Initial Catalog=Restaurant;Persist Security Info=False;User ID=s1023004;Password=!Qaz1023004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    connection.Open();
                    var query = "SET IDENTITY_INSERT dbo.Cart ON; Update dbo.Cart Set OrderFinish = '1' where CartId = " + CartID + "; SET IDENTITY_INSERT dbo.Cart OFF;";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
#endif
                message = "訂單送出成功";
                var cartId = _dbContext.Cart.OrderByDescending(x => x.cartId).FirstOrDefault().cartId + 1;
                ViewData["cartId"] = cartId + 1;
            }
            catch(Exception ex)
            {
                message = "訂單送出失敗，原因:" + ex.Message;
            }

            return Json(message);
        }


        public IActionResult Order()
        {
            List<int> orders = new List<int>();
            orders = (from c in _dbContext.Order
                      where c.isShow == "1"
                      select c.cartId).ToList<int>();

            var carts = from c in _dbContext.Cart
                         where orders.Contains(c.cartId)
                         group c by new
                         {
                             c.cartId,
                             c.menuId
                         } into groupKeys
                         select new Cart()
                         {
                             cartId = groupKeys.Key.cartId,
                             menuId = groupKeys.Key.menuId,
                             menuCount = groupKeys.First().menuCount,
                             orderFinish = groupKeys.First().orderFinish
                         };
            //var carts = _dbContext.Cart.Where(c => orders.Contains(c.cartId)).ToList<Cart>();
            Dictionary<int, List<int>> CartToMenus = new Dictionary<int, List<int>>();
            Dictionary<int,Dictionary<int, MenuWithCount>> menuInCart = new Dictionary<int,Dictionary<int, MenuWithCount>>();
            Dictionary<int, MenuWithCount> menuD = new Dictionary<int, MenuWithCount>();
            Dictionary<int, List<MenuWithCount>> OrderDetails= new Dictionary<int, List<MenuWithCount>>();
            Dictionary<int,int> cartToMoney = new Dictionary<int, int>();

            var menus = (from c in _dbContext.Menu
                         select c).ToList<Menu>();

            MenuWithCount menu1 = new MenuWithCount();
            foreach(var item in menus)
            {
                menu1= new MenuWithCount();
                menu1.price = item.price;
                menu1.menuId = item.menuId;
                menu1.menuName = item.menuName;
                menu1.image = item.image;                
                menuD.Add(item.menuId, menu1);
            }

            foreach (var c in carts)
            {
                if (!CartToMenus.ContainsKey(c.cartId))
                {
                    CartToMenus.Add(c.cartId, new List<int>());
                    menuInCart.Add(c.cartId, new Dictionary<int, MenuWithCount>());
                }
                menu1 = new MenuWithCount();
                menuD[c.menuId].count = c.menuCount;
                menu1.price = menuD[c.menuId].price;
                menu1.menuId = menuD[c.menuId].menuId;
                menu1.menuName = menuD[c.menuId].menuName;
                menu1.count = menuD[c.menuId].count;
                menu1.image = menuD[c.menuId].image;
                menuInCart[c.cartId].Add(c.menuId, menu1);
                CartToMenus[c.cartId].Add(c.menuId);
            }
            
            foreach (int key in CartToMenus.Keys)
            {
                if (!OrderDetails.ContainsKey(key))
                {
                    OrderDetails.Add(key, new List<MenuWithCount>());
                    cartToMoney.Add(key, 0);
                }
                foreach(int menuId in CartToMenus[key])
                {
                    cartToMoney[key] += menuInCart[key][menuId].price * menuInCart[key][menuId].count;
                    OrderDetails[key].Add(menuInCart[key][menuId]);
                }
            }
            OrderDetails = OrderDetails.OrderBy(c => c.Key).ToDictionary(pair =>pair.Key, pair => pair.Value);

            ViewData["Money"] = cartToMoney;
            return View(OrderDetails);
        }

        public IActionResult ClearHistory()
        {
            string message = "";
            try
            {
#if Debug

                using (var connection = new SqlConnection("Server=localhost;Database=Restaurant;User id=sa;password=1111;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True"))
                {
                    connection.Open();
                    var query = "SET IDENTITY_INSERT [Restaurant].[dbo].[Order] ON; Update [Restaurant].[dbo].[Order] Set IsShow = '0'; SET IDENTITY_INSERT [Restaurant].[dbo].[Order] OFF;";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
#else
                using (var connection = new SqlConnection("Server=tcp:rogerwebdb.database.windows.net,1433;Initial Catalog=Restaurant;Persist Security Info=False;User ID=s1023004;Password=!Qaz1023004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    connection.Open();
                    var query = "SET IDENTITY_INSERT [Restaurant].[dbo].[Order] ON; Update [Restaurant].[dbo].[Order] Set IsShow = '0'; SET IDENTITY_INSERT [Restaurant].[dbo].[Order] OFF;";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
#endif
                message = "清除成功!";
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }

            return Json(message);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}