using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardControl : MonoBehaviour, IPointerClickHandler
{
    Color unlocked = Color.white;
    public GameObject CardInfo;
    public GameObject Alert;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Image>().color == unlocked)
        {
            switch (gameObject.name)
            {
                case "Card1":
                    CardInfoControl.cardIndex = 0;
                    break;
                case "Card2":
                    CardInfoControl.cardIndex = 1;
                    break;
                case "Card3":
                    CardInfoControl.cardIndex = 2;
                    break;

            }
            CardInfoControl.CardImage = gameObject.GetComponent<Image>().sprite;
            CardInfo.SetActive(true);
        }
        else
        {
            AlertUI.alert = "Play to unlock card";
            Instantiate(Alert);
        }
           
    }
}
