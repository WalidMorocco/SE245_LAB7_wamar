using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE245_LAB5_wamar
{
    public partial class SearchPersons : Form
    {
        public SearchPersons()
        {
            InitializeComponent();
        }

        private void btnSearchPersons_Click(object sender, EventArgs e)
        {
            PersonV2 temp = new PersonV2();
            DataSet ds = temp.SearchPersons(txtFName.Text, txtLName.Text);
            dgvResults.DataSource = ds;                                  
            dgvResults.DataMember = ds.Tables["Persons_Temp"].ToString();     

        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strPersonV2_Phone = dgvResults.Rows[e.RowIndex].Cells[0].Value.ToString();
            MessageBox.Show(strPersonV2_Phone);
            Form1 Editor = new Form1(strPersonV2_Phone);
            Editor.ShowDialog();
        }
    }
}
