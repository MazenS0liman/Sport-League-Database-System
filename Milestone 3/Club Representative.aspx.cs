using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Club_Representative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void clubInfo(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            string username = Session["user"].ToString();
            string clubname = "";

            SqlCommand allClubRepresentativesCommand = new SqlCommand("SELECT * FROM allClubRepresentatives", conn);
            SqlCommand allClubCommand = new SqlCommand("SELECT * FROM allCLubs", conn);

            conn.Open();

            SqlDataReader readallClubRepresentatives = allClubRepresentativesCommand.ExecuteReader();
            while (readallClubRepresentatives.Read())
            {
                // Get the representative club name
                string representativeusername = readallClubRepresentatives.GetString(readallClubRepresentatives.GetOrdinal("username"));

                if (representativeusername.Equals(username))
                    clubname = readallClubRepresentatives.GetString(readallClubRepresentatives.GetOrdinal("Club Name"));

            }
            readallClubRepresentatives.Close();

            SqlDataReader readAllClub = allClubCommand.ExecuteReader();
            while (readAllClub.Read())
            {
                string name = readAllClub.GetString(readAllClub.GetOrdinal("Name"));
                string location = readAllClub.GetString(readAllClub.GetOrdinal("Location"));
                if (name.Equals(clubname))
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
                    sbuild.Append("</tr>");
                    sbuild.Append("<th>");
                    sbuild.Append(name);
                    sbuild.Append("</th>");
                    sbuild.Append("<th>");
                    sbuild.Append(location);
                    sbuild.Append("</th>");
                    sbuild.Append("</table>");
                    sbuild.Append("</center>");
                    Label label1 = new Label();
                    label1.Text = sbuild.ToString();
                    Panel1.Controls.Add(label1);
                }
            }
            readAllClub.Close();
            conn.Close();
        }

        protected void upComingMatches(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            string username = Session["user"].ToString();
            string clubname = "";

            SqlCommand allClubRepresentativesCommand = new SqlCommand("SELECT * FROM allClubRepresentatives", conn);

            conn.Open();

            SqlDataReader readallClubRepresentatives = allClubRepresentativesCommand.ExecuteReader();
            while (readallClubRepresentatives.Read())
            {
                // Get the representative club name
                string representativeusername = readallClubRepresentatives.GetString(readallClubRepresentatives.GetOrdinal("username"));

                if (representativeusername.Equals(username))
                    clubname = readallClubRepresentatives.GetString(readallClubRepresentatives.GetOrdinal("Club Name"));

            }
            readallClubRepresentatives.Close();
            conn.Close();

            SqlCommand upCommingMatchesCommand = new SqlCommand("SELECT * From upcomingMatchesOfClub2(@clubname)", conn);
            upCommingMatchesCommand.Parameters.Add(new SqlParameter("@clubname", clubname));

            SqlDataAdapter sda = new SqlDataAdapter(upCommingMatchesCommand);
            DataTable dtable = new DataTable();
            sda.Fill(dtable);

            conn.Open();
            StringBuilder sbuild = new StringBuilder();
            sbuild.Append("<center>");
            sbuild.Append("<hr/>");
            sbuild.Append("<table border~4>");
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

        protected void avaibleStadiums(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (avaibledate.Text == "")
            {
                Label label = new Label();
                label.Text = "Date Field can not be empty";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            DateTime starttime = DateTime.Parse(avaibledate.Text);

            SqlCommand AvailableStadiumsCommand = new SqlCommand("SELECT * From viewAvailableStadiumsOn(@starttime)", conn);
            AvailableStadiumsCommand.Parameters.Add(new SqlParameter("@starttime", starttime));

            SqlDataAdapter sda = new SqlDataAdapter(AvailableStadiumsCommand);
            DataTable dtable = new DataTable();
            sda.Fill(dtable);

            conn.Open();
            StringBuilder sbuild = new StringBuilder();
            sbuild.Append("<center>");
            sbuild.Append("<hr/>");
            sbuild.Append("<table border~4>");
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
            avaibledate.Text = "";
            conn.Close();
        }

        protected void sendRequest(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (stadiumName.Text == "")
            {
                Label label = new Label();
                label.Text = "Stadium Name Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (startingtime.Text == "")
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

            string username = Session["user"].ToString();
            string stadiumname = stadiumName.Text;
            DateTime starttime = DateTime.Parse(startingtime.Text);
            string clubname = "";

            SqlCommand allClubRepresentativesCommand = new SqlCommand("SELECT * FROM allClubRepresentatives", conn);

            conn.Open();

            SqlDataReader readallClubRepresentatives = allClubRepresentativesCommand.ExecuteReader();
            while (readallClubRepresentatives.Read())
            {
                // Get the representative club name
                string representativeusername = readallClubRepresentatives.GetString(readallClubRepresentatives.GetOrdinal("username"));

                if (representativeusername.Equals(username))
                    clubname = readallClubRepresentatives.GetString(readallClubRepresentatives.GetOrdinal("Club Name"));
            }
            readallClubRepresentatives.Close();
            conn.Close();

            SqlCommand addHostRequestCommand = new SqlCommand("addHostRequest", conn);
            addHostRequestCommand.CommandType = CommandType.StoredProcedure;
            addHostRequestCommand.Parameters.Add(new SqlParameter("@clubname", clubname));
            addHostRequestCommand.Parameters.Add(new SqlParameter("@stadiumname", stadiumname));
            addHostRequestCommand.Parameters.Add(new SqlParameter("@starttime", starttime));

            SqlCommand haveStadiumManager = new SqlCommand("HaveStadiumManager", conn);
            haveStadiumManager.CommandType = System.Data.CommandType.StoredProcedure;
            haveStadiumManager.Parameters.Add(new SqlParameter("@stadiumname", stadiumname));
            SqlParameter success = haveStadiumManager.Parameters.Add("@success", SqlDbType.Int);
            success.Direction = ParameterDirection.Output;

            SqlCommand unassigndMatch = new SqlCommand("SELECT * FROM allUnassignedMatches(@hostname)", conn);
            unassigndMatch.Parameters.Add(new SqlParameter("@hostname", clubname));

            SqlCommand allStadiums = new SqlCommand("SELECT * FROM allStadiums", conn);

            bool foundUnassignedMatch = false;
            bool foundStadium = false;

            conn.Open();

            SqlDataReader readallunassignedMatches = unassigndMatch.ExecuteReader();
            while (readallunassignedMatches.Read())
            {
                // Get the representative club name
                DateTime time = readallunassignedMatches.GetDateTime(readallunassignedMatches.GetOrdinal("Starttime")); ;

                if (time == starttime)
                    foundUnassignedMatch = true;
            }
            readallunassignedMatches.Close();

            SqlDataReader readallStadiums = allStadiums.ExecuteReader();
            while (readallStadiums.Read())
            {
                string s = readallStadiums.GetString(readallStadiums.GetOrdinal("Name"));
                if (s.Equals(stadiumname))
                {
                    foundStadium = true;
                }

            }
            readallStadiums.Close();

            haveStadiumManager.ExecuteNonQuery();

            if (!foundStadium)
            {
                Label label = new Label();
                label.Text = "Stadium " + stadiumname + " does not exist" + "<br></br>";
                Panel4.Controls.Add(label);
            }
            else if (!success.Value.ToString().Equals("1"))
            {
                Label label = new Label();
                label.Text = "Stadium " + stadiumname + " does not have stadium manager" + "<br></br>";
                Panel4.Controls.Add(label);
            }
            else if (!foundUnassignedMatch)
            {
                Label label = new Label();
                label.Text = "Club " + clubname + " does not have a match unassigned to a stadium at " + starttime + " as a host" + "<br></br>";
                Panel4.Controls.Add(label);
            }
            else
            {
                addHostRequestCommand.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Request is sent successfully" + "<br></br>";
                Panel4.Controls.Add(label);
            }

            stadiumName.Text = "";
            startingtime.Text = "";

            conn.Close();
        }
    }
}