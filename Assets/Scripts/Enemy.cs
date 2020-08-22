using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float _speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
    	Vector3 position = new Vector3 (Random.Range(-9f , 9f) , 6 , 0);
       
    }

    // Update is called once per frame
    void Update()
    {
    	transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -6){
        	transform.position = new Vector3 (Random.Range(-9f , 9f) , 6 , 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject.tag == "Laser"){
    		other.GetComponent<Laser>().addScoreToPlayer();
    		Destroy(other.gameObject);
    		Destroy(this.gameObject);
    	}
    	else if(other.gameObject.tag == "Player"){
    		Player player = other.transform.GetComponent<Player>();
    		if(player != null){
    			player.damage();
    		}
    		Destroy(this.gameObject);
    	}
    }
}
