using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace GhostHunterCorp_SaveEditor
{
    public class Player
    {
        public string Exp { get; set; }
        public string Money { get; set; }
        public string Name { get; set; }
        public string SelectedCharacter { get; set; }
        public string Color { get; set; }
        public string Unknown608 { get; set; }
        public string ContactTaken { get; set; }
        public string TotalPlayedOrExorcised { get; set; }
        public string Unknown614 { get; set; }
        public string Unknown615 { get; set; }
        public string TotalEarned { get; set; }
        public string Fainted610 { get; set; }
        public string Revived611 { get; set; }
        public string Injured612 { get; set; }
        public string TutorialVisited { get; set; }
        public string TutorialPerfected { get; set; }
        public string IntSomething702 { get; set; }
        public string IntSomething703 { get; set; }
        public string Unknown616 { get; set; }
        public string Unknown617 { get; set; }
        public string Unknown618 { get; set; }
        public string OwnedLocation { get; set; }
        public string OwnedItems { get; set; }
        public string Unknown620 { get; set; }
        public string GhostTypeChild { get; set; }
        public string GhostTypeClown { get; set; }
        public string GhostTypeDemon { get; set; }
        public string GhostTypeFallen { get; set; }
        public string GhostTypeMemory { get; set; }
        public string GhostTypePoltergeist { get; set; }
        public string GhostTypeRevenant { get; set; }
        public string GhostTypeShadow { get; set; }
        public string ListSomething900 { get; set; }
        public string LastPlayedTime { get; set; }

        public int GetLevel()
        {
            // DEPRECATED
            return 0;
        }

        public Dictionary<DataType, string> Parse(string input)
        {
            var result = new Dictionary<DataType, string>();

            int dataSectionLength = int.Parse(input.Substring(0, 4));
            string dataSection = input.Substring(4, dataSectionLength);
            string headerSection = input.Substring(4 + dataSectionLength);

            var headerPairs = headerSection.Split('.').Where(s => !string.IsNullOrEmpty(s));
            int currentIndex = 0;
            StringBuilder buffer = new StringBuilder();

            while (currentIndex < dataSection.Length)
            {
                if (dataSection[currentIndex] != '|')
                {
                    buffer.Append(dataSection[currentIndex]);
                    currentIndex++;
                }
                else
                {
                    int dataLength = int.Parse(buffer.ToString());
                    string dataValue = dataSection.Substring(currentIndex + 2, dataLength);
                    result.Add((DataType)Enum.Parse(typeof(DataType), headerPairs.First()), dataValue);
                    headerPairs = headerPairs.Skip(1);
                    currentIndex += dataLength + 2;
                    buffer.Clear();
                }
            }

            foreach (var item in result)
            {
                switch (item.Key)
                {
                    case DataType.EXP:
                        Exp = item.Value;
                        break;
                    case DataType.MONEY:
                        Money = item.Value;
                        break;
                    case DataType.NAME:
                        Name = item.Value;
                        break;
                    case DataType.SELECTED_CHARACTER:
                        SelectedCharacter = item.Value;
                        break;
                    case DataType.COLOR:
                        Color = item.Value;
                        break;
                    case DataType.UNKNOWN_608:
                        Unknown608 = item.Value;
                        break;
                    case DataType.CONTACT_TAKEN:
                        ContactTaken = item.Value;
                        break;
                    case DataType.TOTAL_PLAYED_OR_EXORCISED:
                        TotalPlayedOrExorcised = item.Value;
                        break;
                    case DataType.UNKNOWN_614:
                        Unknown614 = item.Value;
                        break;
                    case DataType.UNKNOWN_615:
                        Unknown615 = item.Value;
                        break;
                    case DataType.TOTAL_EARNED:
                        TotalEarned = item.Value;
                        break;
                    case DataType.FAINTED_610:
                        Fainted610 = item.Value;
                        break;
                    case DataType.REVOVED_611:
                        Revived611 = item.Value;
                        break;
                    case DataType.INJURED_612:
                        Injured612 = item.Value;
                        break;
                    case DataType.TUTORIAL_VISITED:
                        TutorialVisited = item.Value;
                        break;
                    case DataType.TUTORIAL_PERFECTED:
                        TutorialPerfected = item.Value;
                        break;
                    case DataType.INT_702:
                        IntSomething702 = item.Value;
                        break;
                    case DataType.INT_703:
                        IntSomething703 = item.Value;
                        break;
                    case DataType.UNKNOWN_616:
                        Unknown616 = item.Value;
                        break;
                    case DataType.UNKNOWN_617:
                        Unknown617 = item.Value;
                        break;
                    case DataType.UNKNOWN_618:
                        Unknown618 = item.Value;
                        break;
                    case DataType.OWNED_LOCATIONS:
                        OwnedLocation = item.Value;
                        break;
                    case DataType.OWNED_ITEMS:
                        OwnedItems = item.Value;
                        break;
                    case DataType.UNKNOWN_620:
                        Unknown620 = item.Value;
                        break;
                    case DataType.GHOST_TYPE_CHILD:
                        GhostTypeChild = item.Value;
                        break;
                    case DataType.GHOST_TYPE_CLOWN:
                        GhostTypeClown = item.Value;
                        break;
                    case DataType.GHOST_TYPE_DEMON:
                        GhostTypeDemon = item.Value;
                        break;
                    case DataType.GHOST_TYPE_FALLEN:
                        GhostTypeFallen = item.Value;
                        break;
                    case DataType.GHOST_TYPE_MEMORY:
                        GhostTypeMemory = item.Value;
                        break;
                    case DataType.GHOST_TYPE_POLTERGEIST:
                        GhostTypePoltergeist = item.Value;
                        break;
                    case DataType.GHOST_TYPE_REVENANT:
                        GhostTypeRevenant = item.Value;
                        break;
                    case DataType.GHOST_TYPE_SHADOW:
                        GhostTypeShadow = item.Value;
                        break;
                    case DataType.LIST_900:
                        ListSomething900 = item.Value;
                        break;
                    case DataType.LAST_PLAYED_TIME:
                        LastPlayedTime = item.Value;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private char GetDataType(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.OWNED_ITEMS:
                case DataType.LIST_900:
                    return (char)13;
                case DataType.LAST_PLAYED_TIME:
                case DataType.NAME:
                    return (char)4;
                case DataType.TUTORIAL_VISITED:
                case DataType.TUTORIAL_PERFECTED:
                    return (char)8;
                case DataType.COLOR:
                    return (char)9;
                case DataType.EXP:
                    return (char)10;
                case DataType.OWNED_LOCATIONS:
                    return (char)12;
                default:
                    return (char)2;
            }
        }

        override
        public string ToString()
        {
            var dataPairs = new Dictionary<DataType, string>();
            // THIS DUMB CHUNK OF CODE GENERATED BY COPILOT :)
            dataPairs.Add(DataType.EXP, Exp);
            dataPairs.Add(DataType.MONEY, Money);
            dataPairs.Add(DataType.NAME, Name);
            dataPairs.Add(DataType.SELECTED_CHARACTER, SelectedCharacter);
            dataPairs.Add(DataType.COLOR, Color);
            dataPairs.Add(DataType.UNKNOWN_608, Unknown608);
            dataPairs.Add(DataType.CONTACT_TAKEN, ContactTaken);
            dataPairs.Add(DataType.TOTAL_PLAYED_OR_EXORCISED, TotalPlayedOrExorcised);
            dataPairs.Add(DataType.UNKNOWN_614, Unknown614);
            dataPairs.Add(DataType.UNKNOWN_615, Unknown615);
            dataPairs.Add(DataType.TOTAL_EARNED, TotalEarned);
            dataPairs.Add(DataType.FAINTED_610, Fainted610);
            dataPairs.Add(DataType.REVOVED_611, Revived611);
            dataPairs.Add(DataType.INJURED_612, Injured612);
            dataPairs.Add(DataType.TUTORIAL_VISITED, TutorialVisited);
            dataPairs.Add(DataType.TUTORIAL_PERFECTED, TutorialPerfected);
            dataPairs.Add(DataType.INT_702, IntSomething702);
            dataPairs.Add(DataType.INT_703, IntSomething703);
            dataPairs.Add(DataType.UNKNOWN_616, Unknown616);
            dataPairs.Add(DataType.UNKNOWN_617, Unknown617);
            dataPairs.Add(DataType.UNKNOWN_618, Unknown618);
            dataPairs.Add(DataType.OWNED_LOCATIONS, OwnedLocation);
            dataPairs.Add(DataType.OWNED_ITEMS, OwnedItems);
            dataPairs.Add(DataType.UNKNOWN_620, Unknown620);
            dataPairs.Add(DataType.GHOST_TYPE_CHILD, GhostTypeChild);
            dataPairs.Add(DataType.GHOST_TYPE_CLOWN, GhostTypeClown);
            dataPairs.Add(DataType.GHOST_TYPE_DEMON, GhostTypeDemon);
            dataPairs.Add(DataType.GHOST_TYPE_FALLEN, GhostTypeFallen);
            dataPairs.Add(DataType.GHOST_TYPE_MEMORY, GhostTypeMemory);
            dataPairs.Add(DataType.GHOST_TYPE_POLTERGEIST, GhostTypePoltergeist);
            dataPairs.Add(DataType.GHOST_TYPE_REVENANT, GhostTypeRevenant);
            dataPairs.Add(DataType.GHOST_TYPE_SHADOW, GhostTypeShadow);
            dataPairs.Add(DataType.LIST_900, ListSomething900);
            dataPairs.Add(DataType.LAST_PLAYED_TIME, LastPlayedTime);
            // IT'S JUST WORK, LOL

            var dataBody = new StringBuilder();
            var dataHeader = new StringBuilder();

            foreach (var item in dataPairs)
            {
                if (item.Value != null)
                {
                    dataHeader.Append((int)item.Key + ".");

                    var dataType = GetDataType(item.Key);

                    dataBody.Append(item.Value.Length + "|" + dataType + item.Value);
                }
            }

            if (dataHeader.Length > 0)
                dataHeader.Remove(dataHeader.Length - 1, 1);

            return dataBody.ToString().Length.ToString("D4") + dataBody.ToString() + dataHeader.ToString();
        }
    }

    public class Item
    {
        // TODO: REMOVE THIS LIST FOR EASIEST UPDATE, BUT I'M NOT GONNA UPDATE THIS NO MORE :(
        //  need to update when game add new gameplay items.
        public static readonly string[] ValidItems =
        {
            "TORCH", "BIG_TORCH", "TRIPOD", "PHOTO", "CAMERA", "CAMERA_TRIPOD", "MEL", "TEMP", "EMF", "FREQ", "OCCULT",
            "BOOK", "SPIRITBOX", "POLAROID", "BOOK_EXORCISM", "TIGER_EYE", "WATER", "INCENSE", "SEL", "MARIE", "BEAM",
            "CRUCIFIX", "ATTK_SCAN", "FUSIL", "FOG_LAMP", "ENV_SCANNER"
        };

        public static readonly Dictionary<string, int> ItemIdMap = new Dictionary<string, int>() {
            { "TORCH", 32 },
            { "BIG_TORCH", 12 },
            { "TRIPOD", 33 },
            { "PHOTO", 26 },
            { "CAMERA", 16 },
            { "CAMERA_TRIPOD", 17 },
            { "MEL", 24 },
            { "TEMP", 30 },
            { "EMF", 19 },
            { "FREQ", 20 },
            { "OCCULT", 25 },
            { "BOOK", 13 },
            { "SPIRITBOX", 29 },
            { "POLAROID", 27 },
            { "BOOK_EXORCISM", 14 },
            { "TIGER_EYE", 31 },
            { "WATER", 34 },
            { "INCENSE", 22 },
            { "SEL", 28 },
            { "MARIE", 23 },
            { "BEAM", 11 },
            { "CRUCIFIX", 18 },
            { "ATTK_SCAN", 10 },
            { "FUSIL", 21 },
            { "FOG_LAMP", 15 },
            { "ENV_SCANNER", 38 }
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

        public static List<Item> Parse(string input, List<Item> baseList = null)
        {
            if (baseList == null)
            {
                baseList = GetBaseList();
            }

            var items = input.Split('*');
            foreach (string item in items)
            {
                int id = int.Parse(item);
                if (id == 0)
                    continue;

                string name = ItemIdMap.FirstOrDefault(x => x.Value == id).Key;
                if (name == null)
                    continue;

                Item itemObj = baseList.FirstOrDefault(x => x.Name.Equals(name));
                if (itemObj == null)
                    continue;

                itemObj.Amount++;
            }

            return baseList;
        }

        public static string ToString(List<Item> items)
        {
            string output = "";
            foreach (Item item in items)
            {
                for (int i = 0; i < item.Amount; i++)
                {
                    output += ItemIdMap[item.Name] + "*";
                }
            }

            if (output.Length > 0)
                output = output.Substring(0, output.Length - 1);

            return output;
        }

        public static List<Item> UpdateList(List<Item> baselist, List<Item> newlist)
        {
            foreach (Item item in baselist) // O(N^2) not good, but who cares for worst case is just 24 times 24 ?
            {
                bool foundAny = false;
                foreach (Item newitem in newlist)
                {
                    if (item.Name.Equals(newitem.Name))
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

    public class Location
    {

        public static readonly Dictionary<string, uint> LocationsMap = new Dictionary<string, uint>()
        {
            { "USA - Lost House", 1 },
            { "France - The Panist's Manor", 2 },
            { "BELGIUM - Fort ST.SAMAEL", 3 },
            { "PRAGUE - Cemetery Of The Last Rest", 4 },
            { "Germany - Barony Gonther", 5 },
            { "Canada - \"Hope\" Scout Camp", 6 },
            { "France - \"Harmony\" Kindergarten", 7 },
            { "Japan - Bamboo Temple", 8 },
            { "USA - Family Home", 9 },
            { "Varios - Circus \"D'Hell Arte\"", 10 },
            { "USA -Abandoned Farm", 11 }
        };

        public static List<Location> GetBaseList()
        {
            List<Location> _locations = new List<Location>();
            foreach (KeyValuePair<string, uint> item in LocationsMap)
            {
                _locations.Add(new Location(item.Key));
            }

            return _locations;
        }

        public static List<Location> Parse(string input, List<Location> baseList = null)
        {
            if (baseList == null)
                baseList = GetBaseList();

            var items = input.Split('*');
            foreach (string item in items)
            {
                int id = int.Parse(item);
                if (id == 0)
                    continue;

                string name = LocationsMap.FirstOrDefault(x => x.Value == id).Key;
                if (name == null)
                    continue;

                Location location = baseList.FirstOrDefault(x => x.Name.Equals(name));
                if (location == null)
                    continue;

                location.IsOwned = true;
            }

            return baseList;
        }

        public static string ToString(List<Location> locations)
        {
            string output = "";
            foreach (Location location in locations)
            {
                if (location.IsOwned)
                    output += LocationsMap[location.Name] + "*";
            }

            if (output.Length > 0)
                output = output.Substring(0, output.Length - 1);

            return output;
        }

        public Location(string name)
        {
            this.Name = name;
            this.IsOwned = false;
        }

        public string Name { get; }
        public bool IsOwned { get; set; }
    }

    public enum DataType
    {
        EXP = 603,
        MONEY = 604,
        NAME = 600,
        SELECTED_CHARACTER = 602,
        COLOR = 601,
        UNKNOWN_608 = 608,
        CONTACT_TAKEN = 607,
        TOTAL_PLAYED_OR_EXORCISED = 613,
        UNKNOWN_614 = 614,
        UNKNOWN_615 = 615,
        TOTAL_EARNED = 609,
        FAINTED_610 = 610,
        REVOVED_611 = 611,
        INJURED_612 = 612,
        TUTORIAL_VISITED = 700,
        TUTORIAL_PERFECTED = 701,
        INT_702 = 702,
        INT_703 = 703,
        UNKNOWN_616 = 616,
        UNKNOWN_617 = 617,
        UNKNOWN_618 = 618,
        OWNED_LOCATIONS = 606,
        OWNED_ITEMS = 605,
        UNKNOWN_620 = 620,
        GHOST_TYPE_CHILD = 800,
        GHOST_TYPE_CLOWN = 801,
        GHOST_TYPE_DEMON = 802,
        GHOST_TYPE_FALLEN = 803,
        GHOST_TYPE_MEMORY = 804,
        GHOST_TYPE_POLTERGEIST = 805,
        GHOST_TYPE_REVENANT = 806,
        GHOST_TYPE_SHADOW = 807,
        LIST_900 = 900,
        LAST_PLAYED_TIME = 704
    }
}
