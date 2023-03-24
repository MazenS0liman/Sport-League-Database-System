using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Sport_Association_Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addNewMatch(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (add_match_host_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Host Name Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (add_match_guest_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Guest Name Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (add_match_start_time.Text == "")
            {
                Label label = new Label();
                label.Text = "Start Time Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (add_match_end_time.Text == "")
            {
                Label label = new Label();
                label.Text = "End Time Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            //To read the input from the user
            string hostname = add_match_host_name.Text;
            string guestname = add_match_guest_name.Text;
            DateTime starttime = DateTime.Parse(add_match_start_time.Text);
            DateTime endtime = DateTime.Parse(add_match_end_time.Text);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("addNewMatch", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@hostname", hostname));
            cmd.Parameters.Add(new SqlParameter("@guestname", guestname));
            cmd.Parameters.Add(new SqlParameter("@starttime", starttime));
            cmd.Parameters.Add(new SqlParameter("@endtime", endtime));

            SqlCommand readAllMatches = new SqlCommand("SELECT * From  allMatches", conn);
            SqlCommand readAllClubs = new SqlCommand("SELECT * FROM allCLubs", conn);

            //Executing the SQLCommand
            conn.Open();

            bool found = false;
            bool validHost = false;
            bool validGuest = false;
            //pass parameters to the stored procedure
            SqlDataReader rdr1 = readAllMatches.ExecuteReader();
            while (rdr1.Read())
            {
                // Host Name
                string h = rdr1.GetString(rdr1.GetOrdinal("Host Club Name"));
                // Guest Name
                string g = rdr1.GetString(rdr1.GetOrdinal("Guest Club Name"));
                // Start Time
                DateTime start = rdr1.GetDateTime(rdr1.GetOrdinal("Starttime"));

                if ((h.Equals(hostname) || guestname.Equals(g)) && start >= starttime && start <= endtime)
                {
                    found = true;
                }

            }
            rdr1.Close();


            SqlDataReader rdr2 = readAllClubs.ExecuteReader();
            while (rdr2.Read())
            {

                // Club Name
                string name = rdr2.GetString(rdr2.GetOrdinal("Name"));
                // Location
                string location = rdr2.GetString(rdr2.GetOrdinal("Location"));

                if ((name.Equals(hostname)))
                {
                    validHost = true;
                }

                if ((name.Equals(guestname)))
                {
                    validGuest = true;
                }
            }
            rdr2.Close();

            if (!found && validHost && validGuest)
            {
                cmd.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Match is added successfully" + "<br></br>";
                Panel4.Controls.Add(label);
            }
            else
            {
                if (validGuest && validHost)
                {
                    Label label = new Label();
                    label.Text = "Host or Guest have a Match during this time" + "<br></br>";
                    Panel4.Controls.Add(label);
                }

                if (!validHost)
                {
                    Label label = new Label();
                    label.Text = "Host Club Name is not found" + "<br></br>";
                    Panel4.Controls.Add(label);
                }


                if (!validGuest)
                {
                    Label label = new Label();
                    label.Text = "Guest Club Name is not found" + "<br></br>";
                    Panel4.Controls.Add(label);
                }

            }

            add_match_host_name.Text = "";
            add_match_guest_name.Text = "";
            add_match_start_time.Text = "";
            add_match_end_time.Text = "";

            conn.Close();
        }

        protected void deleteMatch(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("deleteCertainMatch", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            bool flag = false;

            if (delete_match_host_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Host Name Field can not be empty" + "<br></br>";
                Panel8.Controls.Add(label);
                flag = true;
            }

            if (delete_match_guest_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Guest Name Field can not be empty" + "<br></br>";
                Panel8.Controls.Add(label);
                flag = true;
            }

            if (delete_match_start_time.Text == "")
            {
                Label label = new Label();
                label.Text = "Start Time Field can not be empty" + "<br></br>";
                Panel8.Controls.Add(label);
                flag = true;
            }

            if (delete_match_end_time.Text == "")
            {
                Label label = new Label();
                label.Text = "End Time Field can not be empty" + "<br></br>";
                Panel8.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }


            string hostname = delete_match_host_name.Text;
            string guestname = delete_match_guest_name.Text;
            DateTime starttime = DateTime.Parse(delete_match_start_time.Text);
            DateTime endtime = DateTime.Parse(delete_match_end_time.Text);

            cmd.Parameters.Add(new SqlParameter("@hostname", hostname));
            cmd.Parameters.Add(new SqlParameter("@guestname", guestname));
            cmd.Parameters.Add(new SqlParameter("@starttime", starttime));
            cmd.Parameters.Add(new SqlParameter("@endtime", endtime));

            SqlCommand readAllMatches = new SqlCommand("SELECT * From  allMatches2", conn);
            SqlCommand readAllClubs = new SqlCommand("SELECT * FROM allCLubs", conn);

            //Executing the SQLCommand
            conn.Open();

            bool foundMatch = false;
            bool validHost = false;
            bool validGuest = false;

            //pass parameters to the stored procedure
            SqlDataReader rdr1 = readAllMatches.ExecuteReader();
            while (rdr1.Read())
            {
                // Host Name
                string h = rdr1.GetString(rdr1.GetOrdinal("Host Club Name"));
                // Guest Name
                string g = rdr1.GetString(rdr1.GetOrdinal("Guest Club Name"));
                // Start Time
                DateTime start = rdr1.GetDateTime(rdr1.GetOrdinal("Starttime"));
                // End Time
                DateTime end = rdr1.GetDateTime(rdr1.GetOrdinal("Endtime"));

                if (h.Equals(hostname) && guestname.Equals(g) && start == starttime && end == endtime)
                {
                    foundMatch = true;
                }
            }
            rdr1.Close();


            SqlDataReader rdr2 = readAllClubs.ExecuteReader();
            while (rdr2.Read())
            {
                // Club Name
                string name = rdr2.GetString(rdr2.GetOrdinal("Name"));
                // Location
                string location = rdr2.GetString(rdr2.GetOrdinal("Location"));

                if ((name.Equals(hostname)))
                {
                    validHost = true;
                }

                if ((name.Equals(guestname)))
                {
                    validGuest = true;
                }
            }
            rdr2.Close();

            if (foundMatch && validHost && validGuest)
            {
                cmd.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Match is deleted successfully" + "<br></br>";
                Panel8.Controls.Add(label);
            }
            else
            {
                if (!foundMatch && validGuest && validHost)
                {
                    Label label = new Label();
                    label.Text = "Match is not found" + "<br></br>";
                    Panel8.Controls.Add(label);
                }

                if (!validHost)
                {
                    Label label = new Label();
                    label.Text = "Host Club Name is not found" + "<br></br>";
                    Panel8.Controls.Add(label);
                }


                if (!validGuest)
                {
                    Label label = new Label();
                    label.Text = "Guest Club Name is not found" + "<br></br>";
                    Panel8.Controls.Add(label);
                }

            }

            delete_match_host_name.Text = "";
            delete_match_guest_name.Text = "";
            delete_match_start_time.Text = "";
            delete_match_end_time.Text = "";

            conn.Close();
        }

        protected void Onclick_AddView3(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("SELECT * FROM clubsNeverMatched", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
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
            Panel3.Controls.Add(label1);
            conn.Close();
        }

        protected void Onclick_RemoveView3(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Panel3.Controls.Clear();
        }

        protected void Onclick_AddView2(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("SELECT * FROM alreadyPlayedMatch", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
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

        protected void Onclick_RemoveView2(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Panel2.Controls.Clear();
        }

        protected void Onclick_AddView1(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("SELECT * FROM upcomingMatch", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            sda.Fill(dtable);

            conn.Open();
            StringBuilder sbuid = new StringBuilder();
            sbuid.Append("<center>");
            sbuid.Append("<hr/>");
            sbuid.Append("<table border~4>");
            sbuid.Append("<tr style='background-color:green; color: White;'>");
            foreach (DataColumn dc in dtable.Columns)
            {
                sbuid.Append("<th>");
                sbuid.Append(dc.ColumnName.ToUpper());
                sbuid.Append("</th>");
            }
            sbuid.Append("</tr>");

            foreach (DataRow dr in dtable.Rows)
            {
                sbuid.Append("</tr>");

                foreach (DataColumn dc in dtable.Columns)
                {
                    sbuid.Append("<th>");
                    sbuid.Append(dr[dc.ColumnName].ToString());
                    sbuid.Append("</th>");
                }
            }
            sbuid.Append("</table>");
            sbuid.Append("</center>");
            Label label1 = new Label();
            label1.Text = sbuid.ToString();
            Panel1.Controls.Add(label1);
            conn.Close();
        }

        protected void Onclick_RemoveView1(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Panel1.Controls.Clear();
        }
    }
}