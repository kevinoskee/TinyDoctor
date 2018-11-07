using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardControl : MonoBehaviour, IPointerClickHandler
{
    Color unlocked = Color.white;
    Color locked = Color.gray;
    public GameObject CardInfo;
    public GameObject Alert;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Image>().color == unlocked)
        {
            switch (gameObject.name)
            {
                case "Card1":
                    CardInfoControl.CardImage = gameObject.GetComponent<Image>().sprite;
                    CardInfoControl.JSONFile = "/Card1.json";
                    break;
                case "Card2":
          
                    break;
                case "Card3":
            
                    break;

            }
            CardInfo.SetActive(true);
        }
        else
        {
            AlertUI.alert = "Play to unlock card";
            Instantiate(Alert);
        }
           
    }
}
