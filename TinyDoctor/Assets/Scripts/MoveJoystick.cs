using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MoveJoystick : MonoBehaviour
{
	public Joystick Joystick;
    public TouchField TouchField;
    public JumpBtn JumpBtn;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var fps = GetComponent<RigidbodyFirstPersonController>();
        fps.RunAxis.x = Joystick.Horizontal();
        fps.RunAxis.y = Joystick.Vertical();
        fps.mouseLook.LookAxis = TouchField.TouchDist;
        fps.JumpAxis = JumpBtn.Pressed;



	}
}
