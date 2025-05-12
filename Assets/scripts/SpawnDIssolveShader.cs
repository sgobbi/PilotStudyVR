using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDissolveShader : MonoBehaviour
{
    public GameObject shapePrefab;
    public int poolSize = 10;
    public float spawnInterval = 2f;
    public float spawnRadius = 5f;
    public float shapeLifetime = 5f;
    public Transform player;

    private Queue<GameObject> shapePool = new Queue<GameObject>();

    void Start()
    {
        // Preload the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(shapePrefab);
            obj.SetActive(false);
            shapePool.Enqueue(obj);
        }

        // Start the spawn loop
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnShape();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnShape()
    {
        Vector3 randomPos = player.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = player.position.y;

        GameObject obj = GetShapeFromPool();
        obj.transform.position = randomPos;
        obj.SetActive(true);

        // Launch dissolve logic on the spawned object
        DissolveUsingShader[] dissolves = obj.GetComponentsInChildren<DissolveUsingShader>();

        if (dissolves.Length > 0)
        {
            StartCoroutine(DissolveAndReturn(obj, dissolves));
        }
    }

    GameObject GetShapeFromPool()
    {
        if (shapePool.Count > 0)
            return shapePool.Dequeue();

        return Instantiate(shapePrefab);
    }

    IEnumerator DissolveAndReturn(GameObject obj, DissolveUsingShader[] dissolveScripts)
    {
        // Start all dissolve coroutines and wait for the longest one
        List<Coroutine> dissolveCoroutines = new List<Coroutine>();

        foreach (var dissolve in dissolveScripts)
        {
            // Start the dissolve coroutine on each child
            yield return dissolve.Dissolver();  // If each Dissolver() handles full dissolve time, you can just yield each
        }

        // Optionally, wait a short buffer if needed

        obj.SetActive(false);
        shapePool.Enqueue(obj);
    }
}