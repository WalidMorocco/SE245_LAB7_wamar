using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SE245_LAB5_wamar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string strPersonV2_Phone)
        {
            InitializeComponent();
            PersonV2 temp = new PersonV2();
            SqlDataReader dr = temp.FindOnePerson(strPersonV2_Phone);
            while (dr.Read())
            {
                txtFName.Text = dr["FName"].ToString();
                txtMName.Text = dr["MName"].ToString();
                txtLName.Text = dr["LName"].ToString();
                txtStreet1.Text = dr["Street1"].ToString();
                txtStreet2.Text = dr["Street2"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtState.Text = dr["State"].ToString();
                txtZip.Text = dr["Zip"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtInstagram.Text = dr["Instagram"].ToString();
                txtCellPhone.Text = dr["CellPhone"].ToString();

            }

        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            Customer temp = new Customer();

            temp.FName = txtFName.Text;
            temp.MName = txtMName.Text;
            temp.LName = txtLName.Text;
            temp.Street1 = txtStreet1.Text;
            temp.Street2 = txtStreet2.Text;
            temp.City = txtCity.Text;
            temp.State = txtState.Text;
            temp.Zip = txtZip.Text;
            temp.Phone = txtPhone.Text;
            temp.Email = txtEmail.Text;
            temp.Instagram = txtInstagram.Text;
            temp.CellPhone = txtCellPhone.Text;
            temp.CustomerSince = dtpCustomerSince.Value;
            temp.TotalPurchases = Double.Parse(txtTotalPurchases.Text);
            temp.DiscountMember = Boolean.Parse(txtDiscountMember.Text);
            temp.RewardsEarned = Int32.Parse(txtRewardsEarned.Text);

            if (temp.Feedback.Contains("ERROR :"))
            {
                lblFeedback.Text = temp.Feedback;
            }
            else
            {
                string strFeedback = temp.AddARecord();
                lblFeedback.Text = $"{temp.FName} {temp.MName} {temp.LName}\n{temp.Street1} {temp.Street2} {temp.City} {temp.State} {temp.Zip}\n{temp.Phone}  -  {temp.CellPhone}\n{temp.Email}\n{temp.Instagram}\n{temp.CustomerSince}\n{temp.TotalPurchases}$\n{temp.DiscountMember}\n{temp.RewardsEarned}\n{strFeedback}";
            }
        }
    }
}
