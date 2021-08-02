using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace GhostHunterCrop_SaveEditor
{
    public struct SaveData
    {
        public Player Player;
        public List<Item> Items;
    }
    
    public static class SaveManager
    {
        private static readonly string EncryptKey = "5qm5fSPW67w";
        
        public static SaveData? LoadSaveFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            try
            {
                string fileData = File.ReadAllText(filePath);
                string[] encryptedSave = fileData.Split('.');

                string encryptedSaveData = encryptedSave[0].Substring(25, encryptedSave[0].Length - 25);
                string encryptedSaveIv = encryptedSave[1].Substring(12, encryptedSave[1].Length - 12);

                Dictionary<string, Dictionary<string, string>> rawSaveData =
                    JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(SaveEncryptor.Decrypted(encryptedSaveData, encryptedSaveIv, SaveManager.EncryptKey));

                return LoadSave(rawSaveData);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static SaveData LoadSave(Dictionary<string, Dictionary<string, string>> rawSaveData)
        {
            Player player = new Player();
            player.Name = rawSaveData["player"]["pseudo"];
            player.Exp = UInt32.Parse(rawSaveData["player"]["xp"]);
            player.Money = UInt32.Parse(rawSaveData["player"]["money"]);
            player.Color = rawSaveData["player"]["scolor"];
            player.Skin = int.Parse(rawSaveData["player"]["skin"]);

            List<Item> items = new List<Item>();

            foreach (KeyValuePair<string, string> item in rawSaveData["inv"])
            {
                if (Item.ValidItems.Contains(item.Key))
                {
                    items.Add(new Item(
                        item.Key,
                        UInt32.Parse(item.Value)
                    ));
                }
            }

            return new SaveData()
            {
                Player = player,
                Items = items
            };
        }

        public static bool SaveFile(string saveFilePath, Player player, List<Item> items)
        {
            try
            {
                Dictionary<string, string> playerData = new Dictionary<string, string>();
                Dictionary<string, string> inventoryData = new Dictionary<string, string>();

                playerData.Add("pseudo", player.Name);
                playerData.Add("skin", player.Skin.ToString());
                playerData.Add("scolor", player.Color);
                playerData.Add("money", player.Money.ToString());
                playerData.Add("xp", player.Exp.ToString());

                foreach (Item item in items)
                {
                    if (item.Amount > 0)
                    {
                        inventoryData.Add(item.Name, item.Amount.ToString());
                    }
                }

                Dictionary<string, Dictionary<string, string>> rawSaveData = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "player", playerData
                },
                {
                    "inv", inventoryData
                }
            };
                string rawJson = JsonConvert.SerializeObject(rawSaveData);

                //System.Diagnostics.Debug.WriteLine(rawJson);

                EncryptedText encryptedText = SaveEncryptor.Encrypted(rawJson, SaveManager.EncryptKey);

                string rawSaveBytes = "JUSTUSEL355_25_CH4R4CTER5" + encryptedText.Text + "." + "ANOTHER_12_C" + encryptedText.Iv;

                File.WriteAllText(saveFilePath, rawSaveBytes);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}