using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnosaurinDuMal : MonoBehaviour {


	public float Vision;//visao maxima
	public float speed;//velocidade em cada passo
	public float attackDistance; //traduza

	public GameObject DinossaurTxt;//conexão de texto do monstro / desconfiometro ou n
	public GameObject CharactScript;// conexão para matar o personagem.
	public Transform target;// utilizado para descobrir a posição do personagem
	// variaveis para o pulo
	public Transform jumpVerify1;
	public Transform jumpVerify2;

	//imports
	private TextMesh txt;

	PlayerMovement PlayMov;
	CharacterControl Cntrl;
	Animator anim;
	Vector3 location;
	//imports

	float passo;
	float Distance;
	bool canMove;
	bool jumpy1;
	bool jumpy2;
	bool canAll;
	public bool eggBrok;



	void Start () {
		PlayMov = CharactScript.GetComponent<PlayerMovement> ();
		Cntrl = CharactScript.GetComponent<CharacterControl> ();
		anim = gameObject.GetComponent<Animator> ();
		txt = DinossaurTxt.GetComponent<TextMesh> ();
		canMove = false;
		eggBrok = false;
		canAll = false;
	}
		

	void Update () {

		if (eggBrok) { // descobre se o usuario quebrou o ovo e ativa o dinossauro
			Debug.Log("egg brok");
			StartCoroutine (eggWasBroken ());
			eggBrok = false;
			canAll = true;
		}

		passo = speed * Time.deltaTime;// define velocidade p/s do dinossauro
		Distance = Vector3.Distance (target.position, transform.position); // distancia - objeto essencial
		if (canAll) {
			if (canMove) {
				if (Distance < Vision) {// se esta na visão o monstro se movimenta, se nao muda a forma como ele fica
					MonsterLocationUpdate ();
					MonsterSideUpdate ();
				} else {
					txt.text = "?";
					txt.color = Color.yellow;
					anim.SetBool ("IsWalking", false);
				}

				if (Distance < attackDistance) {//ataca
					StartCoroutine (Attack ());
				}
			}

		}
			
	}

	void OnBecameVisible (){
		txt.text = "";
		canAll = true;
	}
		
	void OnBecameInvisible (){ // função que para a execução do objeto
		txt.text = "";
		canAll = false;
	}

	//serve pra descobrir onde o target está, e mudar a posição do dinossauro. 
	void MonsterLocationUpdate () {

		Debug.Log ("posicao dino " + transform.position.x + " posicao mlk" + target.position.x); 

		txt.text = "!";
		txt.color = Color.red;

		anim.SetBool("IsWalking",true);

		//move realmente o objeto
		transform.position = Vector3.MoveTowards (transform.position, target.position, passo);

		//verifica se ele esta encostando em algum objeto do tipo chão para pular
		jumpy1 = Physics2D.Linecast (transform.position, jumpVerify1.position, 1 << LayerMask.NameToLayer ("Ground"));//verificar se esta em algum lugar que deve pular
		jumpy2 = Physics2D.Linecast (transform.position, jumpVerify2.position, 1 << LayerMask.NameToLayer ("Ground"));

		//pula se puder pular
		if (jumpy1) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 800f);
		}
		if (jumpy2) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 800f);
		}
			
	}

	void MonsterSideUpdate(){
		if (target.position.x < transform.position.x){
			transform.eulerAngles = new Vector2 (0, 0);
		}
		if (target.position.x > transform.position.x){
			transform.eulerAngles = new Vector2 (0, 180);
		}

	}
		
	//conexão entre o ovo e o dinossauro, para modificação do valor de eggBrok
	public void chngEgg(bool nha){
		eggBrok = nha;
	}

	IEnumerator eggWasBroken(){// animações e interações  ao acordar
		anim.SetBool ("Awakening", true);
		txt.text = "-.-";
		txt.color = Color.cyan;
		yield return new WaitForSeconds (1.3f);
		anim.SetBool ("Awakening", false);
		txt.text = "'-'";
		txt.color = Color.magenta;
		yield return new WaitForSeconds (1.3f);
		canMove = true;
	}

	IEnumerator Attack (){//animações e interações ao atacar
		canMove = false;
		anim.SetBool ("IsWalking", false);
		yield return new WaitForSeconds (0.3f);
		PlayMov.SetMovement (false);
		anim.SetTrigger ("Attacking");
		yield return new WaitForSeconds (0.8f);
		Cntrl.StartCoroutine(Cntrl.DIE ()); //inicia o personagem na morte

	}
}
