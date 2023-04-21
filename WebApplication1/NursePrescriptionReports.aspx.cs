﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class NursePrescriptionReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get Doctor Name for Welcome Header
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            string connectionString = "Server=medicaldatabase3380.mysql.database.azure.com;Database=medicalclinicdb2;Uid=dbadmin;Pwd=Medical123!;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT CONCAT('RN ', fname, ' ', lname) from nurse WHERE NID = @nurseID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("nurseID", nurseID);
            connection.Open();
            object result = command.ExecuteScalar();
            string fullname = result.ToString();
            LinkButton1.Text = "Logged in as: " + fullname;

            if (!IsPostBack)
            {
                // Set default start and end dates
                startDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
                endDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                string query_dr = "SELECT CONCAT('Dr. ', doctor.fname, ' ', doctor.lname) as docName, doctor.doctorID as doctorID FROM doctor, office, nurse, schedule WHERE nurse.NID = @nurseID AND nurse.officeID = office.officeID AND schedule.doctor = doctor.doctorID AND (schedule.Monday = office.officeID OR schedule.Tuesday = office.officeID OR schedule.Wednesday = office.officeID OR schedule.Thursday = office.officeID OR schedule.Friday = office.officeID)";

                MySqlCommand cmd_dr = new MySqlCommand(query_dr, connection);
                cmd_dr.Parameters.AddWithValue("@nurseID", nurseID);
                MySqlDataReader reader_dr = cmd_dr.ExecuteReader();
                while (reader_dr.Read())
                {
                    ListItem item = new ListItem(reader_dr["docName"].ToString(), reader_dr["doctorID"].ToString());
                    doctorName.Items.Add(item);
                }

                reader_dr.Close();
            }
            connection.Close();

        }

        protected void Generate_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected patient ID and date range
                //int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
                int patientID = Convert.ToInt32(patientName.SelectedValue);
                int doctorId = Convert.ToInt32(doctorName.SelectedValue);
                DateTime start, end;
                try
                {
                    start = DateTime.Parse(startDate.Text);
                    end = DateTime.Parse(endDate.Text);
                }
                catch
                {
                    start = DateTime.MinValue;
                    end = DateTime.MaxValue;
                }
                // Construct the connection string
                string connectionString = "Server=medicaldatabase3380.mysql.database.azure.com;Database=medicalclinicdb2;Uid=dbadmin;Pwd=Medical123!;";

                // Construct the query
                string query = "SELECT prescriptions.prescribed_date, patients.fName, patients.mInitial, patients.lName, prescriptions.drug_name, prescriptions.dosage, prescriptions.refills, prescriptions.notes " +
                               "FROM prescriptions " +
                               "JOIN patients ON prescriptions.patientID = patients.patientID " +
                               "WHERE patients.patientID=@patientID AND prescriptions.doctorId = @doctorId AND prescribed_date >= @start AND prescribed_date <= @end";

                // Retrieve the prescription data from the database
                DataTable prescriptions = new DataTable();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@doctorId", doctorId);
                    command.Parameters.AddWithValue("@start", start);
                    command.Parameters.AddWithValue("@end", end);
                    command.Parameters.AddWithValue("@patientID", patientID);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(prescriptions);
                }

                // Generate the report table
                Table reportTable = new Table();
                reportTable.ID = "reportTable";
                reportTable.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                // Generate the header row
                TableHeaderRow headerRow = new TableHeaderRow();
                headerRow.TableSection = TableRowSection.TableHeader;
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Prescribed Date" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Patient Name" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Drug Name" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Dosage" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Refills" });
                headerRow.Cells.Add(new TableHeaderCell() { Text = "Notes" });
                reportTable.Attributes.Add("class", "table-class"); // Add class attribute for styling

                reportTable.Rows.Add(headerRow);

                // Generate the data rows
                foreach (DataRow row in prescriptions.Rows)
                {
                    TableRow dataRow = new TableRow();
                    dataRow.TableSection = TableRowSection.TableBody;
                    dataRow.Cells.Add(new TableCell() { Text = ((DateTime)row["prescribed_date"]).ToString("MM/dd/yyyy") });
                    dataRow.Cells.Add(new TableCell() { Text = row["fName"].ToString() + " " + row["mInitial"].ToString() + " " + row["lName"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["drug_name"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["dosage"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["refills"].ToString() });
                    dataRow.Cells.Add(new TableCell() { Text = row["notes"].ToString() });
                    dataRow.Attributes.Add("class", "data-row-class"); // Add class attribute for styling

                    reportTable.Rows.Add(dataRow);
                }

                // Add the report table to the page
                reportDiv.Controls.Add(reportTable);
            }
            catch
            {
                //
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int nurseID = Convert.ToInt32(Request.QueryString["nurseID"]);
            Response.Redirect("nurseView.aspx?nurseID=" + nurseID);

        }
        protected void DoctorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            patientName.Items.Clear();
            string connectionString = "Server=medicaldatabase3380.mysql.database.azure.com;Database=medicalclinicdb2;Uid=dbadmin;Pwd=Medical123!;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query2 = "SELECT patients.patientID as patientID, CONCAT(patients.fname, ' ', patients.lname) as fullname FROM patients WHERE patients.doctorID = @doctorID";
            int doctorID = Convert.ToInt32(doctorName.SelectedValue);
            MySqlCommand cmd = new MySqlCommand(query2, connection);
            cmd.Parameters.AddWithValue("@doctorID", doctorID);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem item = new ListItem(reader["fullname"].ToString(), reader["patientID"].ToString());
                patientName.Items.Add(item);
            }
            reader.Close();
            connection.Close();
        }
    }
}