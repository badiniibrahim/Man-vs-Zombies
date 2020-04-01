using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {


	void Start () {
    StartCoroutine(chargement());
    //A chaque fois l'utilisateur perd on remet le score à 0
    PlayerPrefs.SetInt("Score", 0);
	}
	

  //On va utiliser un coroutine pour faire la temporisation
  //Pour cela on va utiliser un yeld et on lui passe le temps en parametre
  IEnumerator chargement()
  {
    yield return new WaitForSeconds(2);
    SceneManager.LoadScene("Menu");
  }
}
