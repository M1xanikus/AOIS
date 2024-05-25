using System;
namespace hashtable
{
    public class HashTable
    {
        private const int Size = 20;
        private string[] keys;
        private string[] values;

        public HashTable()
        {
            keys = new string[Size];
            values = new string[Size];
        }

        private int Hash1(string key) => Math.Abs(key.GetHashCode()) % Size;

        private int Hash2(string key) => 1 + (Math.Abs(key.GetHashCode()) % (Size - 1));

        public void Add(string key, string value)
        {
            int hash1 = Hash1(key); // Первая хэш-функция для основного индекса
            int hash2 = Hash2(key); // Вторая хэш-функция для шага при коллизиях
            int i = 0;

            // Поиск свободного места
            while (keys[(hash1 + i * hash2) % Size] != null)
            {
                Console.WriteLine($"Collision occurred at index {(hash1 + i * hash2) % Size} for key \"{key}\"");
                i++;
            }

            // Найдено свободное место, сохраняем ключ и значение
            int index = (hash1 + i * hash2) % Size;
            keys[index] = key;
            values[index] = value;
        }


        public string Get(string key)
        {
            int hash1 = Hash1(key);
            int hash2 = Hash2(key);
            int i = 0;

            while (keys[(hash1 + i * hash2) % Size] != key)
            {
                if (keys[(hash1 + i * hash2) % Size] == null)
                    return null;
                i++;
            }

            return values[(hash1 + i * hash2) % Size];
        }

        public void Remove(string key)
        {
            int hash1 = Hash1(key);
            int hash2 = Hash2(key);
            int i = 0;

            while (keys[(hash1 + i * hash2) % Size] != key)
            {
                if (keys[(hash1 + i * hash2) % Size] == null)
                    return;
                i++;
            }

            int index = (hash1 + i * hash2) % Size;
            keys[index] = null;
            values[index] = null;

            // Rehash the table
            for (int j = 0; j < Size; j++)
            {
                if (keys[j] != null)
                {
                    string tempKey = keys[j];
                    string tempValue = values[j];
                    keys[j] = null;
                    values[j] = null;
                    Add(tempKey, tempValue);
                }
            }
        }

        public void Display()
        {
            for (int i = 0; i < Size; i++)
            {
                if (keys[i] != null)
                {
                    Console.WriteLine($"Index {i}: Key = {keys[i]}, Value = {values[i]}");
                }
            }
        }
    }
}