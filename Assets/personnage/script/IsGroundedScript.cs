using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedScript : MonoBehaviour {

  private AudioSource Audio;
  public AudioClip soundGround;



  //cette methode ce declenche lorsque quelque chose pernetre à l'interieur du trigger
  void OnTriggerStay2D(Collider2D col)
  {
    transform.parent.GetComponent<PlayerController>().isGrounded = true;
    //J'ai coupe le son parce quej'ai remplacer le ontriggerEnter par OnTriggerStay2D
    // Audio.PlayOneShot(soundGround); 
  }



  //cette methode ce declenche lorsque quelque chose sort du trigger
  void OnTriggerExit2D(Collider2D col)
  {
    transform.parent.GetComponent<PlayerController>().isGrounded = false;
  }

  // Use this for initialization
  void Start () {
    Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
