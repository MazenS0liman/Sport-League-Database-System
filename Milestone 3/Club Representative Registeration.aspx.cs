using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Club_Representative_Registeration : System.Web.UI.Page
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

            if (txt_clubname.Text == "")
            {
                Label label = new Label();
                label.Text = "Club Name Field can not be empty" + "<br></br>";
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
            string clubname = txt_clubname.Text;

            bool found = false;
            bool clubnamefound = false;
            bool clubalreadyhaverepresetative = false;

            SqlCommand insertRepresentative = new SqlCommand("addRepresentative", conn);
            insertRepresentative.CommandType = System.Data.CommandType.StoredProcedure;
            insertRepresentative.Parameters.Add(new SqlParameter("@name", name));
            insertRepresentative.Parameters.Add(new SqlParameter("@clubname", clubname));
            insertRepresentative.Parameters.Add(new SqlParameter("@username", username));
            insertRepresentative.Parameters.Add(new SqlParameter("@password", password));

            SqlCommand haveRepresentative = new SqlCommand("HaveRepresentative", conn);
            haveRepresentative.CommandType = System.Data.CommandType.StoredProcedure;
            haveRepresentative.Parameters.Add(new SqlParameter("@clubname", clubname));
            SqlParameter success = haveRepresentative.Parameters.Add("@success", SqlDbType.Int);
            success.Direction = ParameterDirection.Output;

            SqlCommand readAllClubs = new SqlCommand("SELECT * FROM allCLubs", conn);
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

                SqlDataReader rdrAllclubs = readAllClubs.ExecuteReader();
                while (rdrAllclubs.Read())
                {
                    string represetativeclubname = rdrAllclubs.GetString(rdrAllclubs.GetOrdinal("name"));
                    if (represetativeclubname.Equals(clubname))
                    {
                        clubnamefound = true;

                    }
                }
                rdrAllclubs.Close();

                if (!clubnamefound)
                {
                    Label label = new Label();
                    label.Text = "Club Name does not exist";
                    Panel1.Controls.Add(label);
                    txt_name.Text = "";
                    txt_username.Text = "";
                    txt_password.Text = "";
                    txt_clubname.Text = "";
                }
                else
                {
                    haveRepresentative.ExecuteNonQuery();
                    if (success.Value.ToString() == "1")
                    {
                        Label label = new Label();
                        label.Text = "Club " + clubname + " already have representative";
                        Panel1.Controls.Add(label);
                        txt_name.Text = "";
                        txt_username.Text = "";
                        txt_password.Text = "";
                        txt_clubname.Text = "";
                    }
                    else
                    {
                        insertRepresentative.ExecuteNonQuery();
                        Label label = new Label();
                        label.Text = "Registered Successfully";
                        Panel1.Controls.Add(label);
                        txt_name.Text = "";
                        txt_username.Text = "";
                        txt_password.Text = "";
                        txt_clubname.Text = "";
                    }

                }
            }

            conn.Close();
        }
    }
}