namespace Vecsoft.GoCardlessApi
	{
	public enum MandateStatus
		{
		/// <summary>
		/// The mandate has not yet been signed by the second customer.
		/// </summary>
		PendingCustomerApproval,

		/// <summary>
		/// The mandate has not yet been submitted to the customer’s bank.
		/// </summary>
		PendingSubmission,

		/// <summary>
		/// The mandate has been submitted to the customer’s bank but has not been processed yet
		/// </summary>
		Submitted,

		/// <summary>
		/// The mandate has been successfully set up by the customer’s bank.
		/// </summary>
		Active,

		/// <summary>
		/// The mandate could not be created.
		/// </summary>
		Failed,

		/// <summary>
		/// The mandate has been cancelled.
		/// </summary>
		Cancelled,

		/// <summary>
		/// The mandate has expired due to dormancy.
		/// </summary>
		Expired
		}
	}
