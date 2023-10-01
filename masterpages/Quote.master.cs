using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using umbraco.cms.businesslogic.member;
using umbraco.cms.businesslogic.media;
using System.Text.RegularExpressions;
using umbraco.BusinessLogic;
using System.Xml;

public partial class Quote : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		string filepath = "";
		
        if (fileUpEx.HasFile)
        {
            filepath = fileUpEx.PostedFile.FileName;
            Media userUploadRoot = new Media(1232); // prod
            Media ClientFile = Media.MakeNew(filepath, MediaType.GetByAlias("file"), umbraco.BusinessLogic.User.GetUser(0), userUploadRoot.Id);
            fileUpEx.PostedFile.SaveAs(Server.MapPath("\\media\\quote\\" + filepath));
            string mediaPath = "~/media/quote/" + filepath;
            ClientFile.getProperty("umbracoFile").Value = mediaPath;
            ClientFile.XmlGenerate(new XmlDocument());
            ClientFile.Save();  
        }
        
		// Send the email
        string name_ = txtFirstName.Value + " " + txtSurname.Value;
        string company_ = txtCompany.Value;
        string address_ = txtAddress.Value;
        string suburb_ = txtSuburb.Value;
        string state_ = txtState.Value;
        string postcode_ = txtPostCode.Value;
        string telephone_ = txtTelephone.Value;
        string email_ = txtEmail.Value;
        string propertyStatus_ = ddlPropertyStatus.Value;
        string projectStartDate_ = ddlProjectDate.Value;
        string comment_ = txtComments.Value;

        string formIpn = @"Name: " + name_ + "<br/>";
        formIpn += @"Company: " + company_ + "<br/>";
        formIpn += @"Address: " + address_ + "<br/>";
        formIpn += @"Suburb: " + suburb_ + "<br/>";
        formIpn += @"State: " + state_ + "<br/>";
        formIpn += @"Postcode: " + postcode_ + "<br/>";
        formIpn += @"Telephone: " + telephone_ + "<br/>";
        formIpn += @"Email: " + email_ + "<br/>";
        formIpn += @"Property Status: " + propertyStatus_ + "<br/>";
        formIpn += @"Project Start Date: " + projectStartDate_ + "<br/>";
        formIpn += @"Comments: " + comment_ + "<br/>";
        if (cblInterests_0.Checked || cblInterests_1.Checked || cblInterests_2.Checked)
        if (cblInterests_0.Checked)
        {
            formIpn += "Interested in residential<br/>";
        }
        if (cblInterests_1.Checked)
        {
            formIpn += "Interested in commercial<br/>";
        }
        if (cblInterests_2.Checked)
        {
            formIpn += "Interested in industrial<br/>";
        }
		if (filepath.Length > 0) {
			formIpn += "Download file attachment : <a href=\"http://www.encoengineering.com.au/media/quote/" + filepath + "\">Download</a>";
		}
		
		/*Response.Write(formIpn);*/
        //from, then to
        umbraco.library.SendMail("peterb@digitalresponse.com.au", "peterb@digitalresponse.com.au", "Enco Engineering - Request a Quote ", formIpn, true);
        umbraco.library.SendMail("peterb@digitalresponse.com.au", "quotes@pmdesign.com.au", "Enco Engineering - Request a Quote ", formIpn, true);

        lblInfo.Attributes.Remove("style");
        lblInfo.Attributes.Add("style", "font-weight:bold;display:block;color:white;padding:2px;margin-bottom:10px;");		
		lblInfo.Text = "Request for Quote sent successfully. We will respond shortly.";

// reset form
        txtAddress.Value = "";
        txtCompany.Value = "";
        txtEmail.Value = "";
        txtFirstName.Value = "";
        txtPostCode.Value = "";
        txtState.Value = "";
        txtSuburb.Value = "";
        txtSurname.Value = "";
        txtTelephone.Value = "";
        ddlProjectDate.SelectedIndex = 0;
        ddlPropertyStatus.SelectedIndex = 0;
        cblInterests_0.Checked = false;
        cblInterests_1.Checked = false;
        cblInterests_2.Checked = false;
		txtComments.Value = "";
		}
}