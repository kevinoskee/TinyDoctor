using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour, IPointerClickHandler
{
    Color unlocked = Color.white;
    public GameObject InfoUI;
    public ItemInfoControl itemInfoControl;
    public void OnPointerClick(PointerEventData eventData)
    {
        Image[] tempImage;
        Image item = null;
        tempImage = gameObject.GetComponentsInChildren<Image>();
        foreach (Image i in tempImage)
        {
            if (i.name == "Image")
            {
                item = i;
                break;
            }
        }
        switch (gameObject.name)
        {
            case "Item1":
                ItemInfoControl.itemIndex = 0;
                break;
            case "Item2":
                ItemInfoControl.itemIndex = 1;
                break;
            case "Item3":
                ItemInfoControl.itemIndex = 2;
                break;
            case "Item4":
                ItemInfoControl.itemIndex = 3;
                break;
            case "Item5":
                ItemInfoControl.itemIndex = 4;
                break;

        }
  
        ItemInfoControl.ItemImage = item.sprite;
        InfoUI.SetActive(true);
        itemInfoControl.GetInfo();

    }
}
