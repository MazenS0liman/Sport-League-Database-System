using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            bool flag = false;

            if (username_input.Text == "")
            {
                Label label = new Label();
                label.Text = "Username Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (password_input.Text == "")
            {
                Label label = new Label();
                label.Text = "Password Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (flag)
            {
                return;
            }


            //To read the input from the user
            string username = username_input.Text;
            string password = password_input.Text;

            SqlCommand readAllSystemUsers = new SqlCommand("SELECT * FROM SystemUser", conn);
            SqlCommand readAllAssocManagers = new SqlCommand("SELECT * FROM aLLAssocManagers", conn);
            SqlCommand readAllClubRepresentatives = new SqlCommand("SELECT * FROM allClubRepresentatives", conn);
            SqlCommand readAllStadiumManagers = new SqlCommand("SELECT * FROM allStadiumManagers", conn);
            SqlCommand readAllFans = new SqlCommand("SELECT * FROM allFans", conn);

            bool issystemuser = false;
            bool associativemanager = false;
            bool clubrepresentative = false;
            bool stadiummanager = false;
            bool fans = false;
            bool blocked = false;

            conn.Open();
            SqlDataReader rdrSystemuser = readAllSystemUsers.ExecuteReader();
            while (rdrSystemuser.Read())
            {
                string systemuserusername = rdrSystemuser.GetString(rdrSystemuser.GetOrdinal("username"));
                string systemuserpassword = rdrSystemuser.GetString(rdrSystemuser.GetOrdinal("password"));
                if (systemuserusername.Equals(username) && systemuserpassword.Equals(password))
                {
                    issystemuser = true;
                }
            }
            rdrSystemuser.Close();

            SqlDataReader rdrAssoc = readAllAssocManagers.ExecuteReader();
            while (rdrAssoc.Read())
            {
                string asscoiativeusername = rdrAssoc.GetString(rdrAssoc.GetOrdinal("username"));
                if (asscoiativeusername.Equals(username))
                {
                    associativemanager = true;
                }
            }
            rdrAssoc.Close();



            SqlDataReader rdrRepresentatives = readAllClubRepresentatives.ExecuteReader();
            while (rdrRepresentatives.Read())
            {
                string clubrepresentativeusername = rdrRepresentatives.GetString(rdrRepresentatives.GetOrdinal("username"));
                if (clubrepresentativeusername.Equals(username))
                {
                    clubrepresentative = true;
                }
            }
            rdrRepresentatives.Close();

            SqlDataReader rdrStadiumManager = readAllStadiumManagers.ExecuteReader();
            while (rdrStadiumManager.Read())
            {
                string stadiummanagerusername = rdrStadiumManager.GetString(rdrStadiumManager.GetOrdinal("username"));
                if (stadiummanagerusername.Equals(username))
                {
                    stadiummanager = true;
                }
            }
            rdrStadiumManager.Close();


            SqlDataReader rdrFans = readAllFans.ExecuteReader();
            while (rdrFans.Read())
            {
                string fansusername = rdrFans.GetString(rdrFans.GetOrdinal("username"));
                bool status = rdrFans.GetBoolean(rdrFans.GetOrdinal("Status"));

                if (fansusername.Equals(username))
                {
                    fans = true;
                }
                if (fansusername.Equals(username) && !status)
                {
                    blocked = true;
                }
            }
            rdrFans.Close();
            conn.Close();

            Session["user"] = username;

            if (!issystemuser)
            {
                Label label = new Label();
                label.Text = "Incorrect username or password" + "<br></br>";
                Panel1.Controls.Add(label);
                username_input.Text = "";
                password_input.Text = "";

            }
            else if (associativemanager)
            {
                associativemanager = false;
                Response.Redirect("Sport Association Manager.aspx", true);
            }

            else if (clubrepresentative)
            {
                clubrepresentative = false;
                Response.Redirect("Club Representative.aspx", true);
            }

            else if (stadiummanager)
            {
                stadiummanager = false;
                Response.Redirect("Stadium Manager.aspx", true);
            }

            else if (fans)
            {

                fans = false;
                if (blocked)
                {
                    Label label = new Label();
                    label.Text = "You are blocked by System Admin" + "<br></br>";
                    Panel1.Controls.Add(label);
                    username_input.Text = "";
                    password_input.Text = "";
                }
                else
                {
                    Response.Redirect("Fan.aspx", true);
                }
            }
            else if (issystemuser)
            {
                issystemuser = false;
                Response.Redirect("System Admin Page.aspx", true);
            }

            username_input.Text = "";
            password_input.Text = "";

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            Response.Redirect("Registeration.aspx", true);
        }
    }
}