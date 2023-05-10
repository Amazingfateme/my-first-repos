using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.Context;



namespace Accounting.App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }



        private void dgCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        void BindGrid()
        {
            using (UnitofWork db = new UnitofWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.CustomerRepository.GetAllCustomers();

            }
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            BindGrid();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitofWork db = new UnitofWork())
            {
                dgvCustomers.DataSource = db.CustomerRepository.GetCustomersByFilter(txtFilter.Text);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                using (UnitofWork db = new UnitofWork())
                {
                    string name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($" آیا از حذف {name} مطمین هستید؟ ", "توجه",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                        db.CustomerRepository.DeleteCustomer(customerId);
                        db.Save();
                        BindGrid();
                    }
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید");
            }

        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddorEditCustomer frmAdd = new frmAddorEditCustomer();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }


        }

        private void btnEditCuston_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            { 
             int customerId=int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                frmAddorEditCustomer frmAddorEdit = new frmAddorEditCustomer();
                frmAddorEdit.customerId= customerId;
                if (frmAddorEdit.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
                
            }
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
