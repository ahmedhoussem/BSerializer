using BSerializer.Core.Custom;
using NUnit.Framework;
using System.Collections.Generic;

namespace BSerializer.UnitTest.Model
{
    [TestFixture]
    public class BasicSerializationTests
    {
        public BSerializer<Person> Serializer { get; set; }
        public BSerializer<IPerson> InterfaceSerializer { get; set; }
        public BSerializer<List<IPerson>> ListSerializer { get; set; }
        public BSerializer<Dictionary<int, Person>> DictionarySerializer { get; set; }

        [SetUp]
        public void Setup()
        {
            Serializer = new BSerializer<Person>();
            InterfaceSerializer = new BSerializer<IPerson>();
            ListSerializer = new BSerializer<List<IPerson>>();
            DictionarySerializer = new BSerializer<Dictionary<int, Person>>();
        }

        [Test(Description = "Check Recursion Serialization")]
        public void RecursionTest()
        {
            Person parent = new Person() { age = 32, Address = "Some other place", FirstName = "Parent", LastName = "McParenton", Id = 69 };
            parent.Parent = parent;

            string text = Serializer.Serialize(parent);

            Person obj = Serializer.Deserialize(text);

            Assert.AreSame(obj, obj.Parent);
        }

        [Test(Description = "Check Dictionary Serialization")]
        public void DictionaryTest()
        {
            Person p = new Person()
            {
                Id = 69,
                age = 32,
                Address = "Some other place",
                FirstName = "Parent",
                LastName = "McParenton"
            };

            Dictionary<int, Person> dict = new Dictionary<int, Person>()
            {
                { 420 , p },
                { 88 , p }
            };

            string text = DictionarySerializer.Serialize(dict);

            Dictionary<int, Person> obj = DictionarySerializer.Deserialize(text);

            string textTest = DictionarySerializer.Serialize(obj);

            Assert.NotNull(obj);
            Assert.AreEqual(text, textTest);
            Assert.AreSame(obj[420], obj[88]);
        }

        [Test(Description = "Check Interface Serialization")]
        public void InterfaceTest()
        {
            Person p = new Person()
            {
                Id = 69,
                age = 32,
                Address = "Some other place",
                FirstName = "Parent",
                LastName = "McParenton"
            };

            string text = InterfaceSerializer.Serialize(p);

            IPerson inter = InterfaceSerializer.Deserialize(text);

            Assert.NotNull(inter);
            Assert.IsInstanceOf<Person>(inter);
        }

        [Test(Description = "Check List Serialization")]
        public void ListTest()
        {
            Person p = new Person()
            {
                Id = 69,
                age = 32,
                Address = "Some other place",
                FirstName = "Parent",
                LastName = "McParenton"
            };

            List<IPerson> people = new List<IPerson>() { p , p , p , p , p };

            string text = ListSerializer.Serialize(people);

            List<IPerson> inter = ListSerializer.Deserialize(text);

            string textBack = ListSerializer.Serialize(inter);

            Assert.AreEqual(text, textBack);

            Assert.NotNull(inter);

            Assert.NotNull(inter[0]);

            Assert.IsInstanceOf<Person>(inter[0]);
        }
    }
}
