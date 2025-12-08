using ScriptLibrary.Singletons;
using TMPro;
using UnityEngine;
namespace Scripts.Inventory
{
    public class ItemDescription : Singleton<ItemDescription>
    {
        [SerializeField] private GameObject parentObject;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;
        
        public void ShowDescription(Vector2 position)
        {
            parentObject.transform.position = position;
            parentObject.SetActive(true);
        }
        
        public void HideDescription()
        {
            parentObject.SetActive(false);
            ClearDescription();
        }
        
        private void ClearDescription()
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
        }

        public void UpdateDescription(string newName, string newDescription)
        {
            itemNameText.text = newName;
            itemDescriptionText.text = newDescription;
        }
        public void MoveDescription(Vector2 newPosition)
        {
            parentObject.transform.position = newPosition;
        }
    }
}