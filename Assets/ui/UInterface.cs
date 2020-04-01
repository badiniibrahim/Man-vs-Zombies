using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UInterface : MonoBehaviour {

  public Text txtScore;
  public int score;
  public Image ImBar;
  public Text txtVie;
  public float Health = 100;


  public void AddScore(int point)
  {
    score += point;
    txtScore.text = "SCORE " + score;
  }

  public void RomveHealth(float val)
  {
    Health -= val;
    //Pour ne pas avoir à un moment donne la vie à -10 par exemple
    //Donc on clamp la valeur entre la maxi et mini
    Health = Mathf.Clamp(Health, 0, 100);

    ImBar.fillAmount = Health / 100;
    txtVie.text = Health + " %";


    if(Health <= 0)
    {
      GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDead();
    }
  }

  ///Créer une methode qui va le permetre d'avoir des vies
  public void AddHealth(float val)
  {
    //Copier coller la meme chose que remove mais au lieu de faire un - on fait un +
  }

  //Cette fonction va nous permettre à chaque demarrage de charge d'u level le score 
  public void Start()
  {
    AddScore(PlayerPrefs.GetInt("Score"));
  }
}
