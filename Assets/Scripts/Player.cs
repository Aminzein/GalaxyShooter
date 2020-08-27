using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.5f;
	[SerializeField]
	private float speedMult = 2.5f;
	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _tripleShotPrefab;
	[SerializeField]
	private float nextFire = 0.5f;
	private float canFire = -1f;
	[SerializeField]
	private int _lives = 3;
	private spawnManager _spawn;
	[SerializeField]
	private bool isTripleShotActived = false;
	[SerializeField]
	private bool isSpeedBoostActive = false;
	[SerializeField]
	private bool isShieldActive = false;
	[SerializeField]
	private GameObject shieldPrefab;
	[SerializeField]
	private int score = 0;
	private UIManager uiManager;
	[SerializeField]
	private GameObject rightEngineHurt , leftEngineHurt;
	[SerializeField]
	private AudioClip laserAudio;
	[SerializeField]
	private AudioSource playerAudioSource;
	[SerializeField]
	private AudioClip explosionAudioClip;
	[SerializeField]
	private AudioClip powerUpAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawn = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerAudioSource = GetComponent<AudioSource>();
        if(_spawn == null){
        	Debug.Log("spawn is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
    	calculateMovement();
    	if(Input.GetKeyDown(KeyCode.Space) && Time.time > canFire){
    		shootLaser();
    	}
    }
    void calculateMovement(){
    	float horizontelInput = Input.GetAxis("Horizontal");
    	float verticalInput = Input.GetAxis("Vertical");
    	Vector3 direction = new Vector3(horizontelInput , verticalInput , 0);
        transform.Translate(direction * _speed * Time.deltaTime);
    	if(transform.position.y >= 0){
    		transform.position = new Vector3(transform.position.x , 0 , 0);
    	}
    	else if(transform.position.y <= -3.8f){
    		transform.position = new Vector3(transform.position.x , -3.8f,0);
    	}
    	if(transform.position.x >= 11.3f){
    		transform.position = new Vector3 (-11.2f , transform.position.y , 0);
    	}
    	else if(transform.position.x <= -11.3f){
    		transform.position = new Vector3 (11.2f , transform.position.y , 0);
    	}
    }
    void shootLaser(){
    	canFire = Time.time + nextFire;
    	if(isTripleShotActived){
    		Vector3 laserPosition = new Vector3 (transform.position.x-0.9f , transform.position.y+1, 0);
    		Instantiate(_tripleShotPrefab , laserPosition , Quaternion.identity);
    	}
    	else{
    	Vector3 laserPosition = new Vector3 (transform.position.x , transform.position.y + 1.05f , 0);
    	Instantiate(_laserPrefab , laserPosition , Quaternion.identity);
    	}
    	playerAudioSource.clip = laserAudio;
    	playerAudioSource.Play();
    }
    public void damage(){
    	if(isShieldActive){
    		shieldPrefab.SetActive(false);
    		isShieldActive = false;
    		return;
    	}
    	else{
    	_lives -= 1;
    	uiManager.updateLivesImage(_lives);
    	if(_lives == 2){
    		rightEngineHurt.SetActive(true);
    	}
    	else if(_lives == 1){
    		leftEngineHurt.SetActive(true);
    	}
    	else if(_lives <= 0){
    		playerAudioSource.clip = explosionAudioClip;
    		playerAudioSource.Play();
    		_spawn.playerIsDead();
    		Destroy(this.gameObject);
    	}
    }
    }
    public void collectTripleShot(){
    	playPowerUpAudioClips();
    	isTripleShotActived = true;
  		StartCoroutine(tripleShotPowerDownRoutine());
    }

    IEnumerator tripleShotPowerDownRoutine(){
    	while(isTripleShotActived){
    		yield return new WaitForSeconds(5);
    		isTripleShotActived = false;
    	}
    }
    public void speedPowerUp(){
    	playPowerUpAudioClips();
    	isSpeedBoostActive = true;
    	_speed = _speed * speedMult;
    	StartCoroutine(speedPowerUpRoutine());
    }
    IEnumerator speedPowerUpRoutine(){
    	yield return new WaitForSeconds(5);
    	_speed = 3.5f;
    	isSpeedBoostActive = false;
    }
    public void activateShield(){
    	playPowerUpAudioClips();
    	isShieldActive = true;
    	shieldPrefab.SetActive(true);
    }
    public void addScore(){
    	score += 10;
    }
    public int getScore(){
    	return score;
    }
    public int getLives(){
    	return _lives;
    }

    private void playPowerUpAudioClips(){
    	playerAudioSource.clip = powerUpAudioClip;
    	playerAudioSource.Play();
    }
}
