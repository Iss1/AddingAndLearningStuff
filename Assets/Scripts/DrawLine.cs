using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float drawLineSpeed = 10f;

    public TileMap map;
    public ClickableTile clickableTile;

	void Start () {
        origin = map.selectedUnit.transform;

	}
	
	
	void Update () {
        /*if (currentPath != null)
        {
            int currNode = 0;
            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWarldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCoordToWarldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y);

                Debug.DrawLine(start, end, Color.black);
                currNode++;
            }
        }*/
    }
}
