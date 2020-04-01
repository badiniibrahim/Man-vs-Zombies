using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxScrolling : MonoBehaviour {

  float offset;
  public GameObject Player;
  public int speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    offset = Player.transform.position.x / speed;
    GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		
	}
}
