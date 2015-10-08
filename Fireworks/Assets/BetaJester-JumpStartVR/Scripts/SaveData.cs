using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    static Dictionary<string, string> stringDictionary;
    static Dictionary<string, int> intDictionary;
    static Dictionary<string, float> floatDictionary;
    static Dictionary<string, bool> boolDictionary;

    static string primaryDataPath = Application.persistentDataPath + @"\Utility\Primary\";
    static string backUpDataPath = Application.persistentDataPath + @"\Utility\Backups\";        

    //Used by Options script
    //Makes loading from file happen once rather than multiple times
    public static bool loadFromFile;

    private static bool SetupCalled = false;

  
    static void Setup()
    {
        SetupCalled = true;

        loadFromFile = true;

        //Load all settings at start of scene
        //LoadContent();
        LoadGameData();
    }

    void OnDestroy()
    {
        SaveGameData();
        BackupFiles();

        Debug.Log("Destroyed Scene: Saved Game Data and Options and Backed Up Files");
    }

    public void AutoSave()
    {
        //Overload method to extend save functionality
        //Perhaps game state/current level/etc
        SaveGameData();
    }

    public static void AddToStringDictionary(string nameOfData, string dataToSave)
    {
        if (!SetupCalled)
            Setup();

        //Checks if name already exists
        //If it does, it's an update, not an addition
            if (stringDictionary.ContainsKey(nameOfData))
        {
            //Update existing dictionary entry
            stringDictionary[nameOfData] = dataToSave;

            Debug.Log("Updated " + nameOfData + " in StringDictionary");
        }
        else
        {
            //Enters new entry into dictionary
            stringDictionary.Add(nameOfData, dataToSave);

            Debug.Log("Added " + nameOfData + " in StringDictionary");
        }

        //Saves changes to file
        SaveGameData();
    }

    public static  void AddToIntDictionary(string nameOfData, int dataToSave)
    {
        if (!SetupCalled)
            Setup();


        //Checks if name already exists
        //If it does, it's an update, not an addition
        if (intDictionary.ContainsKey(nameOfData))
        {
            //Update existing dictionary entry
            intDictionary[nameOfData] = dataToSave;

            Debug.Log("Updated " + nameOfData + " in IntDictionary");
        }
        else
        {
            //Enters new entry into dictionary
            intDictionary.Add(nameOfData, dataToSave);

            Debug.Log("Added " + nameOfData + " in IntDictionary");
        }

        //Saves changes to file
        SaveGameData();
    }

    public static void AddToFloatDictionary(string nameOfData, float dataToSave)
    {
        if (!SetupCalled)
            Setup();


        //Checks if name already exists
        //If it does, it's an update, not an addition
        if (floatDictionary.ContainsKey(nameOfData))
        {
            //Update existing dictionary entry
            floatDictionary[nameOfData] = dataToSave;

            Debug.Log("Updated " + nameOfData + " in FloatDictionary");
        }
        else
        {
            //Enters new entry into dictionary
            floatDictionary.Add(nameOfData, dataToSave);

            Debug.Log("Added " + nameOfData + " in FloatDictionary");
        }

        //Saves changes to file
        SaveGameData();
    }

    public static void AddToBoolDictionary(string nameOfData, bool dataToSave)
    {
        if (!SetupCalled)
            Setup();


        //Checks if name already exists
        //If it does, it's an update, not an addition
        if (boolDictionary.ContainsKey(nameOfData))
        {
            //Update existing dictionary entry
            boolDictionary[nameOfData] = dataToSave;

            Debug.Log("Updated " + nameOfData + " in BoolDictionary");
        }
        else
        {
            //Enters new entry into dictionary
            boolDictionary.Add(nameOfData, dataToSave);

            Debug.Log("Added " + nameOfData + " in BoolDictionary");
        }

        //Saves changes to file
        SaveGameData();
    }

    public static string GetStringData(string nameOfVariable, string DefaultValue)
    {
        if (!SetupCalled)
            Setup();


        //Loads data from file to local variables
        LoadGameData();

        //Loops through all of the Dictionary
        foreach(KeyValuePair<string,string> keyValuePair in stringDictionary)
        {
            //Triggers when name of variable is found
            if (nameOfVariable == keyValuePair.Key)
            {
                //Returns value relating to the found key
                return keyValuePair.Value;
            }
        }

        //If all fails
        return DefaultValue;
    }

    public static int GetIntData(string nameOfData, int DefaultValue)
    {
        //Loads data from file to local variables
        LoadGameData();

        //Loops through all of the Dictionary
        foreach (KeyValuePair<string, int> keyValuePair in intDictionary)
        {
            //Triggers when name of variable is found
            if (nameOfData == keyValuePair.Key)
            {
                //Returns value relating to the found key
                return keyValuePair.Value;
            }
        }

        //If all fails
        return DefaultValue;
    }

    public static float GetFloatData(string nameOfData, float DefaultValue)
    {
        //Loads data from file to local variables
        LoadGameData();

        //Loops through all of the Dictionary
        foreach (KeyValuePair<string, float> keyValuePair in floatDictionary)
        {
            //Triggers when name of variable is found
            if (nameOfData == keyValuePair.Key)
            {
                //Returns value relating to the found key
                return keyValuePair.Value;
            }
        }

        //If all fails
        return DefaultValue;
    }

    public static bool GetBoolData(string nameOfData, bool DefaultValue)
    {
        //Loads data from file to local variables
        LoadGameData();

        //Loops through all of the Dictionary
        foreach (KeyValuePair<string, bool> keyValuePair in boolDictionary)
        {
            //Triggers when name of variable is found
            if (nameOfData == keyValuePair.Key)
            {
                //Returns value relating to the found key
                return keyValuePair.Value;
            }
        }

        //If all fails
        return DefaultValue;
    }

    public void BackupFiles()
    {
        string fileName;
        string destinationFile;
        string sourcePath = primaryDataPath;
        string targetPath = backUpDataPath; 

        //Checks if target path exists
        //If it doesn't create it
        if (!System.IO.Directory.Exists(targetPath))
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }

        if (System.IO.Directory.Exists(sourcePath))
        {
            string[] files = System.IO.Directory.GetFiles(sourcePath);

            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                fileName = System.IO.Path.GetFileName(s);
                destinationFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.File.Copy(s, destinationFile, true);
            }
        }
        else
        {
            Console.WriteLine("Source path does not exist!");
        }
    }
    
    public static void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();

        //Checks if directory exists, if not, it creates it
        
        if (!System.IO.Directory.Exists(primaryDataPath))
        {
            System.IO.Directory.CreateDirectory(primaryDataPath);

            Debug.Log("Created Primary Save Directory");
        }

        //Setup file name and location
        FileStream file = File.Create(primaryDataPath + "GameData.dat");

        //Copy data into local object
        GameData gameData = new GameData();
        gameData.stringDictionaryFILE = stringDictionary;
        gameData.intDictionaryFILE = intDictionary;
        gameData.floatDictionaryFILE = floatDictionary;
        gameData.boolDictionaryFILE = boolDictionary;

        Debug.Log("Saved Data");

        //Save local object to file and close stream
        bf.Serialize(file, gameData);
        file.Close();
    }

    public static void LoadGameData()
    {
        if (File.Exists(primaryDataPath + "GameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(primaryDataPath + "GameData.dat", FileMode.Open);

            //Casts the deserialized data into local object and closes stream
            GameData gameData = (GameData)bf.Deserialize(file);
            file.Close();

            //Load data from file into local variables
            stringDictionary = gameData.stringDictionaryFILE;
            intDictionary = gameData.intDictionaryFILE;
            floatDictionary = gameData.floatDictionaryFILE;
            boolDictionary = gameData.boolDictionaryFILE;

            Debug.Log("Loaded GameData");
        }
        else
        {
            //If file doesn't exist, populate local values with default values
            //Then create file
            stringDictionary = new Dictionary<string, string>();
            intDictionary = new Dictionary<string, int>();
            floatDictionary = new Dictionary<string, float>();
            boolDictionary = new Dictionary<string, bool>();

            //Creates the file
            SaveGameData();

            Debug.Log("Created new GameData file with default values");
        }
    }
}

[Serializable]
class GameData
{
    public Dictionary<string, string> stringDictionaryFILE;
    public Dictionary<string, int> intDictionaryFILE;
    public Dictionary<string, float> floatDictionaryFILE;
    public Dictionary<string, bool> boolDictionaryFILE;
}
