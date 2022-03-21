
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



/*  Here's the story so far: I'm making a save system that only accepts basic data structures, like int, floats,
 * bools and strings. I don't know if it accepts arrays yet and I need to research that.
 *  Update: turns out it can pass entire objects. Eureka! I can simply use the GameData class and pass the object
 * as a whole.
 *  Update: OK. Now All I have to do is continue the tutorial on Brackeys. I suspect I need to do some sort of 
 * update on the scriptable objects, maybe adding methods to receive the data.
 */
public static class SaveSystem
{
    static readonly string path = Application.persistentDataPath + "/Progress.sav";
    public static void SaveProgress(GameDataSO gameData, PlayerSO playerData, int spawnPointIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameData, playerData);
        data.spawnPointIndex = spawnPointIndex;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadProgress()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        Debug.LogWarning("Save file not found in " + path);
        return null;
    }

    public static void DeleteProgress()
    {
        File.Delete(path);
    }
}
