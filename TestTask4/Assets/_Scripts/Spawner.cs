using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform playerPoint;
    [SerializeField] private List<Transform> enemyPoints;
    [SerializeField] private TankAI tankEnemyExample;
    [SerializeField] private PlayerTank playerTankExample;
    
    private void Start()
    {
        Spawn();
    }
    private void Spawn()
    {
        PlayerTank playerTank = Instantiate(playerTankExample, playerPoint);
        playerTank.SetRespawnPosition(playerPoint);
        for (int i = 0; i < enemyPoints.Count; i++)
        {
            TankAI enemyTank = Instantiate(tankEnemyExample, enemyPoints[i]);
            enemyTank.SetRespawnPosition(enemyPoints[i]);
        }
    }

}
