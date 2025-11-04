using UnityEngine;
using ScriptLibrary;

namespace Scripts.Farming
{
    public class SellBox : OnInteractTrigger2D
    {
        protected override void OnInteract()
        {
            OpenSellBox();
        }
        
        private void OpenSellBox()
        {
            print("cant sell, sell box not implemented yet :((((((");
        }
    }
}