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
                    CardInfoControl.cardName = "Hepatitis";
                    break;
                case "Card2":
                    CardInfoControl.cardName = "Dengue";
                    break;
                case "Card3":
                    CardInfoControl.cardName = "Leptospirosis";
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
