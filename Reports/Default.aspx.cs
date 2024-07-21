using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Reports.Model;

namespace Reports
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected async void btnLogin_Click(object sender, EventArgs e)
		{
			string username = txtEmail.Text;
			string password = txtPassword.Text;

			// Replace with your API endpoint URL
			string apiUrl = "https://localhost:7161/api/Login";

			var loginData = new
			{
				Email = username , 
				Password = password ,
			};

			try
			{
				using (HttpClient client = new HttpClient())
				{

					// Serialize loginData to JSON string
					string jsonData = JsonConvert.SerializeObject(loginData);

					// Prepare request content
					var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");


					// Send POST request and await response
					HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check response status
                    if (response.IsSuccessStatusCode)
					{
						string jsonResponse = await response.Content.ReadAsStringAsync();
						LoginResponse tokenResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

						Session["AuthToken"] = tokenResponse.Token;
						Session["Email"] = tokenResponse.Email;

						Response.Redirect("Dashboard.aspx" , false);
					}
					else
					{
						// Handle unsuccessful login (display error message)
						string errorMessage = await response.Content.ReadAsStringAsync();
						lblMessage.Text =errorMessage;
						lblMessage.Visible = true;
					}
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions (log or display error)
				lblMessage.Text = $"Error: {ex.Message}";
				lblMessage.Visible = true;
			}
		}
	}
}