using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	private float _speed = 8;
	private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y >= 8){
        	if(transform.parent){
        		Destroy(transform.parent.gameObject);
        	}
        	Destroy(this.gameObject);
        }
    }
    public void addScoreToPlayer(){
    	player.addScore();
    }
}
