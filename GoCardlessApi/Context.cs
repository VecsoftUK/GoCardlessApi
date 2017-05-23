using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using GoCardlessTest;
using System.Threading.Tasks;

namespace Vecsoft.GoCardlessApi
	{
	public class Context
		{
		private readonly String ApiKey;

		private const String LiveUri = "https://api.gocardless.com/";
		private const String SandboxUri = "https://api-sandbox.gocardless.com/";
		private const String Version = "2015-07-06";

		public String BaseUri => LiveUri;

		public Context(String apiKey)
			{
			ApiKey = apiKey;
			}

		#region GetCustomers{...}()

		public async Task<IEnumerable<Customer>> GetCustomersAsync()
			{
			return GetCustomers();
			}

		public IEnumerable<Customer> GetCustomers()
			{
			var Request = CreateRequest("GET", "customers");
			var ResponseData = ExecuteRequest(Request);

			var CustomersArray = (JsonArray)ResponseData["customers"];

			return CustomersArray.Cast<JsonObject>().Select(Customer.FromJson);
			}

		#endregion

		#region GetMandates{...}()

		public async Task<IEnumerable<Mandate>> GetMandatesAsync(Customer customer)
			{
			return GetMandates(customer);
			}

		public IEnumerable<Mandate> GetMandates(Customer customer)
			{
			var Arguments = new Dictionary<String, String>
				{
				{ "customer", customer.Id }
				};

			var Request = CreateRequest("GET", "mandates", Arguments);
			var ResponseData = ExecuteRequest(Request);

			var MandatesArray = (JsonArray)ResponseData["mandates"];

			return MandatesArray.Cast<JsonObject>().Select(Mandate.FromJson);
			}

		#endregion

		#region GetPayments{...}()

		public async Task<IEnumerable<Payment>> GetPaymentsAsync(Customer customer)
			{
			return GetPayments(customer);
			}

		public IEnumerable<Payment> GetPayments(Customer customer)
			{
			var Arguments = new Dictionary<String, String>
				{
				{ "customer", customer.Id }
				};

			var Request = CreateRequest("GET", "payments", Arguments);
			var ResponseData = ExecuteRequest(Request);

			var PaymentsArray = (JsonArray)ResponseData["payments"];

			return PaymentsArray.Cast<JsonObject>().Select(Payment.FromJson);
			}

		public void CreatePayment(Payment payment)
			{
			throw new NotImplementedException();
			}

		#endregion

		#region CreateRequest(), EndRequest()

		private HttpWebRequest CreateRequest(String method, String endpoint, IDictionary<String, String> arguments = null)
			{
			var Builder = new UriBuilder(new Uri(new Uri(BaseUri), endpoint));

			if(arguments != null)
				Builder.Query = String.Join("&", arguments.Select(kvp => kvp.Key + "=" + kvp.Value));

			var Request = WebRequest.CreateHttp(Builder.Uri);

			Request.Method = method;
			Request.Headers.Add("Authorization", "Bearer " + ApiKey);
			Request.Headers.Add("GoCardless-Version", Version);

			return Request;
			}

		private IDictionary<String, Object> ExecuteRequest(HttpWebRequest request)
			{
			var Response = request.GetResponse() as HttpWebResponse;

			var ResponseBodyStream = Response.GetResponseStream();

			String ResponseBody;

			using (var reader = new StreamReader(ResponseBodyStream))
				ResponseBody = reader.ReadToEnd();

			var ResponseData = (IDictionary<String, Object>)SimpleJson.DeserializeObject(ResponseBody);

			return ResponseData;
			}

		#endregion
		}
	}
