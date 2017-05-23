﻿using System;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;
using Eto.Serialization.Xaml;
using Vecsoft.GoCardlessApi;
using System.Collections.Generic;

namespace GoCardlessTest
	{
	internal class MainForm : Form
		{
		private GridView _CustomerGrid;
		private GridView _PaymentsGrid;

		public MainForm()
			{
			ClientSize = new Size(800, 600);
			Title = "GoCardless API Test";

			_CustomerGrid = new GridView { };
			_CustomerGrid.Columns.Add(new GridColumn { HeaderText = "ID", DataCell = new TextBoxCell(nameof(Customer.Id)) });
			_CustomerGrid.Columns.Add(new GridColumn { HeaderText = "Name", DataCell = new TextBoxCell(nameof(Customer.FullName)) });
			_CustomerGrid.Columns.Add(new GridColumn { HeaderText = "Company", DataCell = new TextBoxCell(nameof(Customer.CompanyName)) });
			_CustomerGrid.SelectedItemsChanged += _CustomerGrid_SelectedItemsChanged;

			_PaymentsGrid = new GridView { };
			_PaymentsGrid.Columns.Add(new GridColumn { HeaderText = "ID", DataCell = new TextBoxCell(nameof(Payment.Id)) });
			_PaymentsGrid.Columns.Add(new GridColumn { HeaderText = "Amount", DataCell = new TextBoxCell(nameof(Payment.Amount)) });
			_PaymentsGrid.Columns.Add(new GridColumn { HeaderText = "ChargeDate", DataCell = new TextBoxCell(nameof(Payment.ChargeDate)) });
			_PaymentsGrid.Columns.Add(new GridColumn { HeaderText = "Description", DataCell = new TextBoxCell(nameof(Payment.Description)) });
			_PaymentsGrid.Columns.Add(new GridColumn { HeaderText = "Status", DataCell = new TextBoxCell(nameof(Payment.Status)) });

			var PaymentsToolbar = new ToolBar();
			PaymentsToolbar.Items.Add(new ButtonToolItem(InvokeCreatePayment) { Text = "Create Payment" });

			ToolBar = PaymentsToolbar;

			Content = new Splitter
				{
				Panel1 = _CustomerGrid,
				Panel2 = _PaymentsGrid,
				FixedPanel = SplitterFixedPanel.None,
				Orientation = Orientation.Vertical,
				Position = 200
				};
			}

		private void InvokeCreatePayment(Object sender, EventArgs e)
			{
			using (var paymentDialog = new PaymentDialog())
				{
				var Payment = paymentDialog.ShowModal();

				if (Payment == null)
					return;

				Program.Context.CreatePayment(Payment);
				}
			}

		private async void _CustomerGrid_SelectedItemsChanged(Object sender, EventArgs e)
			{
			var Row = _CustomerGrid.SelectedItem as Customer;

			_PaymentsGrid.DataStore = null;

			if (Row == null)
				return;

			var Payments = await Program.Context.GetPaymentsAsync(Row);

			_PaymentsGrid.DataStore = Payments;
			}

		protected override async void OnLoad(EventArgs e)
			{
			base.OnLoad(e);

			var Customers = await Program.Context.GetCustomersAsync();

			_CustomerGrid.DataStore = Customers;
			}
		}
	}