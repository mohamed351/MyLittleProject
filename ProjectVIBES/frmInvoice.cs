﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectVIBES
{
    public partial class frmInvoice : Form
    {
        public frmInvoice()
        {
            InitializeComponent();
        }
        VibesDataSourceEntities entities = new VibesDataSourceEntities();
        private void frmInvoice_Load(object sender, EventArgs e)
        {
            Select();
            MyHelper myHelper = new MyHelper();
            myHelper.WriteNumbersOnly(groupBox1);

        }

        private void Select()
        {
            comboConference.DataSource = entities.Tb_Conferences.ToList();
            comboConference.DisplayMember = "Name";
            comboConference.ValueMember = "ID";
            comboCustomer.DataSource = entities.Tb_suppliers.ToList();

            comboCustomer.DataSource = entities.Tb_suppliers.ToList();
            comboCustomer.DisplayMember = "Name";
            comboCustomer.ValueMember = "ID";
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.DataSource = entities.Doctors.ToList();
            column.DisplayMember = "Name";
            column.ValueMember = "ID";
            column.HeaderText = "Doctor";

            DataGridViewComboBoxColumn column2 = new DataGridViewComboBoxColumn();
            column2.DataSource = entities.Tb_Hotels.ToList();
            column2.DisplayMember = "Name";
            column2.ValueMember = "ID";
            column2.HeaderText = "Hotel";

            dataGridView1.Columns.Add("Type", "Type");
            dataGridView1.Columns.Add(column);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add("Quntaity", "Quntaity");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Total", "Total");

          int  i =   entities.Invoices.Max(a => a.ID);
            txtID.Text = (i + 1).ToString();

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
           
            int ?number = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            double ?number2 = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            if(number == null && number2 ==null)
            {
                MessageBox.Show("you must Qtu and Price");
            }
            else
            {
                double result = Convert.ToDouble(number * number2);
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = result;
            }
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            MyHelper helper = new MyHelper();
            txttotal.Text = helper.calcutotal(dataGridView1).ToString();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            MyHelper helper = new MyHelper();
          List<InvoiceDetail> details=   helper.ConvertToList(this.dataGridView1);
            if(details.Count  <=0)
            {
                MessageBox.Show("please add the detils of the invoice", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Invoice invoice = new Invoice();
            invoice.ConferencesID = Convert.ToInt32(comboConference.SelectedValue);
            invoice.CustomerID = Convert.ToInt32(comboCustomer.SelectedValue);
            invoice.Name = txtname.Text;
            invoice.Note = txtnote.Text;
            invoice.InvoiceDetails = details;
            invoice.DateOfInvoice = dateTimePicker1.Value;
            invoice.total = Convert.ToDecimal(txtDebit.Text);
            invoice.TaxCusto = Convert.ToDecimal(txtcustomerTax.Text);
            invoice.vatcustom = Convert.ToDecimal(txtvatcustomer.Text);
            invoice.ISPaid = false;
            invoice.ISCancel = false;
            invoice.ISprinted = false;

            entities.Invoices.Add(invoice);
            
          if(entities.SaveChanges() >0)
            { 

                MessageBox.Show("The invoice has been added","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                int i = entities.Invoices.Max(a => a.ID);
                txtID.Text = (i + 1).ToString();
            }
          else
            {
                MessageBox.Show("Test");
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
           
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            this.dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count -1);
        }
    }
}
