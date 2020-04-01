using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraParallax : MonoBehaviour {

  public GameObject Player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    //Permets de faire bouge la camera lorsque le player bouge
    //Il faut placer le parallax dans le mainCamera
    transform.position = new Vector3(Player.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
	}
}
