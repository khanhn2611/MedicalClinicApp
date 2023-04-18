﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAppointment.aspx.cs" Inherits="WebApplication1.NewAppointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>NEW APPOINTMENT</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script> 
    	<link href="Content/navbar.css" rel="stylesheet" type="text/css"/>
        <link href="Content/AdminPages.css" rel="stylesheet" type="text/css"/>
</head>
<body>
       <form id="form1" runat="server">
    <div class="navbar">
      <a href="AboutUs.aspx">About Us</a>
      <a href="ContactUs.aspx">Contact Us</a>
      <a href="PatientLogin.aspx">Patient Login</a>
      <a href="ProviderLogin.aspx">Provider Login</a>
      <a href="HomePage.aspx">Home</a>
      <left><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton></left>
    </div>


     <h1 style ="font-size: 32px; width: 907px;" runat="server">
            &nbsp;</h1>


    <h1 style = "text-align: center">Schedule your Appointment Today</h1>
	<p style = "text-align: center">Please fill out where indicated</p>

		<div class="form-container">
			<fieldset>
			<legend>Appointment Details</legend>

            <div class="form-group">
				<label for="SpecDept">
                <br />
                Select a Specialty <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="specdept" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="specdept_SelectedIndexChanged" >
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem>Oncology</asp:ListItem>
                    <asp:ListItem>Dermatology</asp:ListItem>
                    <asp:ListItem>Cardiology</asp:ListItem>
                    <asp:ListItem>Neurology</asp:ListItem>
                    <asp:ListItem>Nephrology</asp:ListItem>
                    <asp:ListItem>Internal Medicine</asp:ListItem>
                </asp:DropDownList>
			    <br />
                <br />
			</div>

            <div class="form-group">
				<label for="date-requested">Date Requested <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="date_requested" runat="server" TextMode="Date" DataFormatString="yyyy-MM-dd" AutoPostBack="True" OnTextChanged="date_requested_TextChanged"></asp:TextBox>
			
			   <span class="required"> <asp:Literal ID="ErrorMessage_date2" EnableViewState="False"  runat="server"></asp:Literal></span>
			
			</div>

            <div class="form-group">
				<label for="officelabel">
                <br />
                Select a office location <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" Width="140px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
			    <br />
                <br />
			</div>
                			
            <div class="form-group">
				<label for="Speclabel">
                <br />
                Select a Specialist Physician <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="specialist" runat="server" Width="140px" AutoPostBack="True" ></asp:DropDownList>
			    <br />
                <br />
			</div>



			<div class="form-group">
				<label for="time-requested">
                <br />
                Time Requested <span class="required">*</span></label>&nbsp;
                <asp:DropDownList ID="ddlTimeRequested" runat="server" Width="140px"  AutoPostBack="True"></asp:DropDownList>
			   <span class="required"> <asp:Literal ID="ErrorMessage_date" EnableViewState="False"  runat="server"></asp:Literal></span>
			    <br />
                <br />
			</div>
                <br />
            <div class="form-group">
				<label for="date-requested">Reason for Visit <span class="required">*</span></label>&nbsp;
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
			<br />
			
			</div>

           <div class="form-group">
				<label for="referral">
                <br />
                Do you have a referral?<span class="required">*</span></label>
                <asp:CheckBox ID="CheckBox5" runat="server" />
                <label for="referral">Yes</label>
                <asp:CheckBox ID="CheckBox6" runat="server" />
                <label for="referral">No<br />
                <br />
                </label>
			&nbsp;</div>

			<p><span class="required">*</span> Required information</p>
            <p>
                <asp:Button ID="SUBMIT" runat="server" Text="SUBMIT" OnClick="SUBMIT_Click" />
            </p>
                <p>
                    &nbsp;</p>
	</fieldset>
    
     </div>    
       
            


            <script>
                //Client-side jQuery to cancel form submission if required fields empty 
                $(function () {
                    // Attach event handler to form submit button click
                    $('#<%=SUBMIT.ClientID %>').on('click', function () {
                        // Check if required fields are filled out
                        if ($('#<%=specdept.ClientID %>').val() === "" || $('#<%=DropDownList1.ClientID %>').val() === "" || $('#<%=ddlTimeRequested.ClientID %>').val() === ""
                            || $('#<%=specialist.ClientID %>').val() === "" || $('#<%=date_requested.ClientID %>').val() === "") {
                            // Display dialog box
                            alert('Please fill out all required fields.');
                            return false; // Cancel form submission
                        }
                    });
                });
            </script>
       </form>
    <div class="footer">
  <section class="contact">
    <p>Email: info@coogmedicalgroup.com | Phone: (713)867-5309</p>
    <p>Coog Clinic © Group 13 - 2023. All rights reserved.</p>
    <p><a href="AdminLogin.aspx">Admin Login</a></p>
  </section>
</div>
</body>
</html>
