using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RESTfulClient.Instance == null)
                RESTfulClient.InitializeClent("https://localhost:7077/api/");
        }
    }
}