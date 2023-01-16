using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnManager : MonoBehaviour
{
    [Header("Core Properties")]
    [SerializeField] private GameObject _coinEndless;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _potion;
    [SerializeField] private float _radius = 1f;

    [Header("Coin Properties")]
    [SerializeField] private float _nextRateCoins = 12f;
    [SerializeField] private float _nextSpawnCoins = 0f;

    [Header("Spawn Properties")]
    [SerializeField] private float _spawnRateEnemy = 20f;
    [SerializeField] private float _nextSpawnEnemy = 0f;

    [Header("Potion Properties")]
    [SerializeField] private float _spawnRatePotion = 25f;
    [SerializeField] private float _nextSpawnPotion = 0f;

    private void Update()
    {
        if(Time.time > _nextSpawnCoins)
        {
            _nextSpawnCoins = Time.time + _nextRateCoins;
            SpawnCoinAtRandom();
            //Debug.Log("COIN SPAWNED");
        }

        if(Time.time > _nextSpawnEnemy)
        {
            _nextSpawnEnemy = Time.time + _spawnRateEnemy;
            SpawnEnemyAtRandom();
            //Debug.Log("ENEMY SPAWNED");
        }

        if(Time.time > _nextSpawnPotion)
        {
            _nextSpawnPotion = Time.time + _spawnRatePotion;
            SpawnPotionAtRandom();
            //Debug.Log("POTION SPAWNED");
        }
    }

    private void SpawnCoinAtRandom()
    {
       Vector3 randomPos = Random.insideUnitCircle * _radius;
       Instantiate(_coinEndless, randomPos, Quaternion.identity);
    }

    private void SpawnEnemyAtRandom()
    {
       Vector3 randomPos = Random.insideUnitCircle * _radius;
       Instantiate(_enemy, randomPos, Quaternion.identity);
    }

    private void SpawnPotionAtRandom()
    {
       Vector3 randomPos = Random.insideUnitCircle * _radius;
       Instantiate(_potion, randomPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}

