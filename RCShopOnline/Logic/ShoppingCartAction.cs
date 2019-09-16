using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RCShopOnline.Models;

namespace RCShopOnline.Logic
{
    public class ShoppingCartAction : IDisposable
    {
        public string ShoppingCartId { get; set; }
        private RCContext _db = new RCContext();
        public const string CartSessionKey = "CartId";
        public void AddToCart(int id)
        {
            // Retrieve the product from the database.
            ShoppingCartId = GetCartId();
            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.RCID == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    RCID = id,
                    CartId = ShoppingCartId,
                    RC = _db.RCs.SingleOrDefault(p => p.RCID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,
                // then add one to the quantity.
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] =
                    HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return _db.ShoppingCartItems.Where(
            c => c.CartId == ShoppingCartId).ToList();
        }
        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();
            // Tổng tiền mỗi cuốn sách (Item Total) = đơn giá (UnitPrice) nhân
            // số lượng (Quantity). Tổng của các tổng tiền chính là
            // số tiền mà người dùng phải trả (Order Total)
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.CartId == ShoppingCartId
                               select (int?)cartItems.Quantity * cartItems.RC.UnitPrice).Sum();
            return total ?? decimal.Zero;
        }
        public ShoppingCartAction GetCart(HttpContext context)
        {
            using (var cart = new ShoppingCartAction())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }
        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[]CartItemUpdates)
        {
            using (var db = new RCShopOnline.Models.RCContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.RC.RCID == CartItemUpdates[i].RCID)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 ||
                                    CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.RCID);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.RCID,
                                    CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " +
                    exp.Message.ToString(), exp);
                }
            }
        }
        public void RemoveItem(string removeCartID, int removeRCID)
        {
            using (var _db = new RCShopOnline.Models.RCContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems
                                  where c.CartId == removeCartID && c.RC.RCID == removeRCID
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Xóa
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " +
                    exp.Message.ToString(), exp);
                }
            }
        }
        public void UpdateItem(string updateCartID, int updateRCID, int quantity)
        {
            using (var _db = new RCShopOnline.Models.RCContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems
                                  where c.CartId == updateCartID && c.RC.RCID == updateRCID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " +
                    exp.Message.ToString(), exp);
                }
            }
        }
        public void EmptyCart()
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(
            c => c.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }
            // cập nhật
            _db.SaveChanges();
        }
        public int GetCount()
        {
            ShoppingCartId = GetCartId();
            // Đếm và tính tổng
            int? count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Trả về 0 nếu rỗng
            return count ?? 0;
            return 0;
        }
        public struct ShoppingCartUpdates
        {
            public int RCID;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }
    }
}