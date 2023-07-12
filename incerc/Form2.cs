using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace incerc
{
    public partial class Form2 : Form
    {
        private readonly string token;
        public Form2(string token)
        {
            InitializeComponent();
            this.token = token;
            //creezi conexiunea la baza de date
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //creezi conexiunea la baza de date
            string connectionString = "Server=localhost;Database=Alexandra123;User Id=;Password=;Integrated Security=true;TrustServerCertificate=True";

            var request = new HttpRequestMessage(HttpMethod.Get, "https://api-testing-dogu.freya.cloud/product/findMany?listOnly=true&productCategoryUid=5b693a50ff2c495290a741c5ecdf26d1&locationUid=cc33a3a158d14b34a80171caf35870e2&pageNo=0&top=100");
            var client = new HttpClient();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("api_key", "f8ce74e00caf759759d311aad1e005a0");

            HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            List<Product> products;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
                MessageBox.Show(responseContent);
            }
            else
            {
                // Handle error response
                string errorMessage = response.ReasonPhrase;
                // Handle error message
            }
        }
    }
}
