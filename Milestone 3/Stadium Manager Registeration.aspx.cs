using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Stadium_Manager_Registeration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            bool flag = false;

            if (txt_name.Text == "")
            {
                Label label = new Label();
                label.Text = "Name Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (txt_password.Text == "")
            {
                Label label = new Label();
                label.Text = "Password Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (txt_username.Text == "")
            {
                Label label = new Label();
                label.Text = "Username Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (txt_stadiumname.Text == "")
            {
                Label label = new Label();
                label.Text = "Stadium Name Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }

            string name = txt_name.Text;
            string username = txt_username.Text;
            string password = txt_password.Text;
            string stadiumname = txt_stadiumname.Text;

            bool found = false;
            bool stadiumamefound = false;

            SqlCommand insertStadiumManager = new SqlCommand("addStadiumManager", conn);
            insertStadiumManager.CommandType = System.Data.CommandType.StoredProcedure;
            insertStadiumManager.Parameters.Add(new SqlParameter("@name", name));
            insertStadiumManager.Parameters.Add(new SqlParameter("@stadiumname", stadiumname));
            insertStadiumManager.Parameters.Add(new SqlParameter("@username", username));
            insertStadiumManager.Parameters.Add(new SqlParameter("@password", password));

            SqlCommand haveStadiumManager = new SqlCommand("HaveStadiumManager", conn);
            haveStadiumManager.CommandType = System.Data.CommandType.StoredProcedure;
            haveStadiumManager.Parameters.Add(new SqlParameter("@stadiumname", stadiumname));
            SqlParameter success = haveStadiumManager.Parameters.Add("@success", SqlDbType.Int);
            success.Direction = ParameterDirection.Output;

            SqlCommand readAllStadiums = new SqlCommand("SELECT * FROM allStadiums", conn);
            SqlCommand readAllSystemUsers = new SqlCommand("SELECT * FROM SystemUser", conn);


            conn.Open();
            SqlDataReader rdrSystemuser = readAllSystemUsers.ExecuteReader();
            while (rdrSystemuser.Read())
            {
                string systemuserusername = rdrSystemuser.GetString(rdrSystemuser.GetOrdinal("username"));
                string systemuserpassword = rdrSystemuser.GetString(rdrSystemuser.GetOrdinal("password"));
                if (systemuserusername.Equals(username))
                {
                    found = true;
                }
            }
            rdrSystemuser.Close();

            if (found)
            {
                Label label = new Label();
                label.Text = "Username already exist";
                Panel1.Controls.Add(label);
            }
            else
            {

                SqlDataReader rdrAllclubs = readAllStadiums.ExecuteReader();
                while (rdrAllclubs.Read())
                {
                    string stadiummanagername = rdrAllclubs.GetString(rdrAllclubs.GetOrdinal("name"));
                    if (stadiummanagername.Equals(stadiumname))
                    {
                        stadiumamefound = true;

                    }
                }
                rdrAllclubs.Close();

                if (!stadiumamefound)
                {
                    Label label = new Label();
                    label.Text = "Stadium does not exist";
                    Panel1.Controls.Add(label);
                }
                else
                {
                    haveStadiumManager.ExecuteNonQuery();

                    if (success.Value.ToString() == "1")
                    {
                        Label label = new Label();
                        label.Text = "Stadium " + stadiumname + " already have Stadium Manager";
                        Panel1.Controls.Add(label);
                    }
                    else
                    {
                        insertStadiumManager.ExecuteNonQuery();
                        Label label = new Label();
                        label.Text = "Registered Successfully";
                        Panel1.Controls.Add(label);
                    }

                }
            }

            txt_name.Text = "";
            txt_username.Text = "";
            txt_password.Text = "";
            txt_stadiumname.Text = "";

            conn.Close();

        }
    }
}