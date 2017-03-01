using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnosaurinDuMal : MonoBehaviour {


	public float Vision;//visao maxima
	public float speed;//velocidade em cada passo
	public float attackDistance; //traduza

	public GameObject DinossaurTxt;
	public GameObject CharactScript;
	public Transform target;
	public Transform jumpVerify1;
	public Transform jumpVerify2;

	private TextMesh txt;

	PlayerMovement PlayMov;
	CharacterControl Cntrl;
	Animator anim;
	Vector3 location;

	float passo;
	float Distance;
	bool canMove;
	bool jumpy1;
	bool jumpy2;
	bool canAll;
	public bool eggBrok;



	// Use this for initialization
	void Start () {
		PlayMov = CharactScript.GetComponent<PlayerMovement> ();
		Cntrl = CharactScript.GetComponent<CharacterControl> ();
		anim = gameObject.GetComponent<Animator> ();
		txt = DinossaurTxt.GetComponent<TextMesh> ();
		canMove = false;
		eggBrok = false;
		canAll = false;
	}
		
	/// <summary>
	/// breve resumo do que deve acontecer:
	/// distance vai calcular a distancia do personagem ao dinossauro
	/// quando o personagem acionar o evento o dinossauro podera ir atrás do personagem, se o personagem estiver lonnge demais oque vai ser calculado pela distancia.
	/// o dinossauro para de ir atras 
	/// a distancia vai servir também para ver se o monstro vai se alimentar do personagem ou nao
	/// move towards funciona para que o monstro vá atrás do personagems,
	/// melhores elaborões serão necessárias. 
	/// </summary>
	void Update () {

		Debug.Log (Distance);

		if (eggBrok) {
			StartCoroutine (eggWasBroken ());
			eggBrok = false;
			canAll = true;
		}

		passo = speed * Time.deltaTime;
		Distance = Vector3.Distance (target.position, transform.position); //codigo que descobre a distancia entre dois objetos
		//transform.position = Vector3.MoveTowards(transform.position, target, Passo);
		if (canAll) {
			if (canMove) {
				if (Distance < Vision) {
					MonsterLocationUpdate ();
				} else {
					txt.text = "?";
					txt.color = Color.yellow;
					anim.SetBool ("IsWalking", false);
				}
			}

			if (Distance < attackDistance) {
				StartCoroutine (Attack ());
			}
		}
			
	}
		
	void OnBecameInvisible (){
		txt.text = "";
	}

	//serve pra descobrir onde o target está, e mudar a posição do dinossauro. 
	void MonsterLocationUpdate () {
		txt.text = "!";
		txt.color = Color.red;

		anim.SetBool("IsWalking",true);

		//verifica se ele esta encostando em algum objeto do chão para pular
		jumpy1 = Physics2D.Linecast (transform.position, jumpVerify1.position, 1 << LayerMask.NameToLayer ("Ground"));//verificar se esta em algum lugar que deve pular
		jumpy2 = Physics2D.Linecast (transform.position, jumpVerify2.position, 1 << LayerMask.NameToLayer ("Ground"));

		//Quaternion Rotation = Quaternion.LookRotation(target.position - transform.position);//quartenion vai descobrir aonde o objeto está no espaço. 
		//transform.rotation = Rotation;//muda o lado do objeto
		//Debug.Log(transform.rotation);

		//move realmente o objeto
		transform.position = Vector3.MoveTowards (transform.position, target.position, passo);

		//pula se puder pular
		if (jumpy1) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 800f);
		}
		if (jumpy2) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 800f);
		}
	}
		
	//conexão entre o ovo e o dinossauro
	public void chngEgg(bool nha){
		eggBrok = nha;
	}

	IEnumerator eggWasBroken(){
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

	IEnumerator Attack (){
		canMove = false;
		anim.SetBool ("IsWalking", false);
		yield return new WaitForSeconds (0.3f);
		PlayMov.SetMovement (false);
		anim.SetTrigger ("Attacking");
		yield return new WaitForSeconds (0.8f);
		Cntrl.StartCoroutine(Cntrl.DIE ());

	}
}
