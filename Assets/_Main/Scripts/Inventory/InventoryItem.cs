using Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour, IPointerClickHandler
    {
        public BaseItem itemData;
        private Image _image;
        
        // actual count of item in this stack
        [SerializeField] private int count = 1;
        public int itemCount
        {
            get => count;
            set => UpdateItemCount(value);
        } 
        // currently attached item to pointer
        public static InventoryItem CurrentlyAttached; 
        
        // is this item clicked and attached to pointer
        private bool _isAttachedToPointer;

        private void Start()
        {
            _image = GetComponent<Image>();

            if (itemData)
            {
                InitializeItem(itemData);
            }

            itemCount = count;
        }
        private void Update()
        {
            // for item swapping in ItemSlot
            _image.raycastTarget = !CurrentlyAttached; // disable raycast if an item is attached to pointer
            
            if (!_isAttachedToPointer) return;
            
            transform.position = Input.mousePosition;
            
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (CurrentlyAttached)
            {
                if (CurrentlyAttached.itemData != itemData)
                {
                    CurrentlyAttached.AttachToObject(transform.parent);
                    AttachToPointer();
                }
            }
            if (_isAttachedToPointer) return;
            AttachToPointer();

        }

        private void AttachToPointer()
        {
            _isAttachedToPointer = true;
            _image.raycastTarget = false;
            CurrentlyAttached = this;
            transform.SetParent(transform.root);
        }
        public void AttachToObject(Transform newParent)
        {
            _image = GetComponent<Image>();
            _isAttachedToPointer = false;
            _image.raycastTarget = true;
            CurrentlyAttached = null;
            transform.SetParent(newParent);
            transform.localScale = Vector3.one;
        }

        private void InitializeItem(BaseItem item)
        {
            _image.sprite = item.itemIcon;
        }

        private void UpdateItemCount(int amount)
        {
            if (itemData != null)
            {
                if (count + amount <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            count = amount;
            if (count == 0)
            {
                Destroy(this.gameObject);
                return;
            }
            GetComponentInChildren<TextMeshProUGUI>().text = count == 1 ? "" : count.ToString();
        }
    }
}