using System;
using GoCardlessTest;

namespace Vecsoft.GoCardlessApi
	{
	public class ValidationError
		{
		public String Message { get; private set; }
		public String Field { get; private set; }
		public String RequestPointer { get; private set; }

		internal static ValidationError FromJson(JsonObject data)
			{
			return new ValidationError
				{
				Message = (String)data["message"],
				Field = (String)data["field"],
				RequestPointer = (String)data["request_pointer"]
				};
			}

		public override String ToString()
			{
			return Field + ": " + Message;
			}
		}
	}