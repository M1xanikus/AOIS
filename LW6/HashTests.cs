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
           
            ht.Add("����������", "�������������� �������");

            ht.Display();

            ht.Add("������", "���������� ������ �����");

            Assert.AreEqual(ht.Get("������"), null);

            ht.Add("������", "������������ �������� �������");

            ht.Add("�����", "������� ���� ������ ����");

            ht.Add("������ �������", "���� ����, ��������� ��� �������������");

            Assert.AreEqual(ht.Get("������"), "������������ �������� �������");
        }
        [TestMethod]
        public void TestRemove()
        { 
            ht.Remove("������");

            Assert.AreEqual(ht.Get("������"), null);
            ht.Add("������", "���������� ������ �����2");
            ht.Add("������", "���������� ������ �����1");
            ht.Remove("������");
            Assert.AreEqual(ht.Get("������"), "���������� ������ �����1");
        }
    }
}