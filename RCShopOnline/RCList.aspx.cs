﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using RCShopOnline.Models;

namespace RCShopOnline
{
    public partial class RCList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<RC> GetRCs([QueryString("id")] int? categoryId)
        {
            var _db = new RCShopOnline.Models.RCContext();
            IQueryable<RC> query = _db.RCs;
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }
            return query;
        }
    }
}