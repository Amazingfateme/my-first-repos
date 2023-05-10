using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer;
using System.IO;

namespace Accounting.App
{
    public partial class frmAddorEditCustomer : Form
    {

        public int customerId=0; 
        UnitofWork db=new UnitofWork(); 
        public frmAddorEditCustomer()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string ImageName = Guid.NewGuid().ToString() +
                    Path.GetExtension(pcCustomer.ImageLocation);

                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                                   
                }
                pcCustomer.Image.Save(path + ImageName);

                Customers customer = new Customers()
                {
                    Address = txtAddress.Text,
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    CustomreImage = ImageName
                };
                if (customerId == 0)
                {
                    db.CustomerRepository.InsertCustomer(customer);
                }
                else
                {
                    customer.CustomerID = customerId;
                    db.CustomerRepository.UpdateCustomer(customer);
                }

                db.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            { 
             pcCustomer.ImageLocation=openFile.FileName;
            }
        }

        private void frmAddorEditCustomer_Load(object sender, EventArgs e)
        {
            if (customerId != 0)
            {
                this.Text = "ویرابش شخص";
                btnSave.Text = "ویرایش";
                var customer = db.CustomerRepository.GetCustomerByID(customerId);
                txtEmail.Text=customer.Email;
                txtAddress.Text = customer.Address;
                txtMobile.Text= customer.Mobile;
                txtName.Text = customer.FullName;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomreImage;            
            }



        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
