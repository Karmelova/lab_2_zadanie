using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            Seller treacher = new Seller("Jan Kowalski", 50);

            Buyer buyer1 = new Buyer("Jaś Fasola 1", 25);
            Buyer buyer2 = new Buyer("Jaś Fasola 2", 21);
            Buyer buyer3 = new Buyer("Jaś Fasola 3", 23);

            buyer1.AddProduct(new Fruit("Apple", 6));
            buyer1.AddProduct(new Meat("Fish", 0.5));

            Person[] persons = { treacher, buyer1, buyer2, buyer3 };

            Product[] products = {
                new Fruit("Apple", 1000),
                new Fruit("Banana", 700),
                new Fruit("Orange", 500),
                new Meat("Fish", 100.0),
                new Meat("Beef", 75.0)
            };

            Shop shop = new Shop("Super Market", persons, products);

            shop.Print();
        }
    }

    public interface IThing
    {
        string Name
        {
            get;
        }
    }

    public class Product : IThing
    {
        private string name;

        public string Name
        {
            get => name;
        }

        public Product(string name)
        {
            this.name = name;
        }

        public virtual void Print(string prefix = "\t")
        {
            Console.Write($"{prefix} {this.name}");
        }
    }

    public class Meat : Product
    {
        private double weight;

        public double Weight
        {
            get => weight;
        }

        public Meat(string name, double weight) : base(name)
        {
            this.weight = weight;
        }

        public override void Print(string prefix = "\t")
        {
            base.Print();
            Console.WriteLine($"({this.weight} kg)");
        }
    }

    public class Fruit : Product
    {
        private int count;

        public int Count
        {
            get => count;
        }

        public Fruit(string name, int count) : base(name)
        {
            this.count = count;
        }

        public override void Print(string prefix = "\t")
        {
            string name = this.count == 1 ? "fruit" : "fruits";

            base.Print();
            Console.WriteLine($"({this.count} {name})");
        }
    }

    public class Person : IThing
    {
        private string name;
        private int age;

        public string Name
        {
            get => name;
        }

        public int Age
        {
            get => age;
        }

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public virtual void Print(string prefix = "\t")
        {
            Console.WriteLine($"{this.name} ({this.age} y.o.) {prefix}");
        }
    }

    public class Buyer : Person
    {
        protected List<Product> products = new();

        public Buyer(string name, int age) : base(name, age)
        {
            this.products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                return;
            products.Add(product);
        }

        public void RemoveProduct(int index)
        {
            if (index > this.products.Count - 1 || index < 0)
                return;
            products.RemoveAt(index);
        }

        public override void Print(string prefix = "\t")
        {
            Console.Write($"{prefix} Buyer: ");
            base.Print();

            if (products.Count > 0)
            {
                Console.WriteLine($"{prefix}{prefix}-- Products: --");

                foreach (Product product in products)
                {
                    Console.Write(prefix);
                    product.Print();
                }
            }
        }
    }

    public class Seller : Person
    {
        public Seller(string name, int age) : base(name, age)
        {
        }

        public override void Print(string prefix = "\t")
        {
            Console.Write($"{prefix} Seller: ");
            base.Print();
        }
    }

    public class Shop : IThing
    {
        private string name;
        private Person[] people;
        private Product[] products;

        public string Name
        {
            get => name;
        }

        public Shop(string name, Person[] people, Product[] products)
        {
            this.products = products;
            this.people = people;
            this.name = name;
        }

        public void Print()
        {
            Console.WriteLine($"Shop: {this.name}");

            Console.WriteLine("-- People: --");

            foreach (var people in people)
            {
                people.Print("\t");
            }

            Console.WriteLine("-- Products: --");

            foreach (var product in products)
            {
                product.Print();
            }
        }
    }
}