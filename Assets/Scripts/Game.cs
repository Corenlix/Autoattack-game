using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [SerializeField] private float _minSpawnPeriod;
    [SerializeField] private float _maxSpawnPeriod;
    [SerializeField] private Enemy _enemyPrefab;

    public static Game Instance => _instance;
    private static Game _instance;
    private float _remainTime;
    private EnemiesSpawner _enemiesSpawner;
    private List<Enemy> _enemies = new List<Enemy>();

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else _instance = this;

        _enemiesSpawner = new EnemiesSpawner(FindObjectOfType<Player>());
    }

    private void Update()
    {
        _remainTime -= Time.deltaTime;
        if (_remainTime > 0) return;

        Enemy spawnedEnemy = _enemiesSpawner.Spawn(_enemyPrefab);
        _enemies.Add(spawnedEnemy);
        _remainTime = Random.Range(_minSpawnPeriod, _maxSpawnPeriod);
        spawnedEnemy.Died += RemoveEnemyFromCollection;
    }

    private void RemoveEnemyFromCollection(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= RemoveEnemyFromCollection;
    }

    public Enemy GetNearestEnemy(Vector3 position)
    {
        if (_enemies.Count == 0) return null;
        float minDistance = Vector2.SqrMagnitude(_enemies[0].transform.position - position);
        Enemy nearestEnemy = _enemies[0];

        for (int i = 1; i < _enemies.Count; i++)
        {
            float distance =  Vector2.SqrMagnitude(_enemies[i].transform.position - position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = _enemies[i];
            }
        }

        return nearestEnemy;
    }
}
