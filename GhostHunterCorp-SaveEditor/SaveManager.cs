using System;
using System.Collections.Generic;
using System.IO;

namespace GhostHunterCorp_SaveEditor
{
    public struct SaveData
    {
        public Player Player;
        public List<Item> Items; // DEPRECATED
    }

    public static class SaveManager
    {
        private static readonly string EncryptKey = "q588zdoifse@ef555q6qd8q";

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

                char[] rawSaveData = SaveEncryptor.Decrypted(encryptedSaveData, encryptedSaveIv, SaveManager.EncryptKey).ToCharArray();
                Array.Reverse(rawSaveData);

                return LoadSave(new string(rawSaveData));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static SaveData LoadSave(string rawSaveData)
        {
            Player player = new Player();
            player.Parse(rawSaveData);

            return new SaveData()
            {
                Player = player,
            };
        }

        public static bool SaveFile(string saveFilePath, Player player)
        {
            try
            {
                string rawSaveString = player.ToString();

                char[] rawSaveBytesArray = rawSaveString.ToCharArray();
                Array.Reverse(rawSaveBytesArray);
                rawSaveString = new string(rawSaveBytesArray);

                EncryptedText encryptedText = SaveEncryptor.Encrypted(rawSaveString, SaveManager.EncryptKey);

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