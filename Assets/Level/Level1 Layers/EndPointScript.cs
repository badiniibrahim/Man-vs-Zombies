using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointScript : MonoBehaviour {

  Animator Anim;
  public string LevelToLoad;
  public AudioClip soundWin;

	void Start () {
    Anim = GetComponent<Animator>();
	}
	
	void Update () {
		
	}

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.tag == "Player")
    {
      Anim.SetTrigger("win");
      GetComponent<AudioSource>().PlayOneShot(soundWin);
      GetComponent<BoxCollider2D>().enabled = false;
    }
  }

  //Chargement du level suivant si le player gagner
  void LoadLevel()
  {
    //A chaque fois que l'on arrive au level suivant on enregistre le high score dans une preference
    /*PlayerPrefs.SetInt("Score", GameObject.Find("Canvas").GetComponent<UInterface>().score);

    //A partir de la on enregistre le level dans une preference
    PlayerPrefs.SetString("LevelToLoad", LevelToLoad);
    SceneManager.LoadScene(LevelToLoad);*/

    StartCoroutine(loadGameOver());

  }

  IEnumerator loadGameOver()
  {
    yield return new WaitForSeconds(2);
    //A chaque fois que l'on arrive au level suivant on enregistre le high score dans une preference
    PlayerPrefs.SetInt("Score", GameObject.Find("Canvas").GetComponent<UInterface>().score);

    //A partir de la on enregistre le level dans une preference
    PlayerPrefs.SetString("LevelToLoad", LevelToLoad);
    SceneManager.LoadScene(LevelToLoad);
  }
}
