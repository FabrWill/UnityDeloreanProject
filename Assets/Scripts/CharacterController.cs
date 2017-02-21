using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	private Animator Anim;
	public Transform GroundVerify;
	public float RunSpeed;
	public float JumpForce;
	public bool CanJump;
	PlayerMovement Can;

	void Start () {
		Can = gameObject.GetComponent<PlayerMovement>();
	}
		
	void Update () {
		CanJump = Physics2D.Linecast (transform.position, GroundVerify.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Can.CanWalk == true) {
				if (CanJump == true) {
					Can.CanTrue ();
					if (Input.GetKeyDown (KeyCode.UpArrow) && CanJump) {
						GetComponent<Rigidbody2D> ().AddForce (transform.up * JumpForce);
					}
				}
				if (Input.GetAxisRaw ("Horizontal") > 0) {
					transform.Translate (Vector2.right * RunSpeed * Time.deltaTime);
				}
				if (Input.GetAxisRaw ("Horizontal") < 0) {
					transform.Translate (Vector2.left * RunSpeed * Time.deltaTime);
				}
		}
	}

}