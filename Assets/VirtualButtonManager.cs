using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualButtonManager : MonoBehaviour {

	private static VirtualButtonManager _instance;
	public static VirtualButtonManager Instance { get { return _instance; } }

	public Text warningText;
	public Transform VirtualButtonParent;

	private List<int> buttonList = new List<int>();

	// Use this for initialization
	void Awake (){
		_instance = this;
	}

	public void AddButtonToList(int buttonNum){
		buttonList.Add (buttonNum);
		CheckForLineOfSite ();
	}

	public void RemoveButtonFromList(int buttonNum){
		buttonList.Remove (buttonNum);
		CheckForLineOfSite ();
	}

	void CheckForLineOfSite(){

		ResetButtons ();

		List<int> even = new List<int> ();
		List<int> odd = new List<int> ();

		//check to see if any areas in a column are blocking each other
		foreach (int button in buttonList) {
			if (button % 2 == 0) {
				even.Add (button);
			} else {
				odd.Add (button);
			}
		}

		if (even.Count > 1) {
			TurnOnWarning (even);
		} else if (odd.Count > 1) {
			TurnOnWarning (odd);
		} 
	}

	void TurnOnWarning(List<int> blockingAreas){
		warningText.transform.parent.gameObject.SetActive (true);
		string warning = "Line Of Site: \n Quadrants: ";
		foreach (int childNum in blockingAreas) {
			warning += childNum + ", ";
		}
		warning = warning.Trim ();
		warning = warning.Remove (warning.Length - 1);
		warningText.text = warning;
		AnimCoroutine = StartCoroutine (DelayAnimation (blockingAreas));
	}

	Coroutine AnimCoroutine;
	IEnumerator DelayAnimation(List<int> blockingAreas){
		foreach (int childNum in blockingAreas) {
			VirtualButtonParent.GetChild (childNum).GetComponent<MeshRenderer> ().enabled = true;
			//VirtualButtonParent.GetChild (childNum).GetComponent<Animation> ().Play ();
			yield return new WaitForSeconds (.2f);
		}
	}

	void ResetButtons(){
		if (AnimCoroutine != null) {
			StopCoroutine (AnimCoroutine);
		}
		//turn off mesh renderers of buttons and disable warning text
		foreach (Transform child in VirtualButtonParent) {
			child.GetComponent<MeshRenderer> ().enabled = false;
			child.GetComponent<Animation> ().Stop ();
		}
		warningText.transform.parent.gameObject.SetActive (false);
	}
}
