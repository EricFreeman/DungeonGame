using System.Collections.Generic;

namespace Assets.Scripts.Player
{
    public static class PlayerInventory
    {
        public static List<string> Keys;

        public static bool HasKey(string keyName)
        {
            return Keys.Contains(keyName);
        }

        public static void AddKey(string keyName)
        {
            if (!HasKey(keyName))
            {
                Keys.Add(keyName);
            }
        }

        public static void ClearInventory()
        {
            Keys.Clear();
        }
    }
}