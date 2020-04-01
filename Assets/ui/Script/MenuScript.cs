using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuScript : MonoBehaviour {

  Text txtHighScore;


   void Start()
  {
    //Affichage du high score
    //Etant donne que le HighScore est un enfant du canvas
    //Attention après il va falloire change ca
    txtHighScore = transform.Find("BackGround").Find("HighScore").GetComponent<Text>();
    txtHighScore.text = "HIGH SCORE " + PlayerPrefs.GetInt("HS");
  }


  public void play()
  {
    //Chargement d'une scene
    SceneManager.LoadScene("Level_1");
  }

  //Cette fonction de quitter le jeu c'est à dire lorsque l'utilisateur clique sur exit 
  public void Exit()
  {
    Application.Quit();
  }


  //Cette partie va permetre au player de continue le jeu
  public void Continue(){
    SceneManager.LoadScene(PlayerPrefs.GetString("LevelToLoad"));

  }

}
