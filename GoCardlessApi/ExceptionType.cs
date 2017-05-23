namespace Vecsoft.GoCardlessApi
	{
	public enum ExceptionType
		{
		/// <summary>
		/// An internal error occurred while processing your request. This should be reported to our support team with the id, so we can resolve the issue.
		/// </summary>
		GoCardless,

		/// <summary>
		/// This is an error with the request you made. It could be an invalid URL, the authentication header could be missing, invalid, or grant insufficient permissions, you may have reached your rate limit, or the syntax of your request could be incorrect. The errors will give more detail of the specific issue.
		/// </summary>
		InvalidApiUsage,

		/// <summary>
		/// The action you are trying to perform is invalid due to the state of the resource you are requesting it on. For example, a payment you are trying to cancel might already have been submitted. The errors will give more details.
		/// </summary>
		InvalidState,

		/// <summary>
		/// The parameters submitted with your request were invalid. Details of which fields were invalid and why are included in the response. The request_pointer parameter indicates the exact field of the request that triggered the validation error.
		/// </summary>
		ValidationFailed,

		/// <summary>
		/// This error type is not understood by this C# API wrapper.
		/// </summary>
		Unrecognised
		}
	}
