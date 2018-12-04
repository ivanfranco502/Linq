using System;
using System.Collections.Generic;
using System.Linq;

namespace EjemploLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            // ArrayWithLinq();
            // GetOddNumbers();
            // CountEvenNumbers();
            // LinqWithGenericType();
            // LinqWithoutGenericType();
            // //Ordering
            // //orderby cust.Name ascending

            // //Grouping
            // LinqWithGrouping();
            // // //More than 2 customers
            // LinqGroupWithCondition();

            // //Join
            // JoinTwoDataSources();

            // LinqTransformData();

            // PerformOperationsWithLinq();

            LinqMethodBasedQuery();
        }

        #region methods
        private static void LinqMethodBasedQuery()
        {
            int[] numbers = { 5, 10, 8, 3, 6, 12 };

            //Query syntax:
            IEnumerable<int> numQuery1 =
                from num in numbers
                where num % 2 == 0
                orderby num
                select num;

            //Method syntax:
            IEnumerable<int> numQuery2 = numbers.Where(num => num % 2 == 0).OrderBy(n => n);

            foreach (int i in numQuery1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(System.Environment.NewLine);
            foreach (int i in numQuery2)
            {
                Console.Write(i + " ");
            }

            // Keep the console open in debug mode.
            Console.WriteLine(System.Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        private static void PerformOperationsWithLinq()
        {
            double[] radii = { 1, 2, 3 };

            // Query.
            IEnumerable<string> query =
                from rad in radii
                select String.Format("Area = {0}", (rad * rad) * 3.14);

            // Query execution. 
            foreach (string s in query)
                Console.WriteLine(s);

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        private static void LinqTransformData()
        {
            // Create the first data source.
            List<Student> students = new List<Student>()
        {
            new Student { First="Svetlana",
                Last="Omelchenko",
                ID=111,
                Street="123 Main Street",
                City="Seattle",
                Scores= new List<int> { 97, 92, 81, 60 } },
            new Student { First="Claire",
                Last="O’Donnell",
                ID=112,
                Street="124 Main Street",
                City="Redmond",
                Scores= new List<int> { 75, 84, 91, 39 } },
            new Student { First="Sven",
                Last="Mortensen",
                ID=113,
                Street="125 Main Street",
                City="Lake City",
                Scores= new List<int> { 88, 94, 65, 91 } },
        };

            // Create the second data source.
            List<Teacher> teachers = new List<Teacher>()
        {
            new Teacher { First="Ann", Last="Beebe", ID=945, City="Seattle" },
            new Teacher { First="Alex", Last="Robinson", ID=956, City="Redmond" },
            new Teacher { First="Michiyo", Last="Sato", ID=972, City="Tacoma" }
        };

            // Create the query.
            var peopleInSeattle = (from student in students
                                   where student.City == "Seattle"
                                   select student.Last)
                        .Concat(from teacher in teachers
                                where teacher.City == "Seattle"
                                select teacher.Last);

            Console.WriteLine("The following students and teachers live in Seattle:");
            // Execute the query.
            foreach (var person in peopleInSeattle)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();



            /* Output:
           The following students and teachers live in Seattle:
           Omelchenko
           Beebe
        */
        }
        private static void JoinTwoDataSources()
        {
            IList<Customer> customers = new List<Customer>{
                new Customer{FirstName="Pedro", LastName="Alvarado", City="Buenos Aires"},
                new Customer{FirstName="John", LastName="Snow", City="London"},
                new Customer{FirstName="Francois", LastName="Pillé", City="Paris"},
                new Customer{FirstName="Alfonso", LastName="Pereyra", City="Buenos Aires"},
                new Customer{FirstName="Albert", LastName="Dillon", City="London"},
            };

            IList<Distributor> distributors = new List<Distributor>{
                new Distributor{Name="Distribuidora Bs As", City="Buenos Aires"},
                new Distributor{Name="England distribution", City="London"},
                new Distributor{Name="Distributé", City="Paris"},
            };

            var innerJoinQuery =
                from cust in customers
                join dist in distributors on cust.City equals dist.City
                select new { CustomerName = cust.FirstName, DistributorName = dist.Name };

            foreach (var query in innerJoinQuery){
                Console.WriteLine(query.CustomerName +"-"+ query.DistributorName );
            }
        }
        private static void LinqGroupWithCondition()
        {
            IList<Customer> customers = new List<Customer>{
                new Customer{FirstName="Pedro", LastName="Alvarado", City="Buenos Aires"},
                new Customer{FirstName="John", LastName="Snow", City="London"},
                new Customer{FirstName="Francois", LastName="Pillé", City="Paris"},
                new Customer{FirstName="Alfonso", LastName="Pereyra", City="Buenos Aires"},
                new Customer{FirstName="Albert", LastName="Dillon", City="London"},
            };

            var custQuery =
                from cust in customers
                group cust by cust.City into custGroup
                where custGroup.Count() > 1
                orderby custGroup.Key
                select custGroup;
        }
        private static void LinqWithGrouping()
        {
            IList<Customer> customers = new List<Customer>{
                new Customer{FirstName="Pedro", LastName="Alvarado", City="Buenos Aires"},
                new Customer{FirstName="John", LastName="Snow", City="London"},
                new Customer{FirstName="Francois", LastName="Pillé", City="Paris"},
                new Customer{FirstName="Alfonso", LastName="Pereyra", City="Buenos Aires"},
                new Customer{FirstName="Albert", LastName="Dillon", City="London"},
            };

            var queryCustomersByCity =
                from cust in customers
                group cust by cust.City;

            // customerGroup is an IGrouping<string, Customer>
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (Customer customer in customerGroup)
                {
                    Console.WriteLine("    {0}", customer.FirstName);
                }
            }
        }
        private static void LinqWithoutGenericType()
        {
            IList<Customer> customers = new List<Customer>{
                new Customer{FirstName="Pedro", LastName="Alvarado", City="Buenos Aires"},
                new Customer{FirstName="John", LastName="Snow", City="London"}
            };

            var customerQuery2 =
                from cust in customers
                where cust.City == "London"
                select cust;

            foreach (var customer in customerQuery2)
            {
                Console.WriteLine(customer.LastName + ", " + customer.FirstName);
            }
        }
        private static void LinqWithGenericType()
        {

            IList<Customer> customers = new List<Customer>{
                new Customer{FirstName="Pedro", LastName="Alvarado", City="Buenos Aires"},
                new Customer{FirstName="John", LastName="Snow", City="London"}
            };

            IEnumerable<Customer> customerQuery =
                from cust in customers
                where cust.City == "London"
                select cust;

            foreach (Customer customer in customerQuery)
            {
                Console.WriteLine(customer.LastName + ", " + customer.FirstName);
            }
        }
        private static void CountEvenNumbers()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var evenNumQuery =
                from num in numbers
                where (num % 2) != 0
                select num;

            int evenNumCount = evenNumQuery.Count();
        }
        private static void GetOddNumbers()
        {
            // The Three Parts of a LINQ Query:
            //  1. Data source.
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            // 3. Query execution.
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        private static void ArrayWithLinq()
        {
            // Specify the data source.
            List<int> scores = new List<int> { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (int i in scoreQuery)
            {
                Console.Write(i + " ");
            }
        }
        #endregion
        #region classes
        private class Customer
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string City { get; set; }
        }
        private class Distributor
        {
            public string Name { get; set; }
            public string City { get; set; }
        }
        private class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public List<int> Scores;
        }
        private class Teacher
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string City { get; set; }
        }
        #endregion
    }
}