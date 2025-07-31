using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TrailShop : MonoBehaviour
{
    [System.Serializable]
    public class TrailItem
    {
        public string name;
        public Color color;
        public int price;
        public Button button;
        public TextMeshProUGUI buttonText;

        [HideInInspector] public bool unlocked = false;
        [HideInInspector] public bool equipped = false;
    }

    [SerializeField] StudioProgress studio;

    public List<TrailItem> trailItems;
    public GameObject trailTarget;

    private TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = trailTarget.GetComponent<TrailRenderer>();

        for (int i = 0; i < trailItems.Count; i++)
        {
            int index = i; // penting agar tidak ter-overwrite di closure
            TrailItem item = trailItems[i];

            item.button.onClick.AddListener(() => OnItemButtonClicked(index));
            item.buttonText.text = item.name; // tampilkan nama saat awal
            UpdateButton(item);
        }
    }

    public void OnItemButtonClicked(int index)
    {
        if (index < 0 || index >= trailItems.Count) return;

        TrailItem clickedItem = trailItems[index];

        // Jika belum terbeli, coba beli dulu
        if (!clickedItem.unlocked)
        {
            if (studio.SpendMoney(clickedItem.price))
            {
                clickedItem.unlocked = true;
                Debug.Log($"Bought trail: {clickedItem.name}");
            }
            else
            {
                Debug.Log("Not enough money");
                return;
            }
        }

        // Jika sedang equipped, maka unequip
        if (clickedItem.equipped)
        {
            clickedItem.equipped = false;
            trailRenderer.Clear(); // hapus jejak yang tersisa
            trailRenderer.enabled = false;
            Debug.Log($"Unequipped trail: {clickedItem.name}");
        }
        else
        {
            // Unequip semua
            foreach (var item in trailItems)
            {
                item.equipped = false;
            }

            // Equip yang dipilih
            clickedItem.equipped = true;
            trailRenderer.startColor = clickedItem.color;
            trailRenderer.enabled = true;
            Debug.Log($"Equipped trail: {clickedItem.name}");
        }

        // Perbarui semua tombol
        foreach (var item in trailItems)
        {
            UpdateButton(item);
        }
    }

    void UpdateButton(TrailItem item)
    {
        if (!item.unlocked)
        {
            item.buttonText.text = $"{item.name} ({item.price})";
        }
        else
        {
            item.buttonText.text = item.equipped ? $"{item.name} (Unequip)" : $"{item.name} (Equip)";
        }
    }
}
