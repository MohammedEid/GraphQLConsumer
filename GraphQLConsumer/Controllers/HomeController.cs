using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GraphQLConsumer.Controllers
{
    public class HomeController : Controller
    {
        public async Task<JsonResult> Test()
        {
            
            #region Query Without argumnet
            
            string query = "query getAllCourses { getCourses { id name description } }";
            var heroRequest = new GraphQLRequest { Query = query, OperationName = "SchoolQuery" };

            var graphQLClient = new GraphQLClient("http://localhost:13515/api/school");
            var graphQLResponse = await graphQLClient.PostAsync(heroRequest);

            string json = JsonConvert.SerializeObject(graphQLResponse.Data);
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<Course>>>(json);
            List<Course> courses = new List<Course>();
            foreach (var obj in result.Values.ElementAt(0))
            {
                Course course = new Course();
                course.Id = obj.Id;
                course.Name = obj.Name;
                course.Description = obj.Description;
                courses.Add(course);
            }
            
            #endregion
            
            #region Query with argument
            /*
            string query = "query GetCourseById($courseId: Int) { getCourseById(myId: $courseId){ id name description } }";
            var heroRequest = new GraphQLRequest { Query = query, OperationName = "SchoolQuery", Variables = new { courseId = 3 } };
            
            var graphQLClient = new GraphQLClient("http://localhost:13515/api/school");
            var graphQLResponse = await graphQLClient.PostAsync(heroRequest);

            string json = JsonConvert.SerializeObject(graphQLResponse.Data);
            var result = JsonConvert.DeserializeObject<Dictionary<string, Course>>(json);
            List<Course> courses = new List<Course>();
            foreach(var obj in result.Values)
            {
                Course course = new Course();
                course.Id = obj.Id;
                course.Name = obj.Name;
                course.Description = obj.Description;
                courses.Add(course);
            }
            */
            #endregion

            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Test1()
        {
            string query = "query getAllCourses { getCourses { id name } }";
            var heroRequest = new GraphQLRequest { Query = query, OperationName = "SchoolQuery" };

            var graphQLClient = new GraphQLClient("http://localhost:13515/api/school");
            var graphQLResponse = await graphQLClient.PostAsync(heroRequest);

            string json = JsonConvert.SerializeObject(graphQLResponse.Data);
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<Course>>>(json);
            List<Course> courses = new List<Course>();
            foreach (var obj in result.Values.ElementAt(0))
            {
                Course course = new Course();
                course.Id = obj.Id;
                course.Name = obj.Name;
                courses.Add(course);
            }
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Orders()
        {
            GraphQLQuery graphQLQuery = GraphQLHelper.GetGraphQLQuery("GetAllOrders");
            var heroRequest = new GraphQLRequest { Query = graphQLQuery.Body, OperationName = "GetAllOrders" };

            var graphQLClient = new GraphQLClient("http://localhost:13515/api/school");
            var graphQLResponse = await graphQLClient.PostAsync(heroRequest);

            string json = JsonConvert.SerializeObject(graphQLResponse.Data);
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<Order>>>(json);
            List<Order> orders = new List<Order>();
            foreach (var obj in result.Values.ElementAt(0))
            {
                orders.Add(obj);
            }
            return View(orders);
        }

        public async Task<ActionResult> OrderDetails(int orderId)
        {
            GraphQLQuery graphQLQuery = GraphQLHelper.GetGraphQLQuery("GetOrderById");
            var graphQLRequest = new GraphQLRequest { Query = graphQLQuery.Body, OperationName = "GetOrderById", Variables = new { orderId = orderId } };

            var graphQLClient = new GraphQLClient("http://localhost:13515/api/school");
            var graphQLResponse = await graphQLClient.PostAsync(graphQLRequest);

            string json = JsonConvert.SerializeObject(graphQLResponse.Data);
            var result = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            //List<Order> orders = new List<Order>();
            //foreach (var obj in result.Values.ElementAt(0))
            //{
            //    orders.Add(obj);
            //}
            return View(result.Values.ElementAt(0));
        }

        public class Course
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public string Description { set; get; }
            //public List<Student> Students { set; get; }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}