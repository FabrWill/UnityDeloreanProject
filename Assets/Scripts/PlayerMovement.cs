using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Classe utilizada para controlar as animações do personagem quando & enquanto ele andar

	//import das class
	private Animator Anim;
	private Vector2 Move;
	private CharacterControl Control; //classe de movimento do personagem
	public bool CanWalk;//controle de pulo do personagem

	void Start () {
		Control = gameObject.GetComponent<CharacterControl>();
		Anim = gameObject.GetComponent<Animator> ();
		CanWalk = true;
	}

	// função primitiva de mudança de valor da variavel Can Walk - "mudado na versão 0.0.0.0.9 para melhor utilização de P.O.O"
	public void setCan(bool CanWalk){
		this.CanWalk = CanWalk;
	}


	// vai mudando a animação conforme necessário
	void Update () {

		//vê qual o movimento do personagem assim como na classe principal
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
