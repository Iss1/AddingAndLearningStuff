﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;
    

    int[,] tiles;
    Node[,] graph;

    List<Node> currentPath = null;

    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start()
    {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;

        GenerateMap();
        GeneratePathfindingGraph();
        GenerateMapVisuals();
    }

    public void Move()
    {
        selectedUnit.GetComponent<Unit>().Move();
    }

    void GenerateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go =(GameObject) Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    void GenerateMap()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                tiles[x, y] = 0;
            }
        }

        for (int x = 3; x <=5; x++)
        {
            for (int y = 0; y <4; y++)
            {
                tiles[x, y] = 1;
            }
        }

        tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;

        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;

        tiles[0, 8] = 2;
        tiles[1, 8] = 2;
        tiles[1, 9] = 2;
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        TileType tt = tileTypes[tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;
        if (sourceX != targetX && sourceY != targetY)
        {
            if (sourceY < targetY && sourceX < targetX)
            {
                if (UnitCanEnterTile(targetX - 1, targetY) == false || UnitCanEnterTile(targetX, targetY - 1) == false)
                    return Mathf.Infinity;
            }
            if (sourceY < targetY && sourceX > targetX)
            {
                if (UnitCanEnterTile(targetX + 1, targetY) == false || UnitCanEnterTile(targetX, targetY - 1) == false)
                    return Mathf.Infinity;
            }

            if (sourceY > targetY && sourceX < targetX)
            {
                if (UnitCanEnterTile(targetX - 1, targetY) == false || UnitCanEnterTile(targetX, targetY + 1) == false)
                    return Mathf.Infinity;
            }
            if (sourceY > targetY && sourceX > targetX)
            {
                if (UnitCanEnterTile(targetX + 1, targetY) == false || UnitCanEnterTile(targetX, targetY + 1) == false)
                    return Mathf.Infinity;
            }
        }

        float cost = tt.movmentCost;

        if(sourceX!=targetX && sourceY != targetY)
        {
            cost += 0.001f;
            /*if (againCross == true)
            {
                cost *= 2;
                againCross = false;
            }
            else
                againCross = true;*/
        }

        return cost;
    }

    void GeneratePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                graph[x, y] = new Node();

                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                //this is the 4-way connection version
                /* if (x > 0)
                     graph[x, y].neighbours.Add(graph[x - 1, y]);
                 if(x < mapSizeX-1)
                     graph[x, y].neighbours.Add(graph[x + 1, y]);
                 if (y > 0)
                     graph[x, y].neighbours.Add(graph[x, y-1]);
                 if (y < mapSizeY - 1)
                     graph[x, y].neighbours.Add(graph[x, y+1]); */

                //This is 8-way connection version (allow diagonal movement)
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x-1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x-1, y + 1]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
            }
        }
    }

    public Vector3 TileCoordToWarldCoord(int x, int y)
    {

        return new Vector3(x, y, -1);
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        return tileTypes[tiles[x, y]].isWalkable;
    }

    public void GeneratePathTo(int x, int y)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if(UnitCanEnterTile(x,y) == false)
        {
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];
        Node target = graph[x,y];

        dist[source] = 0;
        prev[source] = null;

        foreach(Node v in graph)
        {
            if(v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while(unvisited.Count > 0)
        {
            Node u = null;

            foreach(Node possibleU in unvisited)
            {
                if (u == null|| dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
                break;

            unvisited.Remove(u);

            foreach(Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if(prev[target]==null) //jezeli nie ma drogi dostepu
        {
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node curr = target;
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
