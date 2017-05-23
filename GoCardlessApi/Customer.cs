using System;
using System.Globalization;
using GoCardlessTest;

namespace Vecsoft.GoCardlessApi
	{
	public class Customer
		{
		public String Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public String Email { get; set; }
		public String GivenName { get; set; }
		public String FamilyName { get; set; }
		public String CompanyName { get; set; }
		public String AddressLine1 { get; set; }
		public String AddressLine2 { get; set; }
		public String AddressLine3 { get; set; }
		public String City { get; set; }
		public String Region { get; set; }
		public String PostalCode { get; set; }
		public String CountryCode { get; set; }
		public String Language { get; set; }

		internal static Customer FromJson(JsonObject data)
			{
			return new Customer
				{
				Id = (String)data["id"],
				CreatedAt = DateTime.ParseExact((String)data["created_at"], "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None),
				Email = (String)data["email"],
				GivenName = (String)data["given_name"],
				FamilyName = (String)data["family_name"],
				CompanyName = (String)data["company_name"],
				AddressLine1 = (String)data["address_line1"],
				AddressLine2 = (String)data["address_line2"],
				AddressLine3 = (String)data["address_line3"],
				City = (String)data["city"],
				Region = (String)data["region"],
				PostalCode = (String)data["postal_code"],
				CountryCode = (String)data["country_code"],
				Language = (String)data["language"]
				};
			}
		}
	}
