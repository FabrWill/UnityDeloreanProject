using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovito : MonoBehaviour {

	public GameObject dinossauro;
	public GameObject gameObj;
	DinnosaurinDuMal dino;
	private TextMesh txt;

	// Use this for initialization
	void Start () {
		dino = dinossauro.GetComponent<DinnosaurinDuMal> ();
		txt = gameObj.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D (Collider2D other){
		if (Input.GetKeyDown (KeyCode.Z)) {
			Debug.Log ("action");
			txt.text = "Crack";
			Debug.Log ("txt");
			txt.color = Color.yellow;
			dino.chngEgg (true);
			Debug.Log ("coroutine");
		}
	}
}
