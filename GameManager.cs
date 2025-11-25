using UnityEngine; 
using UnityEngine.Events; 

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; private set; }

    public int totalEnemies = 0;
    public static int EnemyCount { get { return Manager ? Manager.totalEnemies : 0; } }

    [Space(4)]
    public bool active = false;
    public int minimumWaveSize = 6;
    public int additionalEnemiesPerWave = 3;
    public float spawnMinInterval = 2f;
    public float spawnMaxInterval = 5f;
    public float breakTime = 10f;

    [Space(4)]
    public int currentWave = 0;
    public int remainingSpawns = 0;
    public float timeElapsed = 0;
    [SerializeField]
    private float timer = 0;

    public static UnityAction OnGameOver;
    public static void GameOver()
    {
        OnGameOver?.Invoke();


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Awake()
    {
        if (Manager) Destroy(Manager);
        Manager = this;
    }
    private void Start()
    {
        NextWave();
    }
    private void OnDestroy()
    {
        OnGameOver = null;
    }

    // Update is called once per frame
    void Update()
    {
        totalEnemies = Enemy.Count + remainingSpawns;
        if (!active) return;

        Clock(); // Manage enemies
        EndGame(); // Try to move to next wave if able
    } 
    private void EndGame()
    {
        if (totalEnemies <= 0) NextWave();
    }
    public void NextWave()
    {
        active = true;

        currentWave++;

        remainingSpawns = GetWaveSize(currentWave);
        timer = breakTime;
    }
    public int GetWaveSize(int wave)
    {
        return minimumWaveSize + additionalEnemiesPerWave * wave;
    }
    private void Clock()
    {
        timeElapsed += Time.deltaTime;

        if (timer > 0) timer -= Time.deltaTime;
        else if(remainingSpawns > 0)
        {
            SpawnEnemy();
            timer = Mathf.Lerp(spawnMinInterval, spawnMaxInterval, Enemy.Count / totalEnemies);
        }
    }
    public void SpawnEnemy()
    {
        bool success = EnemySpawner.SpawnEnemy();
        if (success) remainingSpawns--;

        Debug.Log("Attempted to spawn enemy with result: " + (success ? "SUCCESS" : "FAILURE"));
    }

}
