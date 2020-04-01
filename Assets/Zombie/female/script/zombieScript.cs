using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour {

  public GameObject target;
  public float distance;
  public float attack = 5;
  public float speed = 0.5f;
  Animator Anim;
  public AudioClip SoundHit, SoundDead;
  bool pause = false, dead = false;
  public int point = 20;




  // Use this for initialization
  void Start () {
    Anim = GetComponent<Animator>();
  }
	
	// Update is called once per frame
	void Update () {

   
      
  }

  void FixedUpdate()
  {
    //Calculer la distance pour que le zombie detecte la cible
    distance = Vector2.Distance(transform.position, target.transform.position);

    if (distance < attack && !pause)
    {
      //Faire deplace le zombie
      Anim.SetBool("walk", true);
      Attack();
    }
    else
    {
      Anim.SetBool("walk", false);

    }

    //Faire tourne le zombie
    if (transform.position.x > target.transform.position.x)
    {
      GetComponent<SpriteRenderer>().flipX = true;
    }
    else
    {
      GetComponent<SpriteRenderer>().flipX = false;

    }
  }

    void Attack()
  {
   transform.position = Vector2.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
  }


  //Gestion de l'entrée en collision pour tuer le zombie
  //Il faut activer le istrigger pour savoir si il ya une collision
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.tag == "Player" && !dead)
    {
      Anim.SetTrigger("dead");
      GetComponent<AudioSource>().PlayOneShot(SoundDead);
      //Permets d'arrete le script lorsque l'on touche le zombie

      //Parce que meme si le zombie mort et que l'on le touche on perd des vies donc on assure la gestion ici
      dead = true;
      GetComponent<zombieScript>().enabled = false;
      //Pour ne pas que l'on reste sur zombie lorsque il est mort
      GetComponent<BoxCollider2D>().enabled = false;
      GetComponent<Rigidbody2D>().isKinematic = true;
      //Permets d'avoir accés au canvas pour incrementer le score 
      GameObject.Find("Canvas").GetComponent<UInterface>().AddScore(point);
    }
  }

  public void DestroyGo()
  {
    //Et l'on part dans l'onglet event pour le choisir 
    Destroy(gameObject);
  }

  //Voir si c'est la player qui entre en collision avec le zombiz
  void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Player" && !dead)
    {
      StartCoroutine(hit(collision.gameObject));
    }
  }

  //Faire une temporisation
  IEnumerator hit(GameObject col)
  {
    Debug.Log("Le player est touche");
    GetComponent<AudioSource>().PlayOneShot(SoundHit);
    //Pour eviter que le zombie n'attaque plusieur fois
    //Cela veut que lorsqu'il attaque une fois on fait une pause pour que le player se deplace
    pause = true;

    //Faire tourne le zombie
    if (transform.position.x > target.transform.position.x)
    {
      col.GetComponent<Rigidbody2D>().velocity = (-Vector2.right * 200 * Time.deltaTime);
    }
    else
    {
      col.GetComponent<Rigidbody2D>().velocity = (Vector2.right * 200 * Time.deltaTime);


    }

    //Applique des degats au player en dimuniant ca vie
    GameObject.Find("Canvas").GetComponent<UInterface>().RomveHealth(10);

    yield return new WaitForSeconds(1);
    pause = false;

  }
}
