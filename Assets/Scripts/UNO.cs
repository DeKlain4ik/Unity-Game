using UnityEngine;

public class UFOBoss : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;            // швидкість
    public float changeDirInterval = 1.5f;  // як часто змінюється напрямок
    public Vector2 moveBoundsMin;           // мінімальні координати
    public Vector2 moveBoundsMax;           // максимальні координати

    private Vector2 targetDirection;
    private float dirTimer = 0f;

    [Header("Enemy Spawning")]
    public GameObject robotPrefab;      // префаб робота
    public Transform spawnPoint;        // точка спавну ворога
    public float spawnInterval = 3f;    // як часто створювати ворогів

    private float spawnTimer = 0f;

    void Start()
    {
        PickNewDirection();
    }

    void Update()
    {
        MoveUFO();
        SpawnRobots();
    }

    void MoveUFO()
    {
        dirTimer += Time.deltaTime;

        // оновлення випадкового напрямку
        if (dirTimer >= changeDirInterval)
        {
            PickNewDirection();
            dirTimer = 0f;
        }

        // рух об'єкта
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        // обмеження в межах арени
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, moveBoundsMin.x, moveBoundsMax.x);
        pos.y = Mathf.Clamp(pos.y, moveBoundsMin.y, moveBoundsMax.y);
        transform.position = pos;
    }

    void SpawnRobots()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            Instantiate(robotPrefab, spawnPoint.position, Quaternion.identity);
            spawnTimer = 0f;
        }
    }

    void PickNewDirection()
    {
        // випадковий рух ↑ ↓ ← →
        targetDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}