using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie2 : MonoBehaviour {

  public Transform PtsA, PtsB;
  public float Speed;
  bool Retour = false;
  Animator Anim;
  public AudioClip soundDead, soundHit;
  public int point = 60;

  //Pour savoir si le zombie est mort pour ne pas pouvoir le touche deux fois
  bool dead = false;


  // Use this for initialization
  void Start()
  {
    Anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    //Pour ne pas faire tomber le zombie
    if (transform.position.x > PtsB.transform.position.x)
    {
      transform.position = new Vector2(PtsB.transform.position.x, transform.position.y);
    }

    if (transform.position.x < PtsA.transform.position.x)
    {
      transform.position = new Vector2(PtsB.transform.position.x, transform.position.y);
    }


    if (!Retour)
    {
      transform.position = Vector2.MoveTowards(transform.position, PtsB.position, Speed * Time.deltaTime);
    }
    else
    {
      transform.position = Vector2.MoveTowards(transform.position, PtsA.position, Speed * Time.deltaTime);

    }
  }

  //Savoir s'il est rentre en collision avec les poinst A et B
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "PtsA")
    {
      Retour = false;
      GetComponent<SpriteRenderer>().flipX = true;
    }


    if (collision.gameObject.name == "PtsB")
    {
      Retour = true;
      GetComponent<SpriteRenderer>().flipX = false;

    }

    //Collision avec le player
    if (collision.gameObject.tag == "Player" && !dead)
    {
      dead = true;
      GetComponent<AudioSource>().PlayOneShot(soundDead);
      Anim.SetTrigger("dead");
      //Fix le zombie au sol pour qu'il ne bouge plus meme si on le touche
      GetComponent<Rigidbody2D>().isKinematic = true;
      GetComponent<BoxCollider2D>().enabled = false;
      GetComponent<Zombie2>().enabled = false;
      //Permets d'avoir accés au canvas pour incrementer le score 
      GameObject.Find("Canvas").GetComponent<UInterface>().AddScore(point);
    }
  }

  void DestroyGO()
  {
    Destroy(gameObject);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      //Faire tourne le zombie
      if (transform.position.x > collision.transform.position.x)
      {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = (-Vector2.right * 200 * Time.deltaTime);
      }
      else
      {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.right * 200 * Time.deltaTime);


      }

      GetComponent<AudioSource>().PlayOneShot(soundHit);
      //Applique des degats au player en dimuniant ca vie
      GameObject.Find("Canvas").GetComponent<UInterface>().RomveHealth(10);
    }
  }
}
