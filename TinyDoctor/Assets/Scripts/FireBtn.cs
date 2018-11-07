
using UnityEngine;
using UnityEngine.EventSystems;
public class FireBtn : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public bool Pressed = false;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Pressed = true;
    }
 
}