using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    public ApiService apiService;

    public TMP_Text playerNameText;
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text positionText;
    public TMP_Text errorText;

    public Transform inventoryContainer;
    public GameObject inventoryItemPrefab;

    public Button sortButton;

    ApiResponse currentResponse;
    bool sortedByQuantity;

    void OnEnable()
    {
        apiService.OnDataLoaded += ShowData;
        apiService.OnLoadFailed += ShowError;
    }

    void OnDisable()
    {
        apiService.OnDataLoaded -= ShowData;
        apiService.OnLoadFailed -= ShowError;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sortButton != null)
            sortButton.onClick.AddListener(SortInventory);
    }

    public void ShowData(ApiResponse response)
    {
        currentResponse = response;
        errorText.text = "";

        PlayerData player = response.record;

        playerNameText.text = player.playerName;
        levelText.text = "Level " + player.level;
        healthText.text = "Health: " + player.health;
        positionText.text = "Position: (" + player.position.x + ", " + player.position.y + ", " + player.position.z + ")";

        BuildInventory(player.inventory);
    }

    public void ShowError(string message)
    {
        errorText.text = message;
    }

    void SortInventory()
    {
        if (currentResponse == null) return;

        sortedByQuantity = !sortedByQuantity;

        InventoryItem[] items = sortedByQuantity
            ? currentResponse.record.inventory.OrderByDescending(i => i.quantity).ToArray()
            : currentResponse.record.inventory.OrderBy(i => i.itemName).ToArray();

        BuildInventory(items);
    }

    void BuildInventory(InventoryItem[] items)
    {
        foreach (Transform child in inventoryContainer)
            Destroy(child.gameObject);

        foreach (InventoryItem item in items)
        {
            GameObject row = Instantiate(inventoryItemPrefab, inventoryContainer);
            TMP_Text label = row.GetComponentInChildren<TMP_Text>();
            label.text = item.itemName + "  ×" + item.quantity + "   (" + item.weight + " kg)";
        }
    }
}
