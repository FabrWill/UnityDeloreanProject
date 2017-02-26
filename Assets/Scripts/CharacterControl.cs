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

	float timeFadeOut;
	float time;
	bool exitFade;
	bool act;
	Color nhaw;

	void Start () {
		Can = gameObject.GetComponent<PlayerMovement>();
		Anim = gameObject.GetComponent<Animator> ();
		timeFadeOut = 10;
		exitFade = false;
		time = 0;
		act = false;
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

	void onGUI(){
		if (act) {
			time += Time.deltaTime; // muda o tempo que o fade está rodando conforme o tempo do jogo
			nhaw.a = timeFadeOut / time;// serve pra calcular a transparencia progressivamente da textura


			if (exitFade == false) {//serve enquanto o fade estiver rodando, criando assim a textura e adicionando o efeito
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeTexture); //criando textura
				GUI.color = nhaw;//efeito de fade apartir do alpha.

			}
			if (time > timeFadeOut) {//quando o tempo de execução for maior do que o estipulado
				exitFade = true;//para de rodar o fade
				time = 0;//e volta o tempo pra zero
			}

			if (exitFade) {//quando o fade acaba, é só partir pro abraço.
				if (GUI.Button (new Rect (500, 500, 100, 50), "Restart")) {//botão pra reiniciar
					SceneManager.LoadScene ("Phase-1", LoadSceneMode.Single);//recarrega o nivel (necessidade de atençao no futuro).
				}
			}
		}
	}

	/// <summary>
	/// /Sistema de morte do personagem:
	/// a ideia é que execute a animação do personagem morrendo
	/// em seguida  
	/// </summary>
	public void DIE (){
		Anim.SetBool ("Morreu", true);// teoricamente ativara a animação de morte apenas, como esta no animator deve 
									  //rodar apenas uma vez
		act=true;

	}

}