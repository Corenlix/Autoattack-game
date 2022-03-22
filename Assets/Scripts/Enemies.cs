using System.Collections.Generic;
using Entities;
using UnityEngine;

public class Enemies
{
    private readonly List<Enemy> _enemies = new List<Enemy>();
    private readonly Enemy _enemyPrefab;
    private float _remainTime;
    private FloatRange _spawnPeriod;
    private Rect _zoneAroundPlayerWithoutEnemies;

    public Enemies(FloatRange spawnPeriod, Enemy enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
        _spawnPeriod = spawnPeriod;
        var camera = Camera.main;
        var visibleZoneHeight = 2f * camera.orthographicSize;
        var visibleZoneWidth = visibleZoneHeight * camera.aspect;
        _zoneAroundPlayerWithoutEnemies = new Rect(-visibleZoneWidth / 2 - 1, -visibleZoneHeight / 2 - 1,
            visibleZoneWidth + 2, visibleZoneHeight + 2);
    }

    public void Update()
    {
        _remainTime -= Time.deltaTime;
        if (_remainTime > 0) return;

        var spawnedEnemy = Spawn();
        _enemies.Add(spawnedEnemy);
        _remainTime = _spawnPeriod.RandomValueInRange;
        spawnedEnemy.Died += OnEnemyDied;
    }

    public Enemy GetNearestEnemy(Vector3 position)
    {
        if (_enemies.Count == 0) return null;
        var minDistance = Vector2.SqrMagnitude(_enemies[0].transform.position - position);
        var nearestEnemy = _enemies[0];

        for (var i = 1; i < _enemies.Count; i++)
        {
            var distance = Vector2.SqrMagnitude(_enemies[i].transform.position - position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = _enemies[i];
            }
        }

        return nearestEnemy;
    }

    public Enemy GetRandomEnemy()
    {
        return _enemies[Random.Range(0, _enemies.Count)];
    }

    private Enemy Spawn()
    {
        var side = (SpawnSide) Random.Range(0, 4);
        var spawnPosition = Game.Instance.CurrentPlayer.transform.position;
        switch (side)
        {
            case SpawnSide.Up:
                spawnPosition.x += Random.Range(_zoneAroundPlayerWithoutEnemies.xMin,
                    _zoneAroundPlayerWithoutEnemies.xMax);
                spawnPosition.y += _zoneAroundPlayerWithoutEnemies.yMax;
                break;
            case SpawnSide.Down:
                spawnPosition.x += Random.Range(_zoneAroundPlayerWithoutEnemies.xMin,
                    _zoneAroundPlayerWithoutEnemies.xMax);
                spawnPosition.y += _zoneAroundPlayerWithoutEnemies.yMin;
                break;
            case SpawnSide.Left:
                spawnPosition.x += _zoneAroundPlayerWithoutEnemies.xMin;
                spawnPosition.y += Random.Range(_zoneAroundPlayerWithoutEnemies.yMin,
                    _zoneAroundPlayerWithoutEnemies.yMax);
                break;
            case SpawnSide.Right:
                spawnPosition.x += _zoneAroundPlayerWithoutEnemies.xMax;
                spawnPosition.y += Random.Range(_zoneAroundPlayerWithoutEnemies.yMin,
                    _zoneAroundPlayerWithoutEnemies.yMax);
                break;
        }

        return Object.Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= OnEnemyDied;
    }

    private enum SpawnSide
    {
        Right,
        Up,
        Left,
        Down
    }
}