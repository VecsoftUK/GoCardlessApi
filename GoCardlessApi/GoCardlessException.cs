using System;
using GoCardlessTest;
using System.Collections.Generic;

namespace Vecsoft.GoCardlessApi
	{
	internal class GoCardlessException : Exception
		{
		public String RequestId { get; private set; }
		public Int32 Code { get; private set; }
		public ValidationError[] Errors { get; private set; }
		public String DocumentationUrl { get; private set; }

		public GoCardlessException()
			{
			}

		public GoCardlessException(String message)
			: base(message)
			{
			}

		public GoCardlessException(String message, Exception innerException)
			: base(message)
			{
			}

		internal static Exception FromJson(JsonObject data, Exception innerException = null)
			{
			var Message = (String)data["message"];
			var Type = ParseType((String)data["type"]);

			var ErrorsArray = (JsonArray)data["errors"];

			// TODO: Parse errors

			List<ValidationError> Errors = new List<ValidationError>();

			return new GoCardlessException(Message, innerException)
				{
				RequestId = (String)data["request_id"],
				DocumentationUrl = (String)data["documentation_url"],
				Errors = Errors.ToArray()
				};
			}

		private static ExceptionType ParseType(String value)
			{
			switch(value)
				{
				case "gocardless":
					return ExceptionType.GoCardless;

				case "invalid_api_usage":
					return ExceptionType.InvalidApiUsage;

				case "invalid_state":
					return ExceptionType.InvalidState;

				case "validation_failed":
					return ExceptionType.ValidationFailed;

				default:
					return ExceptionType.Unrecognised;
				}
			}
		}
	}
