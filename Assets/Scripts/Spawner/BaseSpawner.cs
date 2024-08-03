using UnityEngine;
using UnityEngine.AI;

public class BaseSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public static int maxEnemy = 15;
    public static int currentEnemy = 0;

    public float timeSpawn = 2f;
    private float timer;

    public float distance = 50;

    private void Start()
    {
        timer = timeSpawn;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeSpawn;
            if (currentEnemy < maxEnemy)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-distance, distance), 0, Random.Range(-distance, distance));
                Vector3 randomPosition = transform.position + randomOffset;
                NavMeshHit hit;

                // ѕровер€ем, попадает ли случайна€ позици€ на NavMesh
                if (NavMesh.SamplePosition(randomPosition, out hit, 5.0f, NavMesh.AllAreas))
                {
                    var bot = Instantiate(enemyPrefab, hit.position, Quaternion.identity, transform);
                    bot.transform.parent = null;
                    currentEnemy++;
                } else
                {
                    timer = -1;
                }
            }
        }
    }
}
