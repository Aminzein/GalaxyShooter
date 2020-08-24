using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private float rotateSpeed = 5f;
	private Animator asteroidAnimator;
    // Start is called before the first frame update
    void Start()
    {
     	asteroidAnimator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Laser"){
    		Destroy(other.gameObject);
    		asteroidAnimator.SetTrigger("onCollisionWithLaser");
    		Destroy(this.gameObject , 2.5f);
    	}
    }
}
