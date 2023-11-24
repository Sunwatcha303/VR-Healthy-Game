using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class LineLine : MonoBehaviour
{
    // Start is called before the first frame update
    internal LineRenderer lineRenderer;
    private bool isDrawing = false;
    private Vector3 prePos;
    private Vector3 startPos;
    public float minDistance = 0.001f;
    public Camera c;

    public DrawGameController gameController;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.getStart() && Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            lineRenderer.positionCount = 0;
            startPos = getMousePosition();
        }
        if (gameController.getStart() && Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
        if (isDrawing)
        {
            Vector3 curPos = getMousePosition();
            if (Vector3.Distance(curPos, prePos) > minDistance)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPos);
                prePos = curPos;
            }

            if (lineRenderer.positionCount > 1 && Vector3.Distance(startPos, curPos) < 0.05f)
            {
                EndGame();
            }
        }

    }

    void EndGame()
    {
        Debug.Log("End");
        isDrawing = false;
        gameController.SetFinishGame();
    }

    private Vector3 getMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Create a ray from the camera through the mouse position
        Ray ray = c.ScreenPointToRay(mousePosition);

        // Create a RaycastHit variable to store the hit information
        RaycastHit hit;

        // Perform a raycast to see if it hits any objects in the scene
        if (Physics.Raycast(ray, out hit))
        {
            // The hit.point contains the 3D position where the ray intersects with an object
            Vector3 hitPosition = hit.point;
            return hitPosition;
            // Output the hit position to the console (you can use this position for further actions)
        }
        return new Vector3();
    }
}
