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
	public Transform jumpVerify;

	private TextMesh txt;

	Animator anim;
	CharacterControl Cntrl;
	Vector3 location;

	float passo;
	float Distance;
	bool canMove;
	bool jumpy;
	public bool eggBrok;



	// Use this for initialization
	void Start () {
		Cntrl = CharactScript.GetComponent<CharacterControl> ();
		anim = gameObject.GetComponent<Animator> ();
		txt = DinossaurTxt.GetComponent<TextMesh> ();
		canMove = false;
		eggBrok = false;
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
		}

		passo = speed * Time.deltaTime;
		Distance = Vector3.Distance (target.position, transform.position); //codigo que descobre a distancia entre dois objetos
		//transform.position = Vector3.MoveTowards(transform.position, target, Passo);

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
			Attack ();
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
		jumpy = Physics2D.Linecast (transform.position, jumpVerify.position, 1 << LayerMask.NameToLayer ("Ground"));//verificar se esta em algum lugar que deve pular

		Quaternion Rotation = Quaternion.LookRotation(target.position - transform.position);//quartenion vai descobrir aonde o objeto está no espaço. 
		transform.rotation = Rotation;//muda o lado do objeto

		//move realmente o objeto
		transform.position = Vector3.MoveTowards (transform.position, target.position, passo);

		//pula se puder pular
		if (jumpy) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * 300f);
		}
	}
		
	//conexão entre o ovo e o dinossauro
	public void chngEgg(bool nha){
		eggBrok = nha;
	}

	IEnumerator eggWasBroken(){
		anim.SetBool ("Awakening", true);
		yield return new WaitForSeconds (0.5f);
		anim.SetBool ("Awakening", false);
		canMove = true;
	}

	void Attack (){
		anim.SetBool ("IsWalking", false);
		anim.SetTrigger ("Attacking");
		Cntrl.DIE ();
		canMove = false;

	}
}
