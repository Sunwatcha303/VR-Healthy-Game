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
            if (getMousePosition().HasValue)
            {
                isDrawing = true;
                lineRenderer.positionCount = 0;
                startPos = getMousePosition().Value;
            }
            else
            {

            }
        }
        if (gameController.getStart() && Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
        if (isDrawing)
        {
            if (getMousePosition().HasValue)
            {
                Vector3 curPos = getMousePosition().Value;
                if (Vector3.Distance(curPos, prePos) > minDistance)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPos);
                    prePos = curPos;
                }

                if (lineRenderer.positionCount > 10 && Vector3.Distance(startPos, curPos) < 0.05f)
                {
                    lineRenderer.loop = true;
                    EndGame();
                }
            }
            else
            {
                isDrawing = false;
            }

        }

    }

    void EndGame()
    {
        isDrawing = false;
        gameController.SetFinishGame(lineRenderer);
    }

    private Vector3? getMousePosition()
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
            if (hit.collider.CompareTag("Board"))
            {
                Vector3 hitPosition = hit.point;
                hitPosition.z -= 0.1f;
                return hitPosition;

                // Do something specific for objects with the "YourTag" tag
            }
            // Output the hit position to the console (you can use this position for further actions)
        }
        return null;
    }

    public void SetLinePositionCount(int n)
    {
        lineRenderer.positionCount = n;
    }

    public void SetLineRendererLoop(bool b)
    {
        lineRenderer.loop = b;
    }
}
