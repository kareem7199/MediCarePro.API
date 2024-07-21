using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Reports.Model;

namespace Reports
{
	public partial class ReportViewer : System.Web.UI.Page
	{
		protected async void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//if (string.IsNullOrEmpty(Session["AuthToken"]?.ToString()))
				//{
				//	Response.Redirect("default.aspx");
				//	Context.ApplicationInstance.CompleteRequest(); // Complete the request
				//	return;
				//}

				if (Request.QueryString["id"] != null)
				{
					string visitId = Request.QueryString["id"];

					DataTable dt = new DataTable();
					dt.Columns.Add("PatientName", typeof(string));
					dt.Columns.Add("PhysicianName", typeof(string));
					dt.Columns.Add("Amount", typeof(decimal));
					dt.Columns.Add("Date", typeof(DateTime));
					dt.Columns.Add("Diagnosis", typeof(string));

					try
					{
						// Replace with your API URL
						string apiUrl = $"https://localhost:7161/api/PhysicianScreen/Visit/{visitId}";


						using (HttpClient client = new HttpClient())
						{
							var token = Session["AuthToken"]?.ToString();
							client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

							HttpResponseMessage response = await client.GetAsync(apiUrl);
							response.EnsureSuccessStatusCode();

							// Assuming the API returns JSON data
							var jsonString = await response.Content.ReadAsStringAsync();

							// Deserialize JSON to a list of objects
							var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Visit>(jsonString);


							dt.Rows.Add(data.PatientName, data.PhysicanName, data.PhysicanFees, data.Date, data.Diagnosis);

							ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dt); // Replace "DataSetName" with your dataset name in the RDLC file

							ctl14.LocalReport.DataSources.Clear();
							ctl14.LocalReport.DataSources.Add(reportDataSource);

							// Refresh the report viewer
							ctl14.LocalReport.Refresh();
						}
					}
					catch (Exception ex)
					{
						// Handle exceptions
						Response.Redirect("Dashboard.aspx", false);
						Console.WriteLine("Error fetching data: " + ex.Message);
					}
				}
				else Response.Redirect("Dashboard.aspx", false);
			}
		}
	}
}