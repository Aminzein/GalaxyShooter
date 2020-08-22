using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField]
	private float speed = 3f;
	// 0 = triple shot
	// 1 = speed
	// 2 = shield
	[SerializeField]
	private int powerUpId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	move();

    }
    void move(){
    	transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y <= -6){
        	Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Player"){
    		Player player = other.GetComponent<Player>();
    		if(powerUpId == 0){
    			player.collectTripleShot();
    		}
    		else if(powerUpId == 1){
    			player.speedPowerUp();
    		}
    		else{
    			player.activateShield();
    		}
    		Destroy(this.gameObject);
    	}
    }
}
