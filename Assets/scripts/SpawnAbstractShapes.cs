using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
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
        randomPos.y = player.position.y; //pour les garder au sol (enlever si je veux faire flotter des objets)

        GameObject obj = GetShapeFromPool();
        obj.transform.position = randomPos;
        obj.SetActive(true);

        //StartCoroutine(FadeOutAndReturn(obj, shapeLifetime));

        StartCoroutine(FadeInAndOut(obj, shapeLifetime));
    }

    GameObject GetShapeFromPool()
    {
        if (shapePool.Count > 0)
            return shapePool.Dequeue();

        // If pool runs out, optionally instantiate a new one (not ideal)
        return Instantiate(shapePrefab);
    }

    IEnumerator FadeOutAndReturn(GameObject obj, float duration)
    {
        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
        Material mat = mr.material; // Use instantiated copy

        Color originalColor = mat.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.color = originalColor; // Reset
        obj.SetActive(false);
        shapePool.Enqueue(obj);
    }

    IEnumerator FadeInAndOut(GameObject obj, float totalDuration)
{
    MeshRenderer mr = obj.GetComponent<MeshRenderer>();
    Material mat = mr.material; // Creates a unique material instance
    Color originalColor = mat.color;

    float fadeDuration = 1f; // time for fade in/out
    float visibleDuration = totalDuration - (fadeDuration * 2);

    // Fade In
    float t = 0f;
    while (t < fadeDuration)
    {
        float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
        mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        t += Time.deltaTime;
        yield return null;
    }
    mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

    // Stay visible
    yield return new WaitForSeconds(visibleDuration);

    // Fade Out
    t = 0f;
    while (t < fadeDuration)
    {
        float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
        mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        t += Time.deltaTime;
        yield return null;
    }

    mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    obj.SetActive(false);
    shapePool.Enqueue(obj);
}

}
