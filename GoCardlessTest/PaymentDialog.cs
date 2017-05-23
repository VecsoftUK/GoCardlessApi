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

		public PaymentDialog()
			{
			var FieldsTable = new TableLayout
				(
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
					_ChargeDatePicker = new DateTimePicker()
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