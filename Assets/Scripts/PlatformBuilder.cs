using UnityEngine;

public class PlatformBuilder : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject cubePrefab;
    public float spawnInterval = 4f;
    public float platformSize = 30f;
    public int minCubesPerPlatform = 5;
    public int maxCubesPerPlatform = 25;
    public float cubeOffset = 1f;
    public float maxVerticalOffset = 1f;

    private GameObject previousPlatform;

    public GameObject GetPreviousPlatform()
    {
        return previousPlatform;
    }

    private void Start()
    {
        InvokeRepeating("SpawnPlatform", 0f, spawnInterval);
    }

    private void SpawnPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab, transform.position, Quaternion.identity);

        if (previousPlatform != null)
        {
            Vector3 newPosition = previousPlatform.transform.position + Vector3.forward * platformSize;
            newPlatform.transform.position = newPosition;
            Destroy(previousPlatform, spawnInterval);
        }

        previousPlatform = newPlatform;
        StartCoroutine(MovePlatform(newPlatform.transform));
        GenerateCubes(newPlatform.transform);
    }

    private void GenerateCubes(Transform platformTransform)
    {
        int cubeCount = Random.Range(minCubesPerPlatform, maxCubesPerPlatform + 1);
        int numCubesX = Mathf.Min(cubeCount, 5);

        float platformWidth = numCubesX * cubeOffset;
        float startX = -platformWidth / 2f + cubeOffset / 2f;

        for (int i = 0; i < numCubesX; i++)
        {
            float posX = startX + i * cubeOffset;
            float posY = 0f;
            Vector3 spawnPosition = platformTransform.position + new Vector3(posX, posY, 0f);

            GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            cube.transform.SetParent(platformTransform);
        }

        for (int i = numCubesX; i < cubeCount; i++)
        {
            int posXIndex = (i - numCubesX) % numCubesX;
            int posYIndex = (i - numCubesX) / numCubesX + 1;

            float posX = startX + posXIndex * cubeOffset;
            float posY = posYIndex * cubeOffset;
            Vector3 spawnPosition = platformTransform.position + new Vector3(posX, posY, 0f);

            bool skipCube = Random.value < 0.5f && posYIndex > 1;

            if (!skipCube)
            {
                GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                cube.transform.SetParent(platformTransform);
            }
        }
    }

    private System.Collections.IEnumerator MovePlatform(Transform platformTransform)
    {
        Vector3 initialPosition = platformTransform.position;
        Vector3 targetPosition = initialPosition;
        targetPosition.y = -50f;

        platformTransform.position = targetPosition;

        float elapsedTime = 0f;
        float animationDuration = 0.5f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            platformTransform.position = Vector3.Lerp(targetPosition, initialPosition, t);
            yield return null;
        }
        platformTransform.position = initialPosition;
    }
}
