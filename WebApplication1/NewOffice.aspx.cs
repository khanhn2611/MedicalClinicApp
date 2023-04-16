﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class NewOffice : System.Web.UI.Page
    {
        private string adminID;
        protected void Page_Load(object sender, EventArgs e)
        {
            adminID = Request.QueryString["adminID"];
        }
        protected void SUBMIT_Click_AddOff(object sender, EventArgs e)
        {
            string connString = "Server=medicaldatabase3380.mysql.database.azure.com;Database=medicalclinicdb2;Uid=dbadmin;Pwd=Medical123!;";
            MySqlConnection connection = new MySqlConnection(connString);

            connection.Open();

            try
            {
                // Insert new office
                string sql = "INSERT INTO office (officeAddress, phone_num, email) VALUES (@address, @phone, @email)";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@address", address.Text);
                command.Parameters.AddWithValue("@phone", phone.Text);
                command.Parameters.AddWithValue("@email", email.Text);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message + '\n');
            }

            connection.Close();
        }
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            address.Text = "";
        }
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminView.aspx?adminID=" + adminID);
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}