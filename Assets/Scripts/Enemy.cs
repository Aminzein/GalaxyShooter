using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float _speed = 4f;
	[SerializeField]
	private Animator enemyAnimatorComponent;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip explosionAudioClip;
	[SerializeField]
	private GameObject laserPrefab;
	private float fireRate = 3.0f;
	private float canFire = -1;
    // Start is called before the first frame update
    void Start()
    {
    	Vector3 position = new Vector3 (Random.Range(-9f , 9f) , 6 , 0);
       	enemyAnimatorComponent = GetComponent<Animator>();
       	audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	calculateMovement();
    	if(Time.time > canFire){
    		fireRate = Random.Range(3,7);
    		canFire = Time.time + fireRate;
    		GameObject enemyLasers = Instantiate(laserPrefab , transform.position, Quaternion.identity);
    		Laser[] lasers = enemyLasers.GetComponentsInChildren<Laser>();
    		for(int i = 0;i<lasers.Length;i++){
    			lasers[i].assignEnemyLaser();
    		}
    	}
    }

    void calculateMovement(){
    	transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -8){
        	transform.position = new Vector3 (Random.Range(-9f , 9f) , 6 , 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Laser" && !other.gameObject.transform.parent){
    		audioSource.clip = explosionAudioClip;
    		other.GetComponent<Laser>().addScoreToPlayer();
    		Destroy(other.gameObject);
    		enemyAnimatorComponent.SetTrigger("onEnemyDeath");
    		_speed = 0;
    		audioSource.Play();
    		Destroy(GetComponent<Collider2D>());
    		Destroy(this.gameObject , 2.5f);
    	}
    	else if(other.gameObject.tag == "Player"){
    		audioSource.clip = explosionAudioClip;
    		Player player = other.transform.GetComponent<Player>();
    		if(player != null){
    			player.damage();
    		}
    		enemyAnimatorComponent.SetTrigger("onEnemyDeath");
    		_speed = 0;
    		audioSource.Play();
    		Destroy(this.gameObject , 2.5f);
    	}
    }

}
