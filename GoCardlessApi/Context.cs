using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using GoCardlessTest;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Vecsoft.GoCardlessApi
	{
	public class Context
		{
		private readonly String _ApiKey;
		private readonly ApiAccessMode _ApiMode;

		private const String LiveUri = "https://api.gocardless.com/";
		private const String SandboxUri = "https://api-sandbox.gocardless.com/";
		private const String Version = "2015-07-06";

		public String BaseUri => _ApiMode == ApiAccessMode.Live ? LiveUri : SandboxUri;
		
		public Context(ApiAccessMode apiMode, String apiKey)
			{
			_ApiMode = apiMode;
			_ApiKey = apiKey;
			}

		#region GetCustomers{...}()

		public async Task<IEnumerable<Customer>> GetCustomersAsync()
			{
			return GetCustomers();
			}

		public IEnumerable<Customer> GetCustomers()
			{
			var Request = CreateRequest(RequestMethod.Get, "customers");
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

			var Request = CreateRequest(RequestMethod.Get, "mandates", Arguments);
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

			var Request = CreateRequest(RequestMethod.Get, "payments", Arguments);
			var ResponseData = ExecuteRequest(Request);

			var PaymentsArray = (JsonArray)ResponseData["payments"];

			return PaymentsArray.Cast<JsonObject>().Select(Payment.FromJson);
			}

		public void CreatePayment(Mandate mandate, Payment payment)
			{
			var Links = new JsonObject();
			Links.Add("mandate", mandate.Id);

			var Arguments = new Dictionary<String, String>
				{
				{ "payments", payment.ToJson().ToString() },
				{ "links", Links.ToString() }
				};

			var Request = CreateRequest(RequestMethod.Post, "payments", Arguments);
			var ResponseData = ExecuteRequest(Request);

			var PaymentsArray = (JsonArray)ResponseData["payments"];

			var lol = PaymentsArray.Cast<JsonObject>().Select(Payment.FromJson);
			}

		#endregion

		#region CreateRequest(), EndRequest()

		private HttpWebRequest CreateRequest(RequestMethod method, String endpoint, IDictionary<String, String> arguments = null)
			{
			var Builder = new UriBuilder(new Uri(new Uri(BaseUri), endpoint));

			if(arguments != null && method == RequestMethod.Get)
				Builder.Query = String.Join("&", arguments.Select(kvp => kvp.Key + "=" + kvp.Value));

			var Request = WebRequest.CreateHttp(Builder.Uri);

			Request.Method = method.ToString().ToUpper();
			Request.Headers.Add("Authorization", "Bearer " + _ApiKey);
			Request.Headers.Add("GoCardless-Version", Version);
			Request.Accept = "application/json";

			if(arguments != null && method == RequestMethod.Post)
				{
				Request.ContentType = "application/json";

				using (var writer = new StreamWriter(Request.GetRequestStream()))
					{
					writer.WriteLine("{");
					Debug.WriteLine("{");

					var ArgumentIndex = 0;

					foreach (var kvp in arguments)
						{
						var WriteSeparator = (++ArgumentIndex) < arguments.Count;

						writer.WriteLine("\"" + kvp.Key + "\": " + kvp.Value + (WriteSeparator ? ", " : String.Empty));
						Debug.WriteLine("\"" + kvp.Key + "\": " + kvp.Value + (WriteSeparator ? ", " : String.Empty));
						}

					writer.WriteLine("}");
					Debug.WriteLine("}");
					}
				}

			return Request;
			}

		private IDictionary<String, Object> ExecuteRequest(HttpWebRequest request)
			{
			HttpWebResponse Response;
			Boolean IsError;

			try
				{
				Response = request.GetResponse() as HttpWebResponse;
				IsError = false;
				}

			catch (WebException x)
				{
				Response = x.Response as HttpWebResponse;
				IsError = true;
				}

			catch (Exception x)
				{
				throw;
				}

			var ResponseBodyStream = Response.GetResponseStream();

			String ResponseBody;

			using (var reader = new StreamReader(ResponseBodyStream))
				ResponseBody = reader.ReadToEnd();

			var ResponseData = (IDictionary<String, Object>)SimpleJson.DeserializeObject(ResponseBody);

			if(IsError)
				{
				var ErrorData = ResponseData["error"] as JsonObject;
				throw GoCardlessException.FromJson(ErrorData);
				}

			return ResponseData;
			}

		#endregion
		}
	}
