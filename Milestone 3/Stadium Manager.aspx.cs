using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Stadium_Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void stadiumInformation(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            string username = Session["user"].ToString();
            string stadiumname = "";

            SqlCommand allStadiumManagerCommand = new SqlCommand("SELECT * FROM allStadiumManagers", conn);

            conn.Open();

            SqlDataReader readallStadiumManager = allStadiumManagerCommand.ExecuteReader();
            while (readallStadiumManager.Read())
            {
                // Get the representative club name
                string stadiumanagerusername = readallStadiumManager.GetString(readallStadiumManager.GetOrdinal("username"));

                if (stadiumanagerusername.Equals(username))
                    stadiumname = readallStadiumManager.GetString(readallStadiumManager.GetOrdinal("Stadium Name"));

            }
            readallStadiumManager.Close();
            conn.Close();

            SqlCommand allStadiumsCommand = new SqlCommand("SELECT * FROM allStadiums", conn);

            conn.Open();
            SqlDataReader readAllStadiums = allStadiumsCommand.ExecuteReader();
            while (readAllStadiums.Read())
            {
                string name = readAllStadiums.GetString(readAllStadiums.GetOrdinal("Name"));
                string location = readAllStadiums.GetString(readAllStadiums.GetOrdinal("Location"));
                double capacity = readAllStadiums.GetInt32(readAllStadiums.GetOrdinal("Capacity"));
                bool status = readAllStadiums.GetBoolean(readAllStadiums.GetOrdinal("Status"));
                if (name.Equals(stadiumname))
                {
                    StringBuilder sbuild = new StringBuilder();
                    sbuild.Append("<center>");
                    sbuild.Append("<hr/>");
                    sbuild.Append("<table border~2>");
                    sbuild.Append("<tr style='background-color:green; color: White;'>");
                    sbuild.Append("<th>");
                    sbuild.Append("NAME");
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append("LOCATION");
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append("CAPACITY");
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append("STATUS");
                    sbuild.Append("</th>");
                    sbuild.Append("</tr>");
                    sbuild.Append("<th>");
                    sbuild.Append(name);
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append(location);
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append(capacity);
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append(status);
                    sbuild.Append("</th>");
                    sbuild.Append("</table>");
                    sbuild.Append("</center>");
                    Label label1 = new Label();
                    label1.Text = sbuild.ToString();
                    Panel1.Controls.Add(label1);
                }
            }
            readAllStadiums.Close();
            conn.Close();
        }

        protected void allRequestReceived(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            string username = Session["user"].ToString();

            SqlCommand allReceivedRequestsCommand = new SqlCommand("SELECT * FROM allReceivedRequests(@stadiummanagerusername)", conn);
            allReceivedRequestsCommand.Parameters.Add(new SqlParameter("@stadiummanagerusername", username));

            SqlDataAdapter sda = new SqlDataAdapter(allReceivedRequestsCommand);
            DataTable dtable = new DataTable();
            sda.Fill(dtable);

            conn.Open();
            StringBuilder sbuild = new StringBuilder();
            sbuild.Append("<center>");
            sbuild.Append("<hr/>");
            sbuild.Append("<table border~2>");
            sbuild.Append("<tr style='background-color:green; color: White;'>");
            foreach (DataColumn dc in dtable.Columns)
            {
                sbuild.Append("<th>");
                sbuild.Append(dc.ColumnName.ToUpper());
                sbuild.Append("</th>");
            }
            sbuild.Append("</tr>");

            foreach (DataRow dr in dtable.Rows)
            {
                sbuild.Append("</tr>");

                foreach (DataColumn dc in dtable.Columns)
                {
                    sbuild.Append("<th>");
                    sbuild.Append(dr[dc.ColumnName].ToString());
                    sbuild.Append("</th>");
                }
            }
            sbuild.Append("</table>");
            sbuild.Append("</center>");
            Label label1 = new Label();
            label1.Text = sbuild.ToString();
            Panel2.Controls.Add(label1);
            conn.Close();
        }

        protected void AcceptRequest(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (acceptHostName.Text == "")
            {
                Label label = new Label();
                label.Text = "Host Name can not be empty" + "<br></br>";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (acceptGuestName.Text == "")
            {
                Label label = new Label();
                label.Text = "Guest Name can not be empty" + "<br></br>";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (acceptStarttime.Text == "")
            {
                Label label = new Label();
                label.Text = "Start Time Field can not be empty" + "<br></br>";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string stadiummanagerusername = Session["user"].ToString();
            string hostname = acceptHostName.Text;
            string guestname = acceptGuestName.Text;
            DateTime starttime = DateTime.Parse(acceptStarttime.Text);

            SqlCommand acceptRequestCommand = new SqlCommand("acceptRequest", conn);
            acceptRequestCommand.CommandType = CommandType.StoredProcedure;
            acceptRequestCommand.Parameters.Add(new SqlParameter("@stadiumanagerusername", stadiummanagerusername));
            acceptRequestCommand.Parameters.Add(new SqlParameter("@hostname", hostname));
            acceptRequestCommand.Parameters.Add(new SqlParameter("@guestname", guestname));
            acceptRequestCommand.Parameters.Add(new SqlParameter("@starttime", starttime));

            SqlCommand allPendingRequests = new SqlCommand("SELECT * FROM allUnhandeledRequests(@stadiummanagerusername)", conn);
            allPendingRequests.Parameters.Add(new SqlParameter("@stadiummanagerusername", stadiummanagerusername));

            SqlCommand AcceptedMatch = new SqlCommand("AcceptedRequests", conn);
            AcceptedMatch.CommandType = CommandType.StoredProcedure;
            AcceptedMatch.Parameters.Add(new SqlParameter("@hostname", hostname));
            AcceptedMatch.Parameters.Add(new SqlParameter("@guestname", guestname));
            AcceptedMatch.Parameters.Add(new SqlParameter("@starttime", starttime));
            SqlParameter success = AcceptedMatch.Parameters.Add("@success", SqlDbType.Int);
            success.Direction = ParameterDirection.Output;

            bool found = false;

            conn.Open();
            AcceptedMatch.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString() == "1")
            {
                Label label = new Label();
                label.Text = "Match have been assigned to a Stadium";
                Panel3.Controls.Add(label);
                acceptHostName.Text = "";
                acceptGuestName.Text = "";
                acceptStarttime.Text = "";
                return;
            }

            conn.Open();
            SqlDataReader readAllRequests = allPendingRequests.ExecuteReader();
            while (readAllRequests.Read())
            {
                string clubhostname = readAllRequests.GetString(readAllRequests.GetOrdinal("Host Name"));
                string clubguestname = readAllRequests.GetString(readAllRequests.GetOrdinal("Guest Name"));
                DateTime date = readAllRequests.GetDateTime(readAllRequests.GetOrdinal("Starttime"));
                if (clubhostname.Equals(hostname) && clubguestname.Equals(guestname) && date == starttime)
                {
                    found = true;
                }

            }
            readAllRequests.Close();

            if (found)
            {
                acceptRequestCommand.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Request is accepted Successfully";
                Panel3.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = "Request is not Found";
                Panel3.Controls.Add(label);
            }

            acceptHostName.Text = "";
            acceptGuestName.Text = "";
            acceptStarttime.Text = "";

            conn.Close();
        }

        protected void RejectRequest(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (rejectHostName.Text == "")
            {
                Label label = new Label();
                label.Text = "Host Name can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (rejectGuestName.Text == "")
            {
                Label label = new Label();
                label.Text = "Guest Name can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (rejectStarttime.Text == "")
            {
                Label label = new Label();
                label.Text = "Start Time Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string stadiummanagerusername = Session["user"].ToString();
            string hostname = rejectHostName.Text;
            string guestname = rejectGuestName.Text;
            DateTime starttime = DateTime.Parse(rejectStarttime.Text);

            SqlCommand rejectRequestCommand = new SqlCommand("rejectRequest", conn);
            rejectRequestCommand.CommandType = CommandType.StoredProcedure;
            rejectRequestCommand.Parameters.Add(new SqlParameter("@stadiumanagerusername", stadiummanagerusername));
            rejectRequestCommand.Parameters.Add(new SqlParameter("@hostname", hostname));
            rejectRequestCommand.Parameters.Add(new SqlParameter("@guestname", guestname));
            rejectRequestCommand.Parameters.Add(new SqlParameter("@starttime", starttime));

            SqlCommand allUnhandeledRequestsCommand = new SqlCommand("SELECT * FROM allUnhandeledRequests(@stadiummanagerusername)", conn);
            allUnhandeledRequestsCommand.Parameters.Add(new SqlParameter("@stadiummanagerusername", stadiummanagerusername));

            bool found = false;

            conn.Open();

            SqlDataReader readAllRequests = allUnhandeledRequestsCommand.ExecuteReader();
            while (readAllRequests.Read())
            {
                string clubhostname = readAllRequests.GetString(readAllRequests.GetOrdinal("Host Name"));
                string clubguestname = readAllRequests.GetString(readAllRequests.GetOrdinal("Guest Name"));
                DateTime date = readAllRequests.GetDateTime(readAllRequests.GetOrdinal("Starttime"));

                if (clubhostname.Equals(hostname) && clubguestname.Equals(guestname) && date == starttime)
                {
                    found = true;
                }
            }
            readAllRequests.Close();


            if (found)
            {
                rejectRequestCommand.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Request is rejected Sucessfully";
                Panel4.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = "Request is not Found";
                Panel4.Controls.Add(label);
            }

            rejectHostName.Text = "";
            rejectGuestName.Text = "";
            rejectStarttime.Text = "";

            conn.Close();
        }
    }
}