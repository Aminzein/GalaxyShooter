using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private float rotateSpeed = 5f;
	private Animator asteroidAnimator;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip explosionAudioClip;
    // Start is called before the first frame update
    void Start()
    {
     	asteroidAnimator = GetComponent<Animator>();
     	audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Laser"){
    		audioSource.clip = explosionAudioClip;
    		Destroy(other.gameObject);
    		audioSource.Play();
    		asteroidAnimator.SetTrigger("onCollisionWithLaser");
    		Destroy(this.gameObject , 2.5f);
    	}
    }
}
