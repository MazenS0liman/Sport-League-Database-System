using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class SportAssociationManagerRegisteration : System.Web.UI.Page
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

            if (flag)
            {
                return;
            }

            string name = txt_name.Text;
            string username = txt_username.Text;
            string password = txt_password.Text;

            bool found = false;

            SqlCommand insertSPA = new SqlCommand("addAssociationManager", conn);
            insertSPA.CommandType = System.Data.CommandType.StoredProcedure;
            insertSPA.Parameters.Add(new SqlParameter("@name", name));
            insertSPA.Parameters.Add(new SqlParameter("@username", username));
            insertSPA.Parameters.Add(new SqlParameter("@password", password));

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
                label.Text = "Username exist";
                Panel1.Controls.Add(label);
            }
            else
            {
                insertSPA.ExecuteNonQuery();
                Label label = new Label();
                label.Text = "Registered Successfully";
                Panel1.Controls.Add(label);
            }

            txt_name.Text = "";
            txt_username.Text = "";
            txt_password.Text = "";

            conn.Close();
        }
    }
}