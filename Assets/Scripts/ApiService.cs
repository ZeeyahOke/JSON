using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ApiService : MonoBehaviour
{
    public string apiUrl = "https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa";
    public Button refreshButton;

    public event Action<ApiResponse> OnDataLoaded;
    public event Action<string> OnLoadFailed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (refreshButton != null)
            refreshButton.onClick.AddListener(LoadData);

        LoadData();
    }

    public void LoadData()
    {
        Debug.Log("Loading player data from API...");
        StartCoroutine(GetPlayerData());
    }

    IEnumerator GetPlayerData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Request failed: " + request.error);
            OnLoadFailed?.Invoke(request.error);
            yield break;
        }

        string json = request.downloadHandler.text;
        json = json.Replace("\"private\"", "\"isPrivate\"");
        ApiResponse response = JsonUtility.FromJson<ApiResponse>(json);

        if (response == null || response.record == null)
        {
            Debug.LogError("Data came back empty or malformed.");
            OnLoadFailed?.Invoke("Data came back empty or in the wrong format.");
            yield break;
        }

        Debug.Log("Data loaded for player: " + response.record.playerName);
        OnDataLoaded?.Invoke(response);
    }
}
