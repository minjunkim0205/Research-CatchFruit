using UnityEngine;

public class itemGenerator : MonoBehaviour
{
    public GameObject bananaPrefab;
    public GameObject applePrefab;
    public GameObject bombPrefab;

    float spawnTime = 1.0f;
    float waitTime = 0;
    int chance = 2;
    int bananaSpawnLimit = 5;
    int bananaSpawnCount = 0;
    float speed = -0.03f;

    GameObject manager;

    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (!manager.GetComponent<GameManager>().GetIsGameOver())
        {
            waitTime += Time.deltaTime;
            if (waitTime >= spawnTime)
            {
                waitTime = 0;
                GameObject item;
                int dice = Random.Range(1, 11);
                if (dice < chance)
                {
                    item = Instantiate(bombPrefab);
                }
                else
                {
                    if (bananaSpawnCount >= bananaSpawnLimit)
                    {
                        item = Instantiate(bananaPrefab);
                        bananaSpawnCount = 0;
                    }
                    else
                    {
                        item = Instantiate(applePrefab);
                        bananaSpawnCount += 1;
                    }
                }
                float x = Random.Range(-1, 2);
                float z = Random.Range(-1, 2);
                item.transform.position = new Vector3(x, 4, z);
                item.GetComponent<ItemController>().dropSpeed = speed;
            }
        }
    }

    public void SetParamters(float spawn, float speed, int chance)
    {
        spawnTime = spawn;
        this.speed = speed;
        this.chance = chance;
    }
}
