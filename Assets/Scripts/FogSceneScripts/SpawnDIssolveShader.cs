using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnDissolveShader : MonoBehaviour
{
    public GameObject[] shapePrefabs;
    public int poolSize = 20;
    public float spawnInterval = 1f;
    public float spawnRadius = 5f;
    public Transform player;

    private Queue<GameObject> shapePool = new Queue<GameObject>();

    void Start()
    {
        // Preload the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shapePrefab = shapePrefabs[Random.Range(0,shapePrefabs.Length)];
            GameObject obj = Instantiate(shapePrefab);
            Debug.Log("spawned this object: " + shapePrefab); 
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
        Vector3 lookDirection = player.position - obj.transform.position;
        lookDirection.y = 0; // keep it horizontal
        if (lookDirection != Vector3.zero)
        obj.transform.rotation = Quaternion.LookRotation(lookDirection);
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

        return Instantiate(shapePrefabs[Random.Range(0, shapePrefabs.Length)]);
    }

    IEnumerator DissolveAndReturn(GameObject obj, DissolveUsingShader[] dissolveScripts)
    {
        List<Coroutine> runningCoroutines = new List<Coroutine>();

        // Start all dissolve coroutines in parallel
        foreach (var dissolve in dissolveScripts)
        {
            runningCoroutines.Add(StartCoroutine(dissolve.Dissolver()));
        }

        // Wait for all coroutines to finish
        foreach (var coroutine in runningCoroutines)
        {
            yield return coroutine;
        }   

        // Deactivate and return the object to the pool
        obj.SetActive(false);
        shapePool.Enqueue(obj);
    }
}