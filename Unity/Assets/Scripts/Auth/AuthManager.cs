using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;

public class AuthManager : MonoBehaviour
{
    private const string SERVER_URL = " http://localhost:4000";
    private const string ACCESS_TOKEN_PREFS_KEY = "AccessToekn";
    private const string REFRESH_TOKEN_PREFS_KEY = "RefreshToekn";
    private const string TOKEN_EXPIRY_PREFS_KEY = "TokenEXpiry";


    private string accessToken;
    private string refreshToken;
    private DateTime tokenExpiryTime;

    void Start()
    {
        LoadTokenFromPrefs();
    }

    private void LoadTokenFromPrefs()
    {
        accessToken = PlayerPrefs.GetString(ACCESS_TOKEN_PREFS_KEY, "");
        refreshToken = PlayerPrefs.GetString(REFRESH_TOKEN_PREFS_KEY, "");
        long expiryTicks = Convert.ToInt64(PlayerPrefs.GetString(TOKEN_EXPIRY_PREFS_KEY, "0"));
        tokenExpiryTime = new DateTime(expiryTicks);
    }


    private void SaveTokenToPrefs(string accessToken, string refreshToken, DateTime expiryTime)
    {
        PlayerPrefs.SetString(ACCESS_TOKEN_PREFS_KEY , accessToken);
        PlayerPrefs.SetString(REFRESH_TOKEN_PREFS_KEY, refreshToken);
        PlayerPrefs.SetString(TOKEN_EXPIRY_PREFS_KEY, expiryTime.Ticks.ToString());

        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
        this.tokenExpiryTime = expiryTime;
    }

    public IEnumerator Register(string username, string password)
    {
        var user = new { username, password };
        var jsonData = JsonConvert.SerializeObject(user);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm($"{SERVER_URL}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Registration Error: {www.error}");
            }
            else
            {
                Debug.Log("Registration successful");
            }
        }
    }

         public IEnumerator Login(string username, string password)
    {
        var user = new { username, password };
        var jsonData = JsonConvert.SerializeObject(user);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm($"{SERVER_URL}/login", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Registration Error: {www.error}");
            }
            else
            {
                var respone = JsonConvert.DeserializeObject<LoginResponse>(www.downloadHandler.text);
                Debug.Log(respone);
                SaveTokenToPrefs(respone.accessToekn, respone.refreshToken, DateTime.UtcNow.AddMinutes(15));
                Debug.Log("Login successful");
            }
        }
    }
}



[System.Serializable]

public class LoginResponse
{
    public string accessToekn;
    public string refreshToken;
}

[System.Serializable]

public class RefreshTokenResponse
{
    public string accessToekn;
}