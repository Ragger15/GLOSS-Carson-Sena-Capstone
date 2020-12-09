using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GridH : MonoBehaviour
{
	[SerializeField] public int width = 10;
	[SerializeField] public int height = 10;

	[HideInInspector] public Tile[] tiles;

    [SerializeField] private Tile prefab;

    //[SerializeField] public Text textPrefab;

    [HideInInspector] public Canvas canvas;
	[HideInInspector] public HexMesh hexMesh;

	[SerializeField] public Color defaultColor = Color.white;
	[SerializeField] public Color touchedColor = Color.magenta;

	[HideInInspector] public int testIndex;

    public Tile Prefab { get => prefab; set => prefab = value; }

    void Awake()
	{
		canvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();
		tiles = new Tile[height * width];

		for(int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}

	public void ManualCreate()
    {
		for (int z = 0, i = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = (x + z * 0.5f - (z / 2)) * (MathH.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (MathH.outerRadius * 1.5f);

		Tile tile = tiles[i] = Instantiate<Tile>(Prefab);
		tile.transform.SetParent(transform, false);
		tile.transform.localPosition = position;
		tile.coordinates = Coordnaites.BadToGood(x, z);
		tile.color = defaultColor;
		tile.SetVisible(true);

		//Text label = Instantiate<Text>(textPrefab);
		//label.rectTransform.SetParent(canvas.transform, false);
		//label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		//label.text = tile.coordinates.ToString();

	}

	public void Load(GridSaveData saveData)
    {
		if (height == saveData.height && width == saveData.width)
        {
			for(int i = 0; i < tiles.Length; i++)
            {
				tiles[i].Load(saveData.tiles[i]);
            }
        }
		hexMesh.Triangulate(tiles);
	}

    void Start()
    {
        hexMesh.Triangulate(tiles);
    }

	public void ShowToolTip(Vector3 position)
    {
		position = transform.InverseTransformPoint(position);
		Coordnaites coords = Coordnaites.FromPosition(position);

		int index = coords.X + coords.Z * width + coords.Z / 2;

		Debug.Log("//////////// Items //////////");
		foreach(Item item in tiles[index].GetItems())
        {
			Debug.Log(item.Name);
        }

		Debug.Log("//////////// Hints //////////");
		foreach (Hint hint in tiles[index].GetHints())
		{
			Debug.Log(hint.Title);
		}

		Debug.Log("//////////// Pins //////////");
		foreach (Pin pin in tiles[index].GetPins())
		{
			Debug.Log(pin.Title);
		}
	}

	public List<TileSaveData> GetTileSaveData()
	{
		List<TileSaveData> tds = new List<TileSaveData>();

		for(int i = 0; i < tiles.Length; i++)
        {
			tds.Add(tiles[i].GetSaveData());
		}

		return tds;
	}

	public GridSaveData GetSaveData()
	{
		return new GridSaveData(this);
	}

}
