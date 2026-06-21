using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Endless procedural platform generator. Spawns new platform sections
/// ahead of the player as they move forward, and removes old sections
/// once they fall far enough behind the player.
/// </summary>
public class PlatformGenerator : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject[] platformPrefabs;

    [Header("Generation Settings")]
    public float sectionLength = 12f;
    public int sectionsAhead = 6;
    public float despawnBehindDistance = 25f;

    readonly Queue<GameObject> activeSections = new Queue<GameObject>();
    float nextSpawnZ = 0f;

    void Start()
    {
        nextSpawnZ = 0f;
        for (int i = 0; i < sectionsAhead; i++)
        {
            SpawnSection();
        }
    }

    void Update()
    {
        if (player == null) return;

        // Keep spawning sections ahead of the player.
        while (nextSpawnZ < player.position.z + sectionsAhead * sectionLength)
        {
            SpawnSection();
        }

        // Remove sections that have fallen far enough behind the player.
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

    void SpawnSection()
    {
        if (platformPrefabs == null || platformPrefabs.Length == 0) return;

        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        GameObject section = Instantiate(
            prefab,
            new Vector3(0f, 0f, nextSpawnZ),
            Quaternion.identity,
            transform
        );

        activeSections.Enqueue(section);
        nextSpawnZ += sectionLength;
    }
}
