using UnityEngine;

public class Dongle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {// x position follows mouse position with borders
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float leftBorder = -4.2f + transform.localScale.x / 2f;
        float rightBorder = 4.2f - transform.localScale.x / 2f;

        if (mousePos.x < leftBorder)
        {
            mousePos.x = leftBorder;
        }
        else if (mousePos.x > rightBorder)
        {
            mousePos.x = rightBorder;
        }
        mousePos.y = 8;
        mousePos.z = 0; // Set z to 0 to keep the object in the 2D plane
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);

    }
}
