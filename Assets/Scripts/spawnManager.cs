using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] powerUp;
    private bool stopSpawning = false;
    void Start()
    {
        StartCoroutine(routineEnemySpawn());
        StartCoroutine(routineTripleShotSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator routineEnemySpawn(){
    	while(!stopSpawning){
    		Vector3 pos = new Vector3 (Random.Range(-9f , 9f) , 7 , 0);  
    		GameObject newEnemy = Instantiate(prefab , pos , Quaternion.identity);
    		newEnemy.transform.parent = enemyContainer.transform;
    		yield return new WaitForSeconds(5);  	
    	}
    }

    IEnumerator routineTripleShotSpawn(){
    	while(!stopSpawning){
    		float secondsToWait = Random.Range(3f,7f);
    		Vector3 position = new Vector3 (Random.Range(-8f , 8f) , 7 , 0);
    		int randomPowerUp = Random.Range(0,3);
    		GameObject newTripleShot = Instantiate(powerUp[randomPowerUp] , position , Quaternion.identity);
    		yield return new WaitForSeconds(secondsToWait);
    	}
    }
    public void playerIsDead(){
    	stopSpawning = true;
    }
}
