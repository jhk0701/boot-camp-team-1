using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_TextRPG
{
    public class DataDefinition
    {
        public class Item
        {
            public string Name { get; set; }
            public float Spec { get; set; }

            public Item(string n, float spec)
            {
                Name = n;
                Spec = spec;
            }
        }

        
        static DataDefinition _instance;

        public Item[] Items { get; set; }

        private DataDefinition()
        {
            Items = new Item[] {
                new Item("Item 1", 10f),
                new Item("Item 2", 10f),
                new Item("Item 3", 10f),
                new Item("Item 4", 10f),
                new Item("Item 5", 10f),
            };
        }

        public static DataDefinition GetInstance()
        {
            if(_instance == null)
                _instance = new DataDefinition();

            return _instance;
        }

    }
}
