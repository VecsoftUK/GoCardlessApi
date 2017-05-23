using Eto.Drawing;
using Eto.Forms;
using System;
using Vecsoft.GoCardlessApi;

namespace GoCardlessTest
	{
	internal class PaymentDialog : Dialog<Payment>
		{
		private TextBox _DescriptionTextBox;
		private TextBox _AmountTextBox;
		private DateTimePicker _ChargeDatePicker;

		public PaymentDialog(Customer customer, Mandate mandate)
			{
			var FieldsTable = new TableLayout
				(
				new TableRow
					(
					new Label { Text = "Customer" },
					new Label { Text = customer.CompanyName ?? customer.FullName }
					),
				new TableRow
					(
					new Label { Text = "Mandate" },
					new Label { Text = mandate.Reference }
					),
				new TableRow
					(
					new Label { Text = "Description" },
					_DescriptionTextBox = new TextBox()
					),
				new TableRow
					(
					new Label { Text = "Amount" },
					_AmountTextBox = new TextBox()
					),
				new TableRow
					(
					new Label { Text = "Charge Date" },
					_ChargeDatePicker = new DateTimePicker { Mode = DateTimePickerMode.Date }
					),
				null
				)
				{
				Spacing = new Size(2, 2),
				};

			var ButtonsTable = TableLayout.Horizontal
				(
				null,
				new Button { Text = "OK", Command = new Command(HandleOkButtonClick) },
				new Button { Text = "Cancel", Command = new Command(HandleCancelButtonClick) }
				);

			Content = new TableLayout
				(
				new TableRow(FieldsTable),
				new TableRow(ButtonsTable)
				)
				{
				Spacing = new Size(2, 2)
				};

			Padding = new Padding(4);

			// TODO: Need to find a way to hide the checkbox. Setting the value at least ticks it by default.
			_ChargeDatePicker.Value = mandate.NextPossibleChargeDate;
			_ChargeDatePicker.MinDate = mandate.NextPossibleChargeDate ?? DateTime.Now.Date;
			}

		private void HandleOkButtonClick(Object sender, EventArgs e)
			{
			Result = new Payment
				{
				Amount = Decimal.Parse(_AmountTextBox.Text),
				Currency = "GBP",
				Description = _DescriptionTextBox.Text,
				ChargeDate = _ChargeDatePicker.Value.Value
				};

			Close();
			}

		private void HandleCancelButtonClick(Object sender, EventArgs e)
			{
			Result = null;

			Close();
			}
		}
	}