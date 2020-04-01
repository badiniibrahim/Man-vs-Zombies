using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostScript : MonoBehaviour {

  public AudioClip soundHit, soundDead;
  bool dead = false;
  Animator Anim;
  public int point = 40;


	// Use this for initialization
	void Start () {
    Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnCollisionEnter2D(Collision2D collision)
  {
    //Verification s'il ya collision avec le player
    if(collision.gameObject.tag == "Player")
    {
      //vérifier si la transform position du du ghost est plus grande 
      if(transform.position.x > collision.transform.position.x)
      {
        //Ajouter une force qui va pousse le player
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 300 * Time.deltaTime;
      }
      else
      {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 300 * Time.deltaTime;

      }

      GetComponent<AudioSource>().PlayOneShot(soundHit);
      //Applique des degats au player en dimuniant ca vie
      GameObject.Find("Canvas").GetComponent<UInterface>().RomveHealth(10);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.tag == "Player" && !dead)
    {
      dead = true;
      GetComponent<AudioSource>().PlayOneShot(soundDead);
      //Désactiver le collider pour ne pas tuer plusieurs fois
      GetComponent<BoxCollider2D>().enabled = false;
      Anim.SetTrigger("dead");

      //Permets d'avoir accés au canvas pour incrementer le score 
      GameObject.Find("Canvas").GetComponent<UInterface>().AddScore(point);
    }
  }

  void DestroyGO()
  {
    Destroy(gameObject);
  }
}
