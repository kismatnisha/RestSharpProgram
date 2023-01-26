using Newtonsoft.Json;
using RestSharp;

namespace RestSharpTestProgram
{

    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
    }
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000/Employees");
        }

        [TestMethod]
        public void OnCallingList_ReturnList()
        {
            IRestResponse response = getEmployeeList();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            Assert.AreEqual(3, dataResponse.Count);
            foreach (Employee e in dataResponse)
            {
                Console.WriteLine("id : " + e.id + " Name : " + e.name + " salary : " + e.salary);
            }
        }

        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}