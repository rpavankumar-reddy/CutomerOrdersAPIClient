using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPIClient
{
    public class APIClient
    {
        string url = ConfigurationManager.AppSettings["ApiUrl"];
        public List<OrderItems> GetAllProductAsync()
        {
            List<OrderItems> bask = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            
                HttpResponseMessage response = client.GetAsync("/api/CustomerOrders/GetAllOrders").Result;
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var product = response.Content.ReadAsStringAsync().Result;
                    bask = JsonConvert.DeserializeObject<List<OrderItems>>(product.Trim());
                }
            }
            return bask;
        }

        public OrderItems CreateProductAsync(OrderItems product)
        {
            OrderItems bask = null;
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(url);
                    response = client.PostAsJsonAsync("api/CustomerOrders/PostCustomerOrder", product).Result;
                    response.EnsureSuccessStatusCode();
                    var res = response.Content.ReadAsStringAsync().Result;
                    bask = JsonConvert.DeserializeObject<OrderItems>(res.Trim());
                }
                catch (Exception ex)
                {

                }
            }
            return bask;
        }

        public OrderItems UpdateProductAsync(OrderItems orders)
        {
            OrderItems bask = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.PutAsJsonAsync("api/CustomerOrders/PutUpdateCustomerOrder", orders).Result;
                response.EnsureSuccessStatusCode();
                bask = response.Content.ReadAsAsync<OrderItems>().Result;
            }
            return bask;
        }
        public OrderItems PostAddCustomerOrdersItems(int orderId, string itemName, int quantity)
        {
            OrderItems basket = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);              
                var response = client.GetAsync(string.Format("api/CustomerOrders/PostCustomerOrderItems?orderId={0}&itemName={1}&quantity={2}", orderId, itemName, quantity)).Result;
                response.EnsureSuccessStatusCode();
                basket = response.Content.ReadAsAsync<OrderItems>().Result;
            }
            return basket;
        }

        public OrderItems UpdateProductItemsAsync(int orderId, string itemName, int quantity)
        {
            OrderItems basket = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);               
                var response = client.GetAsync(string.Format("api/CustomerOrders/PutCustomerOrderItem?orderId={0}&itemName={1}&quantity={2}", orderId, itemName, quantity)).Result;
                response.EnsureSuccessStatusCode();
                basket = response.Content.ReadAsAsync<OrderItems>().Result;
            }
            return basket;
        }

        public OrderItems RemoveProductItemsAsync(int orderId, string itemName, int quantity)
        {
            OrderItems basket = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);                
                var response = client.DeleteAsync(string.Format("api/CustomerOrders/DeleteCustomerOrderItems?orderId={0}&itemName={1}&quantity={2}", orderId, itemName, quantity)).Result;
                response.EnsureSuccessStatusCode();
                basket = response.Content.ReadAsAsync<OrderItems>().Result;
            }
            return basket;
        }

        public OrderItems GetProductAsync(int orderId)
        {
            OrderItems bask = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));              
                var response = client.GetAsync(string.Format("/api/GetCustomerOrder?orderId={0}", orderId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var product = response.Content.ReadAsStringAsync().Result;
                    bask = JsonConvert.DeserializeObject<OrderItems>(product.Trim());
                }
            }
            return bask;
        }
        public HttpStatusCode DeleteCustomerOrdersList(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                System.Net.Http.HttpResponseMessage response = client.DeleteAsync(string.Format("api/CustomerOrders/DeleteCustomerOrdersList/orderId={0}", id)).Result;
                return response.StatusCode;
            }
        }

        public HttpStatusCode DeleteCustomerOrders(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                System.Net.Http.HttpResponseMessage response = client.DeleteAsync(string.Format("api/CustomerOrders/DeleteCustomerOrder?orderId={0}", id)).Result;
                return response.StatusCode;
            }
        }
    }
}
