using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwipeToStart : MonoBehaviour
{
    private Vector2 mouseDownPosition;
    private Vector2 mouseUpPosition;
    public float minSwipeDistance = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            mouseUpPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpPosition = Input.mousePosition;
            CheckSwipe();
        }
    }

    void CheckSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsSwipeRight())
            {
                Debug.Log("Swipe Right");
                NextLevel();
            }
        }
    }

    bool SwipeDistanceCheckMet()
    {
        return Vector2.Distance(mouseDownPosition, mouseUpPosition) > minSwipeDistance;
    }

    bool IsSwipeRight()
    {
        return mouseUpPosition.x > mouseDownPosition.x;
    }

    void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
