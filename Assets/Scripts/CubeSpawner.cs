using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float cubeDistance = 2f;
    public float spawnPositionOffsetZ = -15f;

    private PlatformBuilder platformBuilder;
    private GameObject previousPlatform;

    private void Awake()
    {
        platformBuilder = FindObjectOfType<PlatformBuilder>();
    }

    private void Update()
    {
        if (platformBuilder == null)
            return;
        GameObject currentPlatform = platformBuilder.GetPreviousPlatform();

        if (currentPlatform != null && currentPlatform != previousPlatform)
        {
            SpawnCubesOnPlatform(currentPlatform);
            previousPlatform = currentPlatform;
        }
    }

    private void SpawnCubesOnPlatform(GameObject platform)
    {
        float platformWidth = 5f;
        float minX = Mathf.Clamp(platform.transform.position.x - 2f, -platformWidth / 2f, platformWidth / 2f - cubeDistance);
        float maxX = Mathf.Clamp(platform.transform.position.x + 2f, -platformWidth / 2f + cubeDistance, platformWidth / 2f);

        for (int i = 0; i < 3; i++)
        {
            float posX = Random.Range(minX, maxX);
            float posY = -50f;
            float posZ = platform.transform.position.z + spawnPositionOffsetZ + i * cubeDistance * 2;

            Vector3 spawnPosition = new Vector3(posX, posY, posZ);
            GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

            StartCoroutine(MoveCube(cube.transform, new Vector3(posX, 0.5f, posZ)));
        }
    }

    private System.Collections.IEnumerator MoveCube(Transform cubeTransform, Vector3 targetPosition)
    {
        Vector3 initialPosition = cubeTransform.position;
        float animationDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            cubeTransform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }

        cubeTransform.position = targetPosition;
    }
}
