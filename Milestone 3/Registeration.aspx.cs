using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Milestone_3
{
    public partial class Registeration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterSPA_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Response.Redirect("Sport Association Manager Registeration.aspx", true);

        }

        protected void RegisterRepresentative_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Response.Redirect("Club Representative Registeration.aspx", true);
        }

        protected void RegisterStadiumManager_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Response.Redirect("Stadium Manager Registeration.aspx", true);
        }

        protected void RegisterFan_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Response.Redirect("Fan Registeration.aspx", true);
        }
    }
}