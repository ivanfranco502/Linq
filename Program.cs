using System;
using System.Collections.Generic;
using System.Linq;

namespace EjemploLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayWithLinq();
            GetOddNumbers();
            CountEvenNumbers();
            LinqWithGenericType();
            LinqWithoutGenericType();
            //Ordering
            //orderby cust.Name ascending

            //Grouping
            LinqWithGrouping();
            //More than 2 customers
            LinqGroupWithCondition();

            //Join
            JoinTwoDataSources();
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
                where custGroup.Count() > 2
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

            foreach(var customer in customerQuery2)
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
                where (num % 2) == 0
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
            int[] scores = new int[] { 97, 92, 81, 60 };

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

        private class Customer
        {
            public string LastName {get;set;}
            public string FirstName {get;set;}
            public string City {get;set;}
        }

        private class Distributor
        {
            public string Name {get;set;}
            public string City {get;set;}
        }
    }
}
