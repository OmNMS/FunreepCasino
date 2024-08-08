using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LobbyScripts;

public class DataStorageController : MonoBehaviour
{
    public static DataStorageController Instance;

    void Awake()
    {
        Instance = this;
    }

    private LoginFormRoot LoginFormRoot;

    public DataStorageController(Transform screenContainer)
    {
    }


    public string Email
    {
        get
        {
            return ReadData(Constants.EMAIL_DATA_KEY);
        }
    }

    public string Password
    {
        get
        {
            return ReadData(Constants.PASSWORD_DATA_KEY);
        }
    }

    public LoginFormRoot LoginResponseData
    {
        get
        {
            return LoginFormRoot;
        }
        set
        {
            LoginFormRoot = value;
        }
    }

    public void SaveCredentials(string email, string password)
    {
        if (!string.IsNullOrWhiteSpace(email))
            SaveData(email, Constants.EMAIL_DATA_KEY);

        if (!string.IsNullOrWhiteSpace(password))
            SaveData(password, Constants.PASSWORD_DATA_KEY);
    }

    public void DeleteCredentials()
    {
        DeleteData(Constants.EMAIL_DATA_KEY);
        DeleteData(Constants.PASSWORD_DATA_KEY);
    }

    public void SaveData(string obj, string fileName)
    {
        PlayerPrefs.SetString(fileName, obj);
        PlayerPrefs.Save();
    }
    public void SaveLoadingScreen(int screen)
    {
        PlayerPrefs.SetInt("screen", screen);
        PlayerPrefs.Save();
    }
    public int GetFirstScreenData()
    {
        if (PlayerPrefs.HasKey("screen"))
            return PlayerPrefs.GetInt("screen");
        return -1;
    }
    public string ReadData(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
            return PlayerPrefs.GetString(keyName);
        return null;
    }

    public void DeleteData(string keyName)
    {
        PlayerPrefs.DeleteKey(keyName);
    }
}
