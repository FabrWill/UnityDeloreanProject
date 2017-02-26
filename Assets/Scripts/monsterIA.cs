using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterIA : MonoBehaviour {

	public Transform target;
	public float Vision;//visao maxima
	public float Distrusting;//desconfiando
	public float speed;//velocidade em cada passo

	float Distance;
	Animator anim;



	Vector3 location;




	// Use this for initialization
	void Start () {
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
		float Passo = speed * Time.deltaTime;
		Distance = Vector3.Distance (target.position, transform.position); //codigo que descobre a distancia entre dois objetos
		//transform.position = Vector3.MoveTowards(transform.position, target, Passo);


	}


	//serve pra descobrir onde o target está, e mudar a posição do dinossauro. 
	void MonsterSizeUpdate () {
		Quaternion Rotation = Quaternion.LookRotation(target.position - transform.position);//quartenion vai descobrir aonde o objeto está no espaço. 
		transform.rotation = Rotation;//muda o lado do objeto
	}

	void Attack (){
	}
}
