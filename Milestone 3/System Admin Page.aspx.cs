using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Label = System.Web.UI.WebControls.Label;

namespace Milestone_3
{
    public partial class System_Admin_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void addClub_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (add_club_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Club Name Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (add_club_location.Text == "")
            {
                Label label = new Label();
                label.Text = "Location Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string clubname = add_club_name.Text;
            string location = add_club_location.Text;



            SqlCommand allClubCommand = new SqlCommand("SELECT * FROM allCLubs", conn);
            SqlCommand addClub = new SqlCommand("addClub", conn);
            addClub.CommandType = CommandType.StoredProcedure;
            addClub.Parameters.Add(new SqlParameter("@clubname", clubname));
            addClub.Parameters.Add(new SqlParameter("@location", location));


            bool found = false;

            conn.Open();
            SqlDataReader readAllClub = allClubCommand.ExecuteReader();
            while (readAllClub.Read())
            {
                string name = readAllClub.GetString(readAllClub.GetOrdinal("Name"));
                if (name.Equals(clubname))
                    found = true;
            }
            readAllClub.Close();


            if (!found)
            {
                addClub.ExecuteNonQuery();
                Label label = new Label();
                label.Text = clubname + " is added successfully";
                Panel1.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = clubname + " already exist";
                Panel1.Controls.Add(label);
            }

            add_club_name.Text = "";
            add_club_location.Text = "";

            conn.Close();
        }


        protected void deleteClub_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (delete_club_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Club Name Field can not be empty" + "<br></br>";
                Panel2.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string clubname = delete_club_name.Text;



            SqlCommand allClubCommand = new SqlCommand("SELECT * FROM allCLubs", conn);
            SqlCommand deleteClub = new SqlCommand("deleteClub", conn);
            deleteClub.CommandType = CommandType.StoredProcedure;
            deleteClub.Parameters.Add("@clubname", clubname);

            bool found = false;

            conn.Open();
            SqlDataReader readAllClub = allClubCommand.ExecuteReader();
            while (readAllClub.Read())
            {
                string name = readAllClub.GetString(readAllClub.GetOrdinal("Name"));
                if (name.Equals(clubname))
                    found = true;
            }
            readAllClub.Close();


            if (found)
            {
                deleteClub.ExecuteNonQuery();
                Label label = new Label();
                label.Text = clubname + " is removed successfully";
                Panel2.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = clubname + " does not exist";
                Panel2.Controls.Add(label);
            }

            delete_club_name.Text = "";

            conn.Close();
        }

        protected void addStadium_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (add_stadium_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Stadium Name Field can not be empty" + "<br></br>";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (add_stadium_location.Text == "")
            {
                Label label = new Label();
                label.Text = "Location Field can not be empty" + "<br></br>";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (add_stadium_capacity.Text == "")
            {
                Label label = new Label();
                label.Text = "Capacity Field can not be empty" + "<br></br>";
                Panel3.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string stadiumname = add_stadium_name.Text;
            string location = add_stadium_location.Text;
            int capacity = (int)Int64.Parse(add_stadium_capacity.Text);

            if (capacity == 0)
            {
                Label label = new Label();
                label.Text = "Can not insert zero into Capacity Field" + "<br></br>";
                Panel3.Controls.Add(label);
                return;
            }

            if (capacity < 0)
            {
                Label label = new Label();
                label.Text = "Can not insert negative number into Capacity Field" + "<br></br>";
                Panel3.Controls.Add(label);
                return;
            }


            SqlCommand allStadiumsCommand = new SqlCommand("SELECT * FROM allStadiums", conn);
            SqlCommand addStadium = new SqlCommand("addStadium", conn);
            addStadium.CommandType = CommandType.StoredProcedure;
            addStadium.Parameters.Add(new SqlParameter("@stadiumname", stadiumname));
            addStadium.Parameters.Add(new SqlParameter("@location", location));
            addStadium.Parameters.Add(new SqlParameter("@capacity", capacity));


            bool found = false;

            conn.Open();
            SqlDataReader readAllStadium = allStadiumsCommand.ExecuteReader();
            while (readAllStadium.Read())
            {
                string name = readAllStadium.GetString(readAllStadium.GetOrdinal("Name"));
                if (name.Equals(stadiumname))
                    found = true;
            }
            readAllStadium.Close();


            if (!found)
            {
                addStadium.ExecuteNonQuery();
                Label label = new Label();
                label.Text = stadiumname + " is added successfully";
                Panel3.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = stadiumname + " already exist";
                Panel3.Controls.Add(label);
            }

            add_stadium_name.Text = "";
            add_stadium_location.Text = "";
            add_stadium_capacity.Text = "";

            conn.Close();
        }

        protected void deleteStadium_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (delete_stadium_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Stadium Name Field can not be empty" + "<br></br>";
                Panel4.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }


            string stadiumname = delete_stadium_name.Text;

            SqlCommand allStadiumsCommand = new SqlCommand("SELECT * FROM allStadiums", conn);
            SqlCommand removeStadium = new SqlCommand("deleteStadium", conn);
            removeStadium.CommandType = CommandType.StoredProcedure;
            removeStadium.Parameters.Add("@stadiumname", stadiumname);

            bool found = false;

            conn.Open();
            SqlDataReader readAllStadium = allStadiumsCommand.ExecuteReader();
            while (readAllStadium.Read())
            {
                string name = readAllStadium.GetString(readAllStadium.GetOrdinal("Name"));
                if (name.Equals(stadiumname))
                    found = true;
            }
            readAllStadium.Close();


            if (found)
            {
                removeStadium.ExecuteNonQuery();
                Label label = new Label();
                label.Text = stadiumname + " is removed successfully";
                Panel4.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = stadiumname + " does not exist";
                Panel4.Controls.Add(label);
            }

            delete_stadium_name.Text = "";

            conn.Close();
        }

        protected void blockFan_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (fannationalID.Text == "")
            {
                Label label = new Label();
                label.Text = "National ID Field can not be empty";
                Panel5.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string nationalID = fannationalID.Text;
            string name = "";

            SqlCommand AllFanCommand = new SqlCommand("SELECT * FROM allFans", conn);

            SqlCommand blockFan = new SqlCommand("blockFan", conn);
            blockFan.CommandType = CommandType.StoredProcedure;
            blockFan.Parameters.Add(new SqlParameter("@fannationalid", nationalID));

            bool found = false;

            conn.Open();

            SqlDataReader readAllFan = AllFanCommand.ExecuteReader();
            while (readAllFan.Read())
            {
                string f_nationalId = readAllFan.GetString(readAllFan.GetOrdinal("NationalID"));
                string f_name = readAllFan.GetString(readAllFan.GetOrdinal("Name"));
                if (f_nationalId.Equals(nationalID))
                {
                    found = true;
                    name = f_name;
                }
            }
            readAllFan.Close();

            if (found)
            {
                blockFan.ExecuteNonQuery();
                Label label = new Label();
                label.Text = name + " is blocked";
                Panel5.Controls.Add(label);
            }
            else
            {
                Label label = new Label();
                label.Text = "There is no fan having a NationalId: " + nationalID + " ";
                Panel5.Controls.Add(label);
            }

            fannationalID.Text = "";

            conn.Close();

        }
    }
}