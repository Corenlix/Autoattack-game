using Entities;
using UnityEngine;

public class Game : MonoBehaviour
{
    [FloatRangeSlider(0, 2f)] [SerializeField]
    private FloatRange _spawnPeriod;

    [SerializeField] private Enemy _enemyPrefab;
    private Enemies _enemies;

    public static Game Instance { get; private set; }

    public Player CurrentPlayer { get; private set; }

    public Enemy RandomEnemy => _enemies.GetRandomEnemy();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentPlayer = FindObjectOfType<Player>();
        _enemies = new Enemies(_spawnPeriod, _enemyPrefab);
    }

    private void Update()
    {
        _enemies.Update();
    }

    public Enemy NearestEnemy(Vector3 position)
    {
        return _enemies.GetNearestEnemy(position);
    }
}