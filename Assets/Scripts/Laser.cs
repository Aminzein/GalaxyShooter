using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	private float _speed = 6;
	private Player player;
	private bool isEnemyLaser = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(!isEnemyLaser){
    		moveUp();
    	}
    	else{
    		moveDown();
    	}
        
    }
    public void addScoreToPlayer(){
    	player.addScore();
    }

    void moveUp(){
    	transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(transform.position.y >= 8){
        	if(transform.parent){
        		Destroy(transform.parent.gameObject);
        	}
        	Destroy(this.gameObject);
        }
    }

    void moveDown(){
    	transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -8){
        	if(transform.parent){
        		Destroy(transform.parent.gameObject);
        	}
        	Destroy(this.gameObject);
        }
    }

    public void assignEnemyLaser(){
    	isEnemyLaser = true;
    } 
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Player" && isEnemyLaser){
    		player.damage();
    	}
    }
   
}
