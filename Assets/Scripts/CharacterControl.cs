using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour {

	private Animator Anim;
	public Transform GroundVerify;
	public Texture FadeTexture;
	public float RunSpeed;
	public float JumpForce;
	public bool CanJump;
	PlayerMovement Can;

	float time;
	bool act;

	void Start () {
		Can = gameObject.GetComponent<PlayerMovement>();
		Anim = gameObject.GetComponent<Animator> ();
		act = false;
	}
		
	void Update () {
		time += Time.deltaTime; // muda o tempo que o fade está rodando conforme o tempo do jogo
		CanJump = Physics2D.Linecast (transform.position, GroundVerify.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Can.CanWalk == true) {
				if (CanJump == true) {
					Can.setCan (true);
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

	void OnGUI(){
		if (act) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeTexture); //criando textura

			if (GUI.Button (new Rect (Screen.width/2, Screen.height/2, 100, 50), "Restart")) {//botão pra reiniciar
					SceneManager.LoadScene ("Phase-1", LoadSceneMode.Single);//recarrega o nivel (necessidade de atençao no futuro).
			}
		}
	}

	/// <summary>
	/// /Sistema de morte do personagem:
	/// a ideia é que execute a animação do personagem morrendo
	/// em seguida  
	/// </summary>
	public IEnumerator DIE (){
		Anim.SetBool ("Morreu", true);// teoricamente ativara a animação de morte apenas, como esta no animator deve 
									  //rodar apenas uma vez
		yield return new WaitForSeconds (1.7f);
		act=true;

	}

}