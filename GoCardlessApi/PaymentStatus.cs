namespace Vecsoft.GoCardlessApi
	{
	public enum PaymentStatus
		{
		/// <summary>
		/// We’re waiting for the customer to approve this payment.
		/// </summary>
		PendingCustomerApproval,

		/// <summary>
		/// The payment has been created, but not yet submitted to the banks.
		/// </summary>
		PendingSubmission,

		/// <summary>
		/// The payment has been submitted to the banks.
		/// </summary>
		Submitted,

		/// <summary>
		/// The payment has been confirmed as collected.
		/// </summary>
		Confirmed,

		/// <summary>
		/// The payment has been included in a payout.
		/// </summary>
		PaidOut,

		/// <summary>
		/// The payment has been cancelled.
		/// </summary>
		Cancelled,

		/// <summary>
		/// The customer has denied approval for the payment. You should contact the customer directly.
		/// </summary>
		CustomerApprovalDenied,

		/// <summary>
		/// The payment failed to be processed. Note that payments can fail after being confirmed if the failure message is sent late by the banks.
		/// </summary>
		Failed,

		/// <summary>
		/// The payment has been charged back.
		/// </summary>
		ChargedBack
		}
	}
