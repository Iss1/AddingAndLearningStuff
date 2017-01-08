using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    public float moveSpeed;

    public List<Node> currentPath = null;

    // From this point I'm starting to draw line
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    public float drawLineSpeed = 10f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(currentPath != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.numPositions = currentPath.Count;
            int currNode = 0;
            while(currNode < currentPath.Count - 1 )
            {
                Vector3 start = map.TileCoordToWarldCoord(currentPath[currNode].x,currentPath[currNode].y);
                Vector3 end = map.TileCoordToWarldCoord(currentPath[currNode+1].x, currentPath[currNode+1].y);

                Debug.DrawLine(start, end, Color.black);
                currNode++;

                lineRenderer.SetPosition(0, transform.position);
                
                lineRenderer.SetPosition(currNode, end);
            }
        } else
            lineRenderer.enabled = false;
    }

    public void Move()
    {
        float reamainingMovement = moveSpeed;
        while (reamainingMovement > 0)
        {

            if (currentPath == null)
                return;

            reamainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

            tileX = currentPath[1].x;
            tileY = currentPath[1].y;
            transform.position = map.TileCoordToWarldCoord(tileX, tileY);

            currentPath.RemoveAt(0);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
            
        }

    }
}
