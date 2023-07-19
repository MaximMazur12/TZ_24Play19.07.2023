using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float maxMovementRange = 2f;
    [SerializeField] private float horizontalMovementSpeed;
    [SerializeField] private float horizontalLimitValue;
    private float horizontalValue;

    private bool isGameOver = false;
    public GameObject Canvas;

    private void Update()
    {
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);

        HandleHeroHorizontalInput();
        SetHeroHorizontalMovement();
    }

    private void HandleHeroHorizontalInput()
    {
        if (Input.GetMouseButton(0))
        {
            horizontalValue = Input.GetAxis("Mouse X");
        }
        else
        {
            horizontalValue = 0;
        }
    }

    private void SetHeroHorizontalMovement()
    {
            float newPositionX = transform.position.x + horizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
            newPositionX = Mathf.Clamp(newPositionX, -horizontalLimitValue, horizontalLimitValue);
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            ShowGameOverCanvas();         
            StartCoroutine(PauseAfterDelay(3f));
        }
    }

   private void ShowGameOverCanvas()
    {
        if (Canvas != null)
        {
            Canvas.SetActive(true);
        }
    }

    private IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        movementSpeed = 0f;
        horizontalMovementSpeed = 0f;   
        Time.timeScale = 0f;
    }

    public void RestartLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);
    }
   
}
