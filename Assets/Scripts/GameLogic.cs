using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject diamondPrefub;
    [SerializeField] TextMeshProUGUI stageIndicator;
    [SerializeField] GameObject gameOverScreen;
    public Transform northEastBound;
    public Transform southWestBound;
    public Transform center;
    public Transform cam;
    public Transform player;
    public float minDistance;
    public int stage;
    public int ballsOnStage;
    public int diamondOnStage;
    public bool spawnedAll,spawnedDiamond;
    public float launchingPower;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        spawnedAll = false;
        spawnedDiamond = false;
        stage = 1;
        ballsOnStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        stageIndicator.text = stage.ToString();
        if (stage % 2 == 0 && stage >= 2)
        {
            while (diamondOnStage <= stage / 2 && !spawnedDiamond)
            {
                SpawnDiamond();
                if(diamondOnStage == stage / 2)
                {
                    spawnedDiamond = true;
                }
            }
        }
    }

    void SpawnDiamond()
    {
        float x = Random.Range(southWestBound.position.x, northEastBound.position.x);
        float z = Random.Range(southWestBound.position.x, northEastBound.position.x);
        float y = northEastBound.position.y + 1;
        Vector3 spawnPos = new Vector3(x, y, z);
        if (Vector3.Distance(spawnPos, player.position) < minDistance)
        {
            spawnPos += (minDistance - Vector3.Distance(spawnPos, player.position)) * (spawnPos - player.position).normalized;
        }
        GameObject newDiamond = Instantiate(diamondPrefub, spawnPos, Quaternion.identity);
        diamondOnStage++;
    }

    public void DiamondUsed()
    {

    }

    public void BallDropped()
    {
        ballsOnStage--;
        if(ballsOnStage == 0)
        {
            spawnedAll = false;
            stage++;
        }
    }

    public void GameOver()
    {
        
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }


    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Debug.Log("Quit");
        Application.Quit();
    }
}
