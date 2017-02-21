
using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

	PlayerMovement Controle;//classe do movimento do personagem
	TextMesh textmesh;// se mata se não souber


	public bool dialog;
	public bool Fade;
	bool jaEscreveu = false;//booleano que vê se o sistema ja mostrou oque queria
	bool dentroDoDialogo;//booleano que diz se o personagem esta dentro de um dialogo
	public string Texto;//texto dos dialogos
	string[] arrayDialogo;

	int ndialogo;//só pro while funcionar mesmo

	void Start () {
		dentroDoDialogo = false;
		arrayDialogo = Texto.Split('/');//divide o dialogo em um array que vai ser utilizado depois.
		textmesh = gameObject.GetComponent<TextMesh> ();//pega os recursos de texto
		Controle = gameObject.GetComponent<PlayerMovement> ();//importa as variaveis e objetos da classe de movimento do personagem
		ndialogo=0;
	}

	// Se o objeto que o personagem interagir tiver um dialogo, esse codigo vai ativa-lo e bloquear o movimento do personagem.


	//ativa o modo fade se for o caso.
	void OnTriggerEnter2D (Collider2D other){
		Debug.Log ("entered in trigger");
		if (Fade) {
			textmesh.text = Texto;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		Debug.Log ("exit on trigger");
		if (Fade){
			textmesh.text = "";
		}
	}

	void Update(){
		if(dialog){
			if (dentroDoDialogo==false){
				if (Input.GetKeyDown (KeyCode.Z)) {
					Controle.CanFalse();
					GameObject.Find ("player").GetComponent<Animator> ().SetBool("Walk", false);
					printing ();
					dentroDoDialogo = true;
				}
			}


			if (jaEscreveu) {
				if (Input.GetKeyDown (KeyCode.Z)) {
					if (arrayDialogo.Length < ndialogo) {

						Controle.CanTrue ();
						textmesh.text = "";
						dentroDoDialogo = false;
					} else {
						ndialogo++;
						printing (); 
						jaEscreveu = false;
					}
				}
			}
		}
	}




	//codigo de 
	public void printing(){
		textmesh.text = arrayDialogo[ndialogo];
		jaEscreveu = true;

	}
		
}
