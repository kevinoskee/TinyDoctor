using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlertUI : MonoBehaviour
{
    public static string alert;

    private void Start()
    {
        GetComponentInChildren<Text>().text = alert;
        Destroy(gameObject, GetComponentInChildren<Animation>().GetClip("AlertAnim").length);
    }
}
