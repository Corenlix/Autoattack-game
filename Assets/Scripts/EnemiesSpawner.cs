using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private Rect _zoneAroundPlayerWithoutEnemies;
    private Player _player;
    
    public EnemiesSpawner(Player player)
    {
        _player = player;
        var camera = Camera.main;
        float visibleZoneHeight = 2f * camera.orthographicSize;
        float visibleZoneWidth = visibleZoneHeight * camera.aspect;
        _zoneAroundPlayerWithoutEnemies = new Rect(-visibleZoneWidth / 2 - 1, -visibleZoneHeight / 2 - 1, visibleZoneWidth + 2, visibleZoneHeight + 2);
    }

    public Enemy Spawn(Enemy enemyPrefab)
    {
        SpawnCorner corner = (SpawnCorner) Random.Range(0, 4);
        var spawnPosition = _player.transform.position;
        switch (corner)
        {
            case SpawnCorner.Up:
                spawnPosition.x += Random.Range(_zoneAroundPlayerWithoutEnemies.xMin,
                    _zoneAroundPlayerWithoutEnemies.xMax);
                spawnPosition.y += _zoneAroundPlayerWithoutEnemies.yMax;
                break;
            case SpawnCorner.Down:
                spawnPosition.x += Random.Range(_zoneAroundPlayerWithoutEnemies.xMin,
                    _zoneAroundPlayerWithoutEnemies.xMax);
                spawnPosition.y += _zoneAroundPlayerWithoutEnemies.yMin;
                break;
            case SpawnCorner.Left:
                spawnPosition.x += _zoneAroundPlayerWithoutEnemies.xMin;
                spawnPosition.y += Random.Range(_zoneAroundPlayerWithoutEnemies.yMin,
                    _zoneAroundPlayerWithoutEnemies.yMax);
                break;
            case SpawnCorner.Right:
                spawnPosition.x += _zoneAroundPlayerWithoutEnemies.xMax;
                spawnPosition.y += Random.Range(_zoneAroundPlayerWithoutEnemies.yMin,
                    _zoneAroundPlayerWithoutEnemies.yMax);
                break;
        }
        return GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private enum SpawnCorner
    { 
        Right,
        Up,
        Left,
        Down
    }
}
