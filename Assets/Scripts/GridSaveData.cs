using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridSaveData
{
    [field: SerializeField] public int width { get; set; }
    [field: SerializeField] public int height { get; set; }

    [field: SerializeField] public List<TileSaveData> tiles { get; set; }

    public GridSaveData()
    {
        width = 1;
        height = 1;

        tiles = new List<TileSaveData>();
    }

    public GridSaveData(GridH grid)
    {
        width = grid.width;
        height = grid.height;
        tiles = grid.GetTileSaveData();
    }

}
