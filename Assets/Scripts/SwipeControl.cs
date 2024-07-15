using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    private float[] pos;
    private bool isDragging = false;
    private int currentIndex = 0;

    void Start()
    {
        // Initialize positions
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        HandleInput();

        if (!isDragging)
        {
            SnapToPosition();
        }
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }

            if (isDragging)
            {
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
        }
        else
        {
            // For mouse input (editor testing)
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
        }
    }

    private void SnapToPosition()
    {
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.2f);
                currentIndex = i; // Update the current index
            }
        }
    }

    public void Next()
    {
        if (currentIndex < pos.Length - 1)
        {
            currentIndex++;
            StartCoroutine(SmoothScrollTo(pos[currentIndex]));
        }
    }

    public void Previous()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(SmoothScrollTo(pos[currentIndex]));
        }
    }

    private IEnumerator SmoothScrollTo(float targetPos)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // Adjust the duration as needed
        float startValue = scrollbar.GetComponent<Scrollbar>().value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(startValue, targetPos, elapsedTime / duration);
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value; // Update scroll_pos during the smooth scroll
            yield return null;
        }

        scrollbar.GetComponent<Scrollbar>().value = targetPos;
        scroll_pos = targetPos; // Ensure scroll_pos is set to the final target position
    }
}
