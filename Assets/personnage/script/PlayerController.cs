using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	//Déclarations de variables
	public int speed = 7, jump = 300;
	//Savoir si le player est au sol ou pas
	public bool isGrounded = false;
	private bool isJumping = false;
	//Permets l'animation (c'est à dire faire le player)
	private Animator Anim;
  public AudioClip soudJump, soundDead;
  private AudioSource Audio;


	// Use this for initialization
	void Start () {
		//Pour charge le coposent de notre l'animation
		Anim = GetComponent<Animator> ();
    Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
    //Pour eviter un tremblement lorsque vous déplacer le Player dans un collider
    //On mets la fonction dans le FixedUpdate
  }

  private void OnTriggerStay(Collider other)
  {
    transform.parent.GetComponent<PlayerController>().isGrounded = true;
  }


  //Se dclanche à un temps donnée
  //Conseiller d'utiliser ca lorsque l'on agit sur un regideBody
  void FixedUpdate(){

    //Pour la gestion des touches du clavier, l'axe horizontal pour aller de gauche à droit 
    //float h = Input.GetAxis ("Horizontal");

    //Cette partie permet d'utiliser le joyStick virtuel
    float h = CrossPlatformInputManager.GetAxis("Horizontal");

    //Deplacer selon un vecteur de translation le personnage (gauche, droit ou le haut)
    //(Time.deltaTime) uniformiser la vitesse de translaion selon la puissance de la machine
    transform.Translate(Vector2.right * h * speed * Time.deltaTime);

    //Pour aller à droit
    //Maintenant il faut ajouter l'action qui va permetre de le faire tournée
    //(flipx) permets de faire tourne le personnage
    //On va remarque qu'il ya un temps de latence et pour cela on part dans les settings 
    //De la transistion et on changer interrup source en nextStep
    if (h > 0) GetComponent<SpriteRenderer>().flipX = false; Anim.SetBool("walk", true);

    //Pour aller à gauche
    if (h < 0) GetComponent<SpriteRenderer>().flipX = true; Anim.SetBool("walk", true);

    //Je ne touche à rien
    if (h == 0) Anim.SetBool("walk", false);




    //Gestion du saut lorsque l'on clique sur le clavier
    //isGrounded => permets de savoir que mon personnage est sol pour pouvoir le faire saute
    //Pour pouvoir utiliser le joyStick virtuel je vais remplacer le Input => CrossPlatformInputManager
    if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded){

      //Ajoute de l'audio quand on saute
      Audio.PlayOneShot(soudJump);

			//Récupération du component 
			//On utiliser 2D parce que l'on est 2 demimension
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jump * Time.deltaTime;

			Anim.SetTrigger("jump");
			//Parce que ilne doit pas marche quand il saute
			//Régler l'animation en fonction de la taille du player
			Anim.SetBool("walk",false);

		}

	}

  public void playerDead()
  {
    //Verification du high score
    int score = GameObject.Find("Canvas").GetComponent<UInterface>().score;
    int HS = PlayerPrefs.GetInt("HS");

    if(score > HS)
    {
      PlayerPrefs.SetInt("HS", score);
    }

    Anim.SetTrigger("dead");
    Audio.PlayOneShot(soundDead);
    //Pour eviter que le player bouge lorsqu'il est mort
    GetComponent<PlayerController>().enabled = false;
    //pour Unity 5.5 ou sup (Changement du composant) sinon utiliser isKinematic 
    GetComponent<Rigidbody2D>().simulated = false;
    GetComponent<BoxCollider2D>().enabled = false;
    StartCoroutine(loadGameOver());
    


  }

  IEnumerator loadGameOver()
  {
    yield return new WaitForSeconds(2);
    SceneManager.LoadScene("GameOver");
  }
}
