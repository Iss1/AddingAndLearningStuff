using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

    public int mapSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSelection();
        DrawMap();
	}

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("MapPlane")))
        {
            Debug.Log(hit.point);
        }
    }

    private void DrawMap()
    {
        Vector2 widthLine = Vector2.right * 8;
        Vector2 heigthLine = Vector2.up * 8;

        for(int i = 0; i <= mapSize; i++)
        {
            Vector2 start = Vector2.up * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= mapSize; j++)
            {
                start = Vector2.right * j;
                Debug.DrawLine(start, start + heigthLine);
            }
        }
    }
}
