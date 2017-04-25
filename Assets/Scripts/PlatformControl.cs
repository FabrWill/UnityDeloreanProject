using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour {

	//variaveis de controle geral
	public float velocidade_tempoDeQuebra;//velocidade q a plataforma tem
	public Vector3 positionTarget;//posição q se deseja que a plataforma vá
	public float TipoDePlataforma; // muda entre plataforma automatica, manual & de quebra

	float velocidadePasso;//variavel q vai dizer o quanto ele vai se movimentar em 1s
	bool posicao;//troca entre pontos q se movimenta
	Vector3 originalPosition;//grava a posição original para voltar a ela depois

	void Start () {
		originalPosition = transform.position;
		posicao = true;

	}

	IEnumerator OnTriggerStay2D (Collider2D other){
		if (TipoDePlataforma == 2) {
			WalkingPlatform ();
		}
		if (TipoDePlataforma == 3) {
			yield return new WaitForSeconds (4);
			Destroy(this);
		}
	}

	void Update () {
		if (TipoDePlataforma == 1) {
			WalkingPlatform ();
		}

	}

	void WalkingPlatform () {
		velocidadePasso  = velocidade_tempoDeQuebra * Time.deltaTime;

		if (posicao == true) {transform.position = Vector3.MoveTowards(transform.position,positionTarget,velocidadePasso);}

		if(posicao==false){transform.position = Vector3.MoveTowards (transform.position, originalPosition, velocidadePasso);}

		if (transform.position == positionTarget) {posicao = false;}

		if(transform.position == originalPosition) {posicao = true;}
	}
}
