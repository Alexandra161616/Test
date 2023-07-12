using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace incerc
{
    
    public partial class Form1 : Form
    {

        private const string apikey1 = "f8ce74e00caf759759d311aad1e005a0";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var login = new LoginCredentials
            {
                UserName = "team@freyapos.com",
                Password = "16819215"

            };

            if (textBox1.Text == login.UserName && textBox2.Text == login.Password)
            {

                var apiKey = new ApiKey();

                HttpClient client = new HttpClient();
                HttpRequestMessage request;
                HttpResponseMessage response;

                string responseBody;

                //am creat obiectul clasei http
                client = new HttpClient();
                request = new HttpRequestMessage(HttpMethod.Post, "https://api-testing-dogu.freya.cloud/login");
                request.Headers.Authorization = new AuthenticationHeaderValue("apikey", "f8ce74e00caf759759d311aad1e005a0");

                var stringData = JsonConvert.SerializeObject(login);
                //de atasat apiKey ul la api
                var stringContent = new StringContent(stringData, Encoding.UTF8, "application/json");




                request.Content = stringContent;
                client.DefaultRequestHeaders.Add("apikey", "f8ce74e00caf759759d311aad1e005a0");
                response = await client.SendAsync(request);
                responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response body
                    Root responseObject = JsonConvert.DeserializeObject<Root>(responseBody);

                    // Access the deserialized data
                    if (responseObject != null && responseObject.isSuccess)
                    {
                        Payload payload = responseObject.payload;
                        // Access the payload data as needed
                        string token = payload.token;
                        textBox1.Text = token;
                        CookieContainer cookieContainer = new CookieContainer();
                        Uri uri = new Uri("https://api-testing-dogu.freya.cloud/login"); 
                        cookieContainer.Add(uri, new Cookie("token", token));



                        Form2 form2 = new Form2(token);
                        form2.Show();




                    }
                    else
                    {
                        // Handle error response
                        string errorMessage = responseObject?.errorMessage;
                        // Handle error message
                    }
                }
                else
                {
                    // Handle non-success status code
                    string errorMessage = response.ReasonPhrase;
                    // Handle error message
                }

               
            }
            else
            {
                MessageBox.Show("Ai introdus gresit datele");
            }



        }
    }


    public class LoginCredentials
    {
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }

    public class ApiKey
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string apiKey { get; set; }
        public int orderFlag { get; set; }
        public bool hasCustomModules { get; set; }
        public List<Module> modules { get; set; }
    }

    public class Module
    {
        public int key { get; set; }
        public string name { get; set; }
    }

    public class Payload
    {
        public string token { get; set; }
        public string userFullName { get; set; }
        public int expiresIn { get; set; }
        public string imageUid { get; set; }
        public string dateTimeFormat { get; set; }
        public string userLanguageUid { get; set; }
        public string userUid { get; set; }
        public string username { get; set; }
        public double timeZoneOffsetMinutes { get; set; }
        public string rerouteToUrl { get; set; }
        public bool isUsingTwoFactorAuth { get; set; }
        public DateTime expirationDate { get; set; }
        public List<ApiKey> apiKeys { get; set; }
        public List<string> claims { get; set; }
    }

    public class Root
    {
        public int status { get; set; }
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        public Payload payload { get; set; }
    }

}