using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterIA : MonoBehaviour {

	public float vision;
	public float attackDistance;
	public float speed;

	public Transform posTarget1;// posições de movimentação padrão
	public Transform posTarget2;
	public Transform target;
	public Transform jumpVerify;
	public GameObject textObj;
	public GameObject character;
	private TextMesh txt;

	PlayerMovement playMov;
	CharacterControl cntrl;
	Animator anim;
	Vector3 location;

	bool canMove;
	bool jump;
	float passo;
	float distance;
	int local;


	// Use this for initialization
	void Start () {
		
		playMov = character.GetComponent<PlayerMovement> ();
		cntrl = character.GetComponent<CharacterControl> ();
		anim = gameObject.GetComponent<Animator> ();
		txt = textObj.GetComponent<TextMesh> ();
		local = 1;

	}


	/// <summary>
	/// breve resumo do que deve acontecer
	/// distance vai calcular a distancia do personagem ao dinossauro
	/// quando o personagem acionar o evento o dinossauro podera ir atrás do personagem, se o personagem estiver lonnge demais oque vai ser calculado pela distancia.
	/// o dinossauro para de ir atras 
	/// a distancia vai servir também para ver se o monstro vai se alimentar do personagem ou nao
	/// move towards funciona para que o monstro vá atrás do personagems,
	/// melhores elaborões serão necessárias. 
	/// </summary>
	void Update () {
		passo = speed * Time.deltaTime;
		distance = Vector3.Distance (target.position, transform.position);
		if (canMove) {
			if (distance < vision) {
				MonsterSideUpdate ();
				MonsterMove ();
			} else {
				if (local == 1) {
					transform.position = Vector3.MoveTowards (transform.position, posTarget1.position, passo);
				}
				if (local == 2) {
					transform.position = Vector3.MoveTowards (transform.position, posTarget2.position, passo);
				}
				if (transform.position == posTarget1.position) {
					local = 2;
				}
				if (transform.position == posTarget2.position) {
					local = 1;
				}
			}
		}

	}


	//serve pra descobrir onde o target está, e mudar a posição do monstro.
	void MonsterMove () {
		txt.text = "!";
		txt.color = Color.red;

		anim.SetBool("Walking",true);

		//verifica se ele esta encostando em algum objeto do tipo chão para pular
		jump = Physics2D.Linecast (transform.position, jumpVerify.position , 1 << LayerMask.NameToLayer ("Ground"));


		//move realmente o objeto
		transform.position = Vector3.MoveTowards (transform.position, target.position, passo);

		//pula se puder pular
		if (jump) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 800f);
		}
	}

	IEnumerator distrusting () {
		canMove = false;
		txt.text = "?";
		txt.color = Color.yellow;
		yield return new WaitForSeconds (1f);
		canMove = true;
	}

	//gira o monstro
	void MonsterSideUpdate () {
		Quaternion Rotation = Quaternion.LookRotation(target.position - transform.position);//quartenion vai descobrir aonde o objeto está no espaço. 
		transform.rotation = Rotation;//muda o lado do objeto
	}

	IEnumerator Attack (){//animações e interações ao atacar
		canMove = false;
		anim.SetBool ("Walking", false);
		yield return new WaitForSeconds (0.3f);
		playMov.SetMovement (false);
		anim.SetBool ("Attacking", true);
		yield return new WaitForSeconds (0.8f);
		cntrl.StartCoroutine(cntrl.DIE ()); //inicia o personagem na morte

	}
