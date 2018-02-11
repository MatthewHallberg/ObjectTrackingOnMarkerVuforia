using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonController : MonoBehaviour, IVirtualButtonEventHandler {

	private VirtualButtonBehaviour virtualButtonBehavior;
	private int buttonNum;

	void Start(){
		virtualButtonBehavior = GetComponent<VirtualButtonBehaviour> ();
		virtualButtonBehavior.RegisterEventHandler (this);
		buttonNum = int.Parse (virtualButtonBehavior.VirtualButtonName);
		GetComponent<MeshRenderer> ().enabled = false;
	}

	public void OnButtonPressed(VirtualButtonBehaviour VBbehavior){
		VirtualButtonManager.Instance.AddButtonToList (buttonNum);
	}

	public void OnButtonReleased(VirtualButtonBehaviour VBbehavior){
		VirtualButtonManager.Instance.RemoveButtonFromList (buttonNum);
	}
}
