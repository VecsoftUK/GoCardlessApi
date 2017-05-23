using System;
using System.Globalization;
using GoCardlessTest;

namespace Vecsoft.GoCardlessApi
	{
	public class Payment
		{
		/// <summary>
		/// Unique identifier, beginning with “PM”.
		/// </summary>
		public String Id { get; set; }

		/// <summary>
		/// Amount in pence (GBP), cents (EUR), or öre (SEK).
		/// </summary>
		public Decimal Amount { get; set; }

		/// <summary>
		/// Amount refunded in pence/cents/öre.
		/// </summary>
		public Decimal AmountRefunded { get; set; }

		/// <summary>
		/// A future date on which the payment should be collected. If not specified, the payment will be collected as soon as possible. This must be on or after the mandate’s next_possible_charge_date, and will be rolled-forwards by GoCardless if it is not a working day.
		/// </summary>
		public DateTime ChargeDate { get; set; }

		/// <summary>
		/// Fixed timestamp, recording when this resource was created.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// ISO 4217 currency code. Currently only “GBP”, “EUR”, and “SEK” are supported.
		/// </summary>
		public String Currency { get; set; }

		/// <summary>
		/// A human-readable description of the payment. This will be included in the notification email GoCardless sends to your customer if your organisation does not send its own notifications (see compliance requirements).
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// An optional payment reference that will appear on your customer’s bank statement. For Bacs payments this can be up to 10 characters, for SEPA payments the limit is 140 characters, and for Autogiro payments the limit is 11 characters.
		/// </summary>
		public String Reference { get; set; }

		public PaymentStatus Status { get; set; }
		
		internal static Payment FromJson(JsonObject data)
			{
			return new Payment
				{
				Id = (String)data["id"],
				Amount = Convert.ToDecimal(data["amount"]) / 100,
				AmountRefunded = Convert.ToDecimal(data["amount_refunded"]),
				ChargeDate = DateTime.ParseExact((String)data["charge_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
				CreatedAt = DateTime.ParseExact((String)data["created_at"], "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None),
				Currency = (String)data["currency"],
				Description = (String)data["description"],
				Reference = (String)data["reference"],
				Status = ParseStatus((String)data["status"])
				};
			}

		internal JsonObject ToJson()
			{
			return new JsonObject
				{
				{ "amount", (Amount * 100).ToString("#0") },
				{ "charge_date", ChargeDate.ToString("yyyy-MM-dd") },
				{ "currency", Currency },
				{ "description", Description },
				};
			}

		private static PaymentStatus ParseStatus(String value)
			{
			switch (value)
				{
				case "pending_customer_approval":
					return PaymentStatus.PendingCustomerApproval;

				case "pending_submission":
					return PaymentStatus.PendingSubmission;

				case "submitted":
					return PaymentStatus.Submitted;

				case "confirmed":
					return PaymentStatus.Confirmed;

				case "paid_out":
					return PaymentStatus.PaidOut;

				case "cancelled":
					return PaymentStatus.Cancelled;

				case "customer_approval_denied":
					return PaymentStatus.CustomerApprovalDenied;

				case "failed":
					return PaymentStatus.Failed;
				
				case "charged_back":
					return PaymentStatus.ChargedBack;

				default:
					throw new ArgumentOutOfRangeException(nameof(value), $"Payment status value '{value}' could not be parsed");
				}
			}
		}
	}
