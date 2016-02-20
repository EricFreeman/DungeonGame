using System.Collections.Generic;

namespace Assets.Scripts.Player
{
    public static class PlayerInventory
    {
        public static List<string> Items = new List<string>();

        public static bool HasItem(string itemName)
        {
            return Items.Contains(itemName);
        }

        public static void AddItem(string itemName)
        {
            if (!HasItem(itemName))
            {
                Items.Add(itemName);
            }
        }

        public static void ClearInventory()
        {
            Items.Clear();
        }
    }
}