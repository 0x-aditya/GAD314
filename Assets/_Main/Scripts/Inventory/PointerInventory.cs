using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using ScriptLibrary.Singletons;

public class PointerInventory : Singleton<PointerInventory>
{
    [SerializeField] private Canvas canvas; 
    // ReSharper disable once InconsistentNaming
    [SerializeField] private FarmPlant _inventoryItem;

    private Camera cam => Camera.main;
    private RectTransform _pointerRect;

    public FarmPlant inventoryItem
    {
        get => _inventoryItem;
        set
        {
            _inventoryItem = value;
            DestroyOldPointerObject();

            if (_inventoryItem == null) return;
            CreateNewPointerObject();
        }
    }

    private void Start()
    {
        if (canvas == null)
        {
            canvas = Instantiate(new GameObject("Inventory Canvas").AddComponent<Canvas>());
        }
        inventoryItem = _inventoryItem;
    }
    private void Update()
    {
        if (_pointerRect == null) return;
        SetPointerObjectPosition();
    }

    private void SetPointerObjectPosition()
    {
        Vector2 screenPos = Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
        
        if (canvas == null)
            return;

        _pointerRect.position = screenPos;
    }

    public void DestroyOldPointerObject()
    {
        if (_pointerRect == null) return;
        
        Destroy(_pointerRect.gameObject);
        _pointerRect = null;
    }

    private void CreateNewPointerObject()
    {
        if (canvas == null)
        {
            canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                Debug.LogWarning("PointerInventory: No Canvas found.");
                return;
            }
        }

        GameObject go = new GameObject("Pointer Item", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(canvas.transform, false);

        var img = go.GetComponent<Image>();
        img.sprite = _inventoryItem.icon;
        img.raycastTarget = false; // so it doesnt block clicks

        _pointerRect = go.GetComponent<RectTransform>();
        _pointerRect.pivot = new Vector2(0.5f, 0.5f);

        SetPointerObjectPosition();
    }
}
