using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] platformPrefabs;

    public float sectionLength = 12f;
    public int sectionsAhead = 6;
    public float despawnBehindDistance = 25f;

    readonly Queue<GameObject> activeSections = new Queue<GameObject>();
    float nextSpawnZ = 0f;
    int sectionsSpawned = 0;

    void Start()
    {
        nextSpawnZ = 0f;
        sectionsSpawned = 0;
        for (int i = 0; i < sectionsAhead; i++)
        {
            SpawnSection();
        }
    }

    void Update()
    {
        if (player == null) return;

        while (nextSpawnZ < player.position.z + sectionsAhead * sectionLength)
        {
            SpawnSection();
        }

        while (activeSections.Count > 0)
        {
            GameObject oldest = activeSections.Peek();
            if (oldest == null)
            {
                activeSections.Dequeue();
                continue;
            }
            if (oldest.transform.position.z < player.position.z - despawnBehindDistance)
            {
                activeSections.Dequeue();
                Destroy(oldest);
            }
            else
            {
                break;
            }
        }
    }

    GameObject FindSafePrefab()
    {
        foreach (GameObject prefab in platformPrefabs)
        {
            if (prefab != null && prefab.name.ToLower().Contains("straight"))
            {
                return prefab;
            }
        }
        return platformPrefabs.Length > 0 ? platformPrefabs[0] : null;
    }

    void SpawnSection()
    {
        if (platformPrefabs == null || platformPrefabs.Length == 0) return;

        GameObject prefab;
        if (sectionsSpawned < 2)
        {
            prefab = FindSafePrefab();
        }
        else
        {
            prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        }

        GameObject section = Instantiate(prefab, new Vector3(0f, 1f, nextSpawnZ), Quaternion.identity, transform);

        activeSections.Enqueue(section);
        nextSpawnZ += sectionLength;
        sectionsSpawned++;
    }
}
