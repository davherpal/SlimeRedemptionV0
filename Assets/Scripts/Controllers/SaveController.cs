using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    private string savePath;


    void Start()
    {
        savePath = Application.persistentDataPath + "/gamesave.save";
    }

    public void SaveData(int score, float height, int enemies)
    {
        var save = new Save()
        {
            savedEnemies = enemies, 
            savedHeight = height,
            savedScore = score
        };

        var binaryFormatter = new BinaryFormatter();
        using (var fileStream = File.Create(savePath))
        {

            binaryFormatter.Serialize(fileStream, save);

        }

    }

    public int LoadDataScore()
    {
        if (File.Exists(savePath))
        {
            Save save;

            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = File.Open(savePath, FileMode.Open))
            {

                save = (Save)binaryFormatter.Deserialize(fileStream);

            }

            return save.savedScore;
        }
        else
        {
            return 0;
        }
    }

    public float LoadDataHeight()
    {
        if (File.Exists(savePath))
        {
            Save save;

            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = File.Open(savePath, FileMode.Open))
            {

                save = (Save)binaryFormatter.Deserialize(fileStream);

            }

            return save.savedHeight;
        }
        else
        {
            return 0;
        }
    }

    public int LoadDataEnemies()
    {
        if (File.Exists(savePath))
        {
            Save save;

            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = File.Open(savePath, FileMode.Open))
            {

                save = (Save)binaryFormatter.Deserialize(fileStream);

            }

            return save.savedEnemies;
        }
        else
        {
            return 0;
        }
    }
}

    
