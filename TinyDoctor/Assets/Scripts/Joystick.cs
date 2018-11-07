using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image backgroundImg;
	private Image joystickImg;
	public Vector3 inputVector;

	private void Start()
	{
		backgroundImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 position;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (backgroundImg.rectTransform,
			   ped.position,
			   ped.pressEventCamera,
			   out position)) 
		{
			position.x = (position.x / backgroundImg.rectTransform.sizeDelta.x);
			position.y = (position.y / backgroundImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 ((position.x - 0.5f) * 2 , 0, (position.y - 0.5f) * 2);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			//Moving Joystick
			joystickImg.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * (backgroundImg.rectTransform.sizeDelta.x / 3), 
				inputVector.z * (backgroundImg.rectTransform.sizeDelta.y / 3));
			

		}

	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}
	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal()
	{
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis("Horizontal");
		
	}
	public float Vertical()
	{
		if (inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis("Vertical");
	}
}
