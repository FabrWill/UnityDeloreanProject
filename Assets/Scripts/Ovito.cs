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

	IEnumerator OnTriggerStay2D (Collider2D other){
		if (Input.GetKeyDown (KeyCode.Z)) {
			yield return new WaitForSeconds (0.3f);
			Debug.Log ("action");
			txt.text = "Crack";
			Debug.Log ("txt");
			txt.color = Color.yellow;
			yield return new WaitForSeconds (0.2f);
			Destroy (gameObject);
			dino.chngEgg (true);
			Debug.Log ("coroutine");
			Destroy (this);
		}
	}
}