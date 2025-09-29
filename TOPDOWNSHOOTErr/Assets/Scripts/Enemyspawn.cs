using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float minSpawnTime = 1.0f;
    [SerializeField] float maxSpawnTime = 3.0f;

    float spawnDistance = 10f;
    Vector2 screenBounds;
    Vector2 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void spawnEnemy()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0:             //om den har värdet 0 gör detta
                spawnPos = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + spawnDistance);
                break;
            case 1:             //om den har värdet 1 gör detta
                spawnPos = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - spawnDistance);
                break;
            case 2:             //om den har värdet 2 gör detta
                spawnPos = new Vector2(screenBounds.x + spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 3:             //om den har värdet 3 gör detta
                spawnPos = new Vector2(-screenBounds.x - spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;

        }
        Instantiate(enemyPrefab, spawnPos, transform.rotation);
        Invoke("spawnEnemy", spawnTime);
    }
}
