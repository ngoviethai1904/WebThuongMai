using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using RCShopOnline.Models;

namespace RCShopOnline
{
    public partial class RCDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<RC> GetDetails([QueryString("RCID")] int? RCId)
        {
            var _db = new RCShopOnline.Models.RCContext();
            IQueryable<RC> query = _db.RCs;
            if (RCId.HasValue && RCId > 0)
            {
                query = query.Where(p => p.RCID == RCId);
            }
            else
            {
                query = null;
            }
            return query;
        }
    }
}