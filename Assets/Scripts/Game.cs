using UnityEngine;

public class Game : MonoBehaviour
{
    [FloatRangeSlider(0, 2f)]
    [SerializeField] private FloatRange _spawnPeriod;
    [SerializeField] private Enemy _enemyPrefab;

    public static Game Instance => _instance;
    private static Game _instance;
    public Player CurrentPlayer => _currentPlayer;
    private Player _currentPlayer;
    private Enemies _enemies;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        _currentPlayer = FindObjectOfType<Player>();
        _enemies = new Enemies(_spawnPeriod, _enemyPrefab);
    }

    private void Update()
    {
        _enemies.Update();
    }

    public Enemy GetNearestEnemy(Vector3 position) => _enemies.GetNearestEnemy(position);
}
