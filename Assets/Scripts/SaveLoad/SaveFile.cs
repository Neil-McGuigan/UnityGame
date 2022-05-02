using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveFile
{
    //Save and load system reference from youtube tutorial
    //(Brackeys - SAVE & LOAD SYSTEM in Unity) - https://www.youtube.com/watch?v=XOjd_qU2Ido
    public static void savePlayer(HealthManager playerHP, KeyItemHolder playerItems, PlayerController checkpoint)
    {
        BinaryFormatter format = new BinaryFormatter();
        string saveLocation = Application.persistentDataPath + "/player.test"; //Sets save directory
        FileStream saveStream = new FileStream(saveLocation, FileMode.Create);

        PlayerData data = new PlayerData(playerHP, playerItems, checkpoint);

        format.Serialize(saveStream, data); //Encrypts data and stores it in the designated save file directory
        saveStream.Close(); //Closes datastream
    }

    public static PlayerData loadPlayer()
    {
        string saveLocation = Application.persistentDataPath + "/player.test";

        //Check if file exists
        if(File.Exists(saveLocation))
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream loadStream = new FileStream(saveLocation, FileMode.Open);

            PlayerData data = format.Deserialize(loadStream) as PlayerData;
            
            loadStream.Close(); //Closes datastream
            return data;
        }

        else
        {
            Debug.LogError("File not found at: " + saveLocation);
            return null;
        }
    }
}
