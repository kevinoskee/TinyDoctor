using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SettingsBtn : MonoBehaviour, IPointerClickHandler
{
    public GameObject SettingsUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        SettingsUI.SetActive(true);
    }
}
