using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Milestone_3
{
    public partial class Fan_Registeration : System.Web.UI.Page
    {
        public object DateTimeSyles { get; private set; }

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

            if (txt_nationalID.Text == "")
            {
                Label label = new Label();
                label.Text = "National ID Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (txt_phonenumber.Text == "")
            {
                Label label = new Label();
                label.Text = "Phone number Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (String.IsNullOrWhiteSpace(txt_birthdate.Text))
            {
                Label label = new Label();
                label.Text = "Birth Date Field can not be empty" + "<br></br>";
                Panel1.Controls.Add(label);
                flag = true;
            }

            if (txt_address.Text == "")
            {
                Label label = new Label();
                label.Text = "Address Field can not be empty" + "<br></br>";
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
            string nationalID = txt_nationalID.Text;
            string phonenumber = txt_phonenumber.Text;
            DateTime birthdate = DateTime.Parse(txt_birthdate.Text);
            string address = txt_address.Text;


            bool found = false;
            bool sameNationalID = false;

            SqlCommand insertFan = new SqlCommand("addFan", conn);
            insertFan.CommandType = System.Data.CommandType.StoredProcedure;
            insertFan.Parameters.Add(new SqlParameter("@fanname", name));
            insertFan.Parameters.Add(new SqlParameter("@fanusername", username));
            insertFan.Parameters.Add(new SqlParameter("@fanpassword", password));
            insertFan.Parameters.Add(new SqlParameter("@nationalID", nationalID));
            insertFan.Parameters.Add(new SqlParameter("@birthdate", birthdate));
            insertFan.Parameters.Add(new SqlParameter("@address", address));
            insertFan.Parameters.Add(new SqlParameter("@phonenumber", phonenumber));

            SqlCommand readAllSystemUsers = new SqlCommand("SELECT * FROM SystemUser", conn);
            SqlCommand readAllFans = new SqlCommand("SELECT * FROM allFans", conn);


            conn.Open();
            SqlDataReader rdrSystemuser = readAllSystemUsers.ExecuteReader();
            while (rdrSystemuser.Read())
            {
                string systemuserusername = rdrSystemuser.GetString(rdrSystemuser.GetOrdinal("username"));

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
                SqlDataReader rdrAllFans = readAllFans.ExecuteReader();
                while (rdrAllFans.Read())
                {
                    string fanrusername = rdrAllFans.GetString(rdrAllFans.GetOrdinal("username"));
                    string fannationalId = rdrAllFans.GetString(rdrAllFans.GetOrdinal("NationalID"));

                    if (fannationalId.Equals(nationalID))
                    {
                        sameNationalID = true;
                    }
                }
                rdrAllFans.Close();

                if (sameNationalID)
                {
                    Label label = new Label();
                    label.Text = "National ID exist";
                    Panel1.Controls.Add(label);
                }
                else
                {
                    insertFan.ExecuteNonQuery();
                    Label label = new Label();
                    label.Text = "Registered Successfully";
                    Panel1.Controls.Add(label);
                }
            }

            txt_name.Text = "";
            txt_username.Text = "";
            txt_password.Text = "";
            txt_nationalID.Text = "";
            txt_phonenumber.Text = "";
            txt_birthdate.Text = "";
            txt_address.Text = "";

            conn.Close();
        }
    }
}