using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private Tile[,] tiles;

    public Tile[,] Tiles
    {
        get { return tiles; }
        set { tiles = value; }
    }

    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public Map(int height, int width, string titl)
    {
        tiles = new Tile[height,width];
        title = titl;
    }


}
