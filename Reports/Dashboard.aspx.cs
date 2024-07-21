using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reports
{
	public partial class Dashboard : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (string.IsNullOrEmpty(Session["AuthToken"]?.ToString()))
					Response.Redirect("default.aspx");

				// Display welcome message or user-specific information
				string email = Session["Email"]?.ToString(); // Replace with actual logic to get username or user information
				lblWelcomeMessage.Text = string.Format("<h2>Welcome {0}!</h2>", email);

			}
		}

		protected void btnLogout_Click(object sender, EventArgs e)
		{
			// Clear session and redirect to login page on logout
			Session.Clear();
			Response.Redirect("default.aspx");
		}
	}
}