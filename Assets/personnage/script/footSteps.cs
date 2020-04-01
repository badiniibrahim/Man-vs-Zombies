using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cette classe permets de faire joue du son lorsque
 * le personnage se deplace
 * */
public class footSteps : MonoBehaviour {

  public AudioClip SoundFootSteps;
  private AudioSource Audio;
  public float PitchMin = 0.9f, PitchMax = 1.2f, VoMin = 0.8f, VolMax =1.2f;

	// Use this for initialization
	void Start () {
    Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
    //Deplacement sur l'axe horizontal
    if(Input.GetAxis("Horizontal") != 0){

      if (!Audio.isPlaying && transform.parent.GetComponent<PlayerController> ().isGrounded){
          Audio.pitch = Random.Range(PitchMin, PitchMax);
          Audio.volume = Random.Range(VoMin, VolMax);
          Audio.pitch = 1;
          Audio.volume = 1;
          Audio.PlayOneShot(SoundFootSteps);
      }
     
    }
		
	}
}
