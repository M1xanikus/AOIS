using hashtable;
using Menu;
using System.Diagnostics.CodeAnalysis;
namespace HashTests
{
    [TestClass]
    public class HashTests
    {
        private HashTable ht;

        [TestInitialize]
        public void TestInitialize() 
        {
            ht = new();
            
        }
        [TestMethod]
        public void TestAddGet()
        {
           
            ht.Add("Территория", "географическая область");

            ht.Display();

            ht.Add("Рельеф", "физическое облика Земли");

            Assert.AreEqual(ht.Get("Климат"), null);

            ht.Add("Климат", "совокупность погодных условий");

            ht.Add("Почва", "верхний слой земной коры");

            ht.Add("Водные ресурсы", "виды воды, доступные для использования");

            Assert.AreEqual(ht.Get("Климат"), "совокупность погодных условий");
        }
        [TestMethod]
        public void TestRemove()
        { 
            ht.Remove("Рельеф");

            Assert.AreEqual(ht.Get("Рельеф"), null);
            ht.Add("Рельеф", "физическое облика Земли2");
            ht.Add("Рельеф", "физическое облика Земли1");
            ht.Remove("Рельеф");
            Assert.AreEqual(ht.Get("Рельеф"), "физическое облика Земли1");
        }
    }
}