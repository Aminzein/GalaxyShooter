using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text scoreText;
	private Player player;
	[SerializeField]
	private Image livesImage;
	[SerializeField]
	private Sprite[] livesSprite;
	[SerializeField]
	private Text gameOverText;
	[SerializeField]
	private Text restartLevelText;
	private gameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
     	scoreText.text = "Score : " + 0; 
     	player = GameObject.Find("Player").GetComponent<Player>(); 
     	gameOverText.gameObject.SetActive(false);
     	restartLevelText.gameObject.SetActive(false);
     	gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
     	scoreText.text = "Score : " + player.getScore();   
    }

    public void updateLivesImage(int currentLives){
    	livesImage.sprite = livesSprite[currentLives];
    	if(currentLives <= 0){
    		gameOverText.gameObject.SetActive(true);
    		restartLevelText.gameObject.SetActive(true);
    		StartCoroutine(gameOverTextFlickerRoutine());
    		gameManager.gameOver();
    	}
    }

    IEnumerator gameOverTextFlickerRoutine(){
    	while(true){
    		gameOverText.text = "GAME OVER";
    		yield return new WaitForSeconds(0.5f);
    		gameOverText.text = "";
    		yield return new WaitForSeconds(0.5f);
    	}
    }
}
