
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyBallPrefub;
    [SerializeField] GameLogic gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (gameLogic.ballsOnStage < gameLogic.stage && !gameLogic.spawnedAll)
        {
            SpawnEnemy();
            if(gameLogic.ballsOnStage == gameLogic.stage)
            {
                gameLogic.spawnedAll = true;
            }
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(gameLogic.southWestBound.position.x, gameLogic.northEastBound.position.x);
        float z = Random.Range(gameLogic.southWestBound.position.x, gameLogic.northEastBound.position.x);
        float y = gameLogic.northEastBound.position.y + 1;
        Vector3 spawnPos = new Vector3 (x, y, z);
        if(Vector3.Distance(spawnPos,gameLogic.player.position) < gameLogic.minDistance)
        {
            spawnPos += (gameLogic.minDistance - Vector3.Distance(spawnPos, gameLogic.player.position)) * (spawnPos - gameLogic.player.position).normalized;
        }
        EnemyMovement newEnemy = Instantiate(enemyBallPrefub,spawnPos,Quaternion.identity).GetComponent<EnemyMovement>();
        newEnemy.gameLogic = gameLogic;
        gameLogic.ballsOnStage++;
    }
}
