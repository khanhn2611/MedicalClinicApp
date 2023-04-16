﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connString = "Server=medicaldatabase3380.mysql.database.azure.com;Database=medicalclinicdb2;Uid=dbadmin;Pwd=Medical123!;";
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            string query = $"SELECT * FROM login WHERE username=@username AND passwrd=@passwrd AND nurseID IS NOT NULL";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", UserName.Text);
            command.Parameters.AddWithValue("@passwrd", Password.Text);


            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (reader.HasRows)

            {
                int nurseID = (int)reader["nurseID"];
                Response.Redirect("NurseView.aspx?nurseID=" + nurseID);
            }

            else
            {
                ErrorMessage.Text = "Incorrect credentials";
            }

            reader.Close();
            connection.Close();
        }
    }
}