using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GhostHunterCorp_SaveEditor
{
    public class Player
    {
        public string Name;
        public uint Money;
        public uint Exp;
        public string Color;
        public int Skin;

        private static Player _player;

        public int GetLevel()
        {
            int level = -1;
            int currentExp = (int) this.Exp;
            int expPerLevel = 50;
            do
            {
                level++;
                currentExp -= expPerLevel;
                expPerLevel = (int)System.Math.Ceiling((double)expPerLevel * 1.1);
            }
            while (currentExp > expPerLevel);

            return level;
        }

        //public static Player GetPlayer()
        //{
        //    return _player ??= new Player();
        //}
    }

    public class Item
    {
        // private static List<Item> _items;

        // public static List<Item>  GetItems()
        // {
        //     return _items;
        // }

        // public static void AddItem(Item item)
        // {
        //     GetItems().Add(item);
        // }

        public static readonly string[] ValidItems = // need to update when game add new gameplay items.
        {
            "TORCH", "BIG_TORCH", "TRIPOD", "PHOTO", "CAMERA", "CAMERA_TRIPOD", "MEL", "TEMP", "EMF", "FREQ", "OCCULT",
            "BOOK", "SPIRITBOX", "POLAROID", "BOOK_EXORCISM", "TIGER_EYE", "WATER", "INCENSE", "SEL", "MARIE", "BEAM",
            "CRUCIFIX", "ATTK_SCAN", "FUSIL"
        };

        public static List<Item> GetBaseList()
        {
            List<Item> _items = new List<Item>();
            foreach (string item in ValidItems)
            {
                _items.Add(new Item(item, 0));
            }

            return _items;
        }

        public static List<Item> UpdateList(List<Item> baselist, List<Item> newlist)
        {
            foreach(Item item in baselist) // O(N^2) not good, but who cares for worst case is just 24 times 24 ?
            {
                bool foundAny = false;
                foreach(Item newitem in newlist)
                {
                    if(item.Name.Equals(newitem.Name))
                    {
                        item.Amount = newitem.Amount;
                        foundAny = true;
                        break;
                    }
                }
                if (!foundAny)
                    item.Amount = 0;
            }
            return baselist;
        }
        
        //
        
        public string Name { get; }
        public uint Amount { get; set; }

        public Item()
        {
            // Item.AddItem(this);
        }

        public Item(string name, uint amount = 0) : this()
        {
            this.Name = name;
            this.Amount = amount;
        }
    }
}
