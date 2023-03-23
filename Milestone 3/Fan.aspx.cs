using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Fan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewMatches_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (String.IsNullOrWhiteSpace(date_entered.Text))
            {
                Label label = new Label();
                label.Text = "Date Field can not be empty";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            DateTime starttime = DateTime.Parse(date_entered.Text);

            SqlCommand viewAllMatches = new SqlCommand("SELECT * FROM availableMatchesToAttend(@availabletime)", conn);
            viewAllMatches.Parameters.Add(new SqlParameter("@availabletime", starttime));

            SqlDataAdapter sda = new SqlDataAdapter(viewAllMatches);
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
            Panel1.Controls.Add(label1);
            date_entered.Text = "";
            conn.Close();
        }

        protected void purchaseTickeT_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (hostname.Text.Equals(""))
            {
                Label label = new Label();
                label.Text = "Host Name Field can not be empty" + "<br></br>";
                Panel2.Controls.Add(label);
                flag = true;
            }

            if (guestname.Text.Equals(""))
            {
                Label label = new Label();
                label.Text = "Guest Name Field can not be empty" + "<br></br>";
                Panel2.Controls.Add(label);
                flag = true;
            }
            if (starttime.Text.Equals(""))
            {
                Label label = new Label();
                label.Text = "Date Field can not be empty" + "<br></br>";
                Panel2.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }


            string clubhostname = hostname.Text;
            string clubguestname = guestname.Text;
            DateTime date = DateTime.Parse(starttime.Text);
            string username = Session["user"].ToString();
            string nationalId = "";

            SqlCommand allFans = new SqlCommand("SELECT * FROM allFans", conn);

            conn.Open();

            SqlDataReader readAllFan = allFans.ExecuteReader();
            while (readAllFan.Read())
            {
                string id = readAllFan.GetString(readAllFan.GetOrdinal("NationalID"));
                string user = readAllFan.GetString(readAllFan.GetOrdinal("username"));
                bool status = readAllFan.GetBoolean(readAllFan.GetOrdinal("Status"));
                if (user.Equals(username))
                {
                    nationalId = id;
                }
            }
            conn.Close();

            SqlCommand allMatch = new SqlCommand("SELECT * FROM availableMatchesToAttend(@availabletime)", conn);
            allMatch.Parameters.Add(new SqlParameter("@availabletime", date));

            SqlCommand purchaseTicket = new SqlCommand("purchaseTicket", conn);
            purchaseTicket.CommandType = CommandType.StoredProcedure;
            purchaseTicket.Parameters.Add(new SqlParameter("@fannationalID", nationalId));
            purchaseTicket.Parameters.Add(new SqlParameter("@hostname", clubhostname));
            purchaseTicket.Parameters.Add(new SqlParameter("@guestname", clubguestname));
            purchaseTicket.Parameters.Add(new SqlParameter("@starttime", date));

            bool found = false;

            conn.Open();

            SqlDataReader readallMatches = allMatch.ExecuteReader();
            while (readallMatches.Read())
            {
                string h = readallMatches.GetString(readallMatches.GetOrdinal("Host Name"));
                string g = readallMatches.GetString(readallMatches.GetOrdinal("Guest Name"));
                DateTime t = readallMatches.GetDateTime(readallMatches.GetOrdinal("Starttime"));


                if (h.Equals(clubhostname) && g.Equals(clubguestname) && t == date)
                {
                    found = true;
                }
            }
            readallMatches.Close();

            if (found)
            {
                purchaseTicket.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Purchase Completed Successfully";
                Panel2.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = "Match is unavailable";
                Panel2.Controls.Add(label);
            }


            hostname.Text = "";
            guestname.Text = "";
            starttime.Text = "";
            conn.Close();
        }
    }
}