using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovito : MonoBehaviour {

	public GameObject gameObj;
	DinnosaurinDuMal eggBroken;
	private TextMesh txt;

	// Use this for initialization
	void Start () {
		eggBroken = gameObject.GetComponent<DinnosaurinDuMal> ();
		txt = gameObj.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnColliderStay2D (){
		if (Input.GetKeyDown (KeyCode.Z)) {
			txt.text = "Crack";
			txt.color = Color.yellow;
			eggBroken.StartCoroutine ("eggWasBroken");
		}
	}
}
