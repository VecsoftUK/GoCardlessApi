using System;
using System.Globalization;
using GoCardlessTest;

namespace Vecsoft.GoCardlessApi
	{
	public class Mandate
		{
		/// <summary>
		/// Unique identifier, beginning with “MD”.
		/// </summary>
		public String Id { get; set; }

		/// <summary>
		/// Fixed timestamp, recording when this resource was created.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The earliest date a newly created payment for this mandate could be charged.
		/// </summary>
		public DateTime? NextPossibleChargeDate { get; set; }

		/// <summary>
		/// Boolean value showing whether payments and subscriptions under this mandate require approval via an automated email before being processed.
		/// </summary>
		public Boolean PaymentsRequireApproval { get; set; }

		/// <summary>
		/// Unique reference. Different schemes have different length and character set requirements. GoCardless will generate a unique reference satisfying the different scheme requirements if this field is left blank.
		/// </summary>
		public String Reference { get; set; }

		/// <summary>
		/// Direct Debit scheme to which this mandate and associated payments are submitted. Can be supplied or automatically detected from the customer’s bank account. Currently only “autogiro”, “bacs”, and “sepa_core” are supported.
		/// </summary>
		public String Scheme { get; set; }

		public MandateStatus Status { get; set; }
		
		internal static Mandate FromJson(JsonObject data)
			{
			return new Mandate
				{
				Id = (String)data["id"],
				CreatedAt = ParseUtilities.ParseTimestamp(data["created_at"]),
				NextPossibleChargeDate = ParseUtilities.ParseNullableDate(data["next_possible_charge_date"]),
				PaymentsRequireApproval = (Boolean)data["payments_require_approval"],
				Reference = (String)data["reference"],
				Scheme = (String)data["scheme"],
				Status = ParseStatus((String)data["status"])
				};
			}

		private static MandateStatus ParseStatus(String value)
			{
			switch(value)
				{
				case "pending_customer_approval":
					return MandateStatus.PendingCustomerApproval;

				case "pending_submission":
					return MandateStatus.PendingSubmission;

				case "submitted":
					return MandateStatus.Submitted;

				case "active":
					return MandateStatus.Active;

				case "failed":
					return MandateStatus.Failed;

				case "cancelled":
					return MandateStatus.Cancelled;

				case "expired":
					return MandateStatus.Expired;

				default:
					throw new ArgumentOutOfRangeException(nameof(value), $"Mandate status value '{value}' could not be parsed");
				}
			}
		}
	}
