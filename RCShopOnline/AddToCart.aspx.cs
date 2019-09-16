using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using RCShopOnline.Logic;

namespace RCShopOnline
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["RCID"];
            int RcId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out RcId))
            {
                using (ShoppingCartAction usersShoppingCart = new
                       ShoppingCartAction())
                {
                    usersShoppingCart.AddToCart(Convert.ToInt16(rawId));
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to AddToCart.aspx without a RCId.");
                throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a RCId.");
            }
            Response.Redirect("ShoppingCart.aspx");
        }
    }
}