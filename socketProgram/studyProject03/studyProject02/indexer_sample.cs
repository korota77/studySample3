using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studyProject02
{
    class indexer_sample
    {
        Item head;

        public indexer_sample()
        {
            this.head = new Item(null, null, null);
        }

        public string this[string key]
        {
            set
            {
                for (Item item = this.head.next; item != null; item = item.next)
                    if (item.key == key)
                    {
                        item.value = value;
                        return;
                    }
                this.head.next = new Item(key, value, this.head.next);
            }
            get
            {
                for (Item item = this.head.next; item != null; item = item.next)
                    if (item.key == key)
                        return item.value;
                return null;
            }
        }
    }

    internal class Item
    {
        public string key;
        public string value;
        public Item next;

        public Item(string key, string value, Item next)
        {
            this.key = key;
            this.value = value;
            this.next = next;
        }
    }
}
