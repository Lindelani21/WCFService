﻿using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                NotLoggedin.Attributes.Remove("hidden");
                return;
            }

            User user = (User)Session["User"];

            switch(user.Role)
            {
                case "student":
                case "assistant":
                    StudentLoggedin.Attributes.Remove("hidden");
                    break;
                case "lecturer":
                    LecturerLoggedin.Attributes.Remove("hidden");
                    break;
            }
        }
    }
}