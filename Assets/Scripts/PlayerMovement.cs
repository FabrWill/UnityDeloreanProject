using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Animator Anim;
	private Vector2 Move;
	private CharacterController Control;
	public bool CanWalk;

	void Start () {
		Control = gameObject.GetComponent<CharacterController>();
		Anim = gameObject.GetComponent<Animator> ();
		CanWalk = true;
	}

	public void CanTrue(){
		CanWalk = true;
	}

	public void CanFalse(){
		CanWalk = false;
	}

	void Update () {

		Move = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Move != Vector2.zero && CanWalk) {
			Anim.SetBool ("Walk", true);
			Anim.SetFloat ("X", Move.x);

			if (Control.CanJump == false) {
				Anim.SetFloat ("Y", 1);
			} else {
				Anim.SetFloat ("Y", 0);
			}
		} 

		else {
			Anim.SetBool ("Walk", false);
		}


			
	}

	public void SetMovement (bool Value){
		CanWalk = Value;
		if (CanWalk == false) {
			Anim.SetBool ("Walk", false);
		}
	}
		
}
