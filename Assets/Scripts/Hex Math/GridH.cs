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

	[SerializeField] public Tile prefab;

	[HideInInspector] public Tile[] tiles;

	[SerializeField] public Text textPrefab;

	[HideInInspector] public Canvas canvas;
	[HideInInspector] public HexMesh hexMesh;

	[SerializeField] public Color defaultColor = Color.white;
	[SerializeField] public Color touchedColor = Color.magenta;

	[HideInInspector] public int testIndex;
	private Item CurrentItem = new Item();
	private Hint CurrentHint = new Hint();
	private Pin CurrentPin = new Pin();

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

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = (x + z * 0.5f - (z / 2)) * (MathH.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (MathH.outerRadius * 1.5f);

		Tile tile = tiles[i] = Instantiate<Tile>(prefab);
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

    void Start()
    {
        hexMesh.Triangulate(tiles);
    }

	public void EditCell(Vector3 position, Color color)
	{
		position = transform.InverseTransformPoint(position);
		Coordnaites coords = Coordnaites.FromPosition(position);

		int index = coords.X + coords.Z * width + coords.Z / 2;

		EditCell(tiles[index], color);
	}

	public void EditCell(Tile tile, Color color)
	{
		tile.SetVisible(true);
		tile.color = color;

		hexMesh.Triangulate(tiles);
        switch (testIndex)
        {
            case 0:
                tile.AddItem(CurrentItem);
				Debug.Log(CurrentItem.Name);
                break;
            case 1:
                tile.AddHint(CurrentHint);
				Debug.Log(CurrentHint);
				break;
            case 2:
                tile.AddPin(CurrentPin);
				Debug.Log(CurrentPin);
				break;
            case 3:
                tile.Clear();
                break;
        }

        //Debug.Log(JsonUtility.ToJson(tiles[0]));

        //Debug.Log("touched at " + tile.coordinates.ToString());
    }

	public void SetName(Text name)
    {
		CurrentItem.Name = name.text;
    }

	public void SetValue(Text value)
	{
		CurrentItem.Value = Int32.Parse(value.text);
	}

	public void SetTL(Text tl)
	{
		CurrentItem.TL = float.Parse(tl.text);
	}

	public void SetQuantity(Text quantiti)
	{
		CurrentItem.Quantity = float.Parse(quantiti.text);
	}

	public void SetHintTitle(Text title)
    {
		CurrentHint.Title = title.text;
	}

	public void SetHintMessage(Text message)
	{
		CurrentHint.Messgae = message.text;
	}

	public void SetPinTitle(Text title)
    {
		CurrentPin.Title = title.text;
	}

	public void SetPinMessage(Text message)
	{
		CurrentPin.Messgae = message.text;
	}

	public void EditCells(Vector3 position1, Vector3 position2, Color color)
    {
		position1 = transform.InverseTransformPoint(position1);
		position2 = transform.InverseTransformPoint(position2);
		Coordnaites coords1 = Coordnaites.FromPosition(position1);
		Coordnaites coords2 = Coordnaites.FromPosition(position2);

		int highX = (coords1.X < coords2.X ? coords2.X : coords1.X);
		int lowX = (coords1.X < coords2.X ? coords1.X : coords2.X);
		int highZ = (coords1.Z < coords2.Z ? coords2.Z : coords1.Z);
		int lowZ = (coords1.Z < coords2.Z ? coords1.Z : coords2.Z);

		foreach (Tile tile in tiles)
        {
			if (tile.coordinates.X <= highX && tile.coordinates.X >= lowX && tile.coordinates.Z <= highZ && tile.coordinates.Z >= lowZ )
            {
				EditCell(tile, color);
            }
        }

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

	public string ToJsonString()
    {
		string json = "{\"tiles\":["; //"{\"width\":" + width + ",\"height\":" + height + ",\"tiles\":[";

		for (int i = 0; i < tiles.Length; i++)
        {
			tiles[i].UpdateForSave();
			json += JsonUtility.ToJson(tiles[i]);
			if (i + 1 < tiles.Length)
            {
				json += ",";
            }
            else
            {
				json += "]";
			}
		}

		json += "}";

		return json;
    }

	public void Load(string json)
    {
		string tempString = json.Substring(10);
		List<int> coords = new List<int>();
		int brackets = 0;
		bool instring = false;

		for (int i = 0; i < tempString.Length; i++)
        {
			if (brackets == 0 && tempString[i] != ',')
            {
				coords.Add(i);
            }

			if (tempString[i] == '{' && !instring)
            {
				brackets++;
            }
			else if (tempString[i] == '}' && !instring)
            {
				brackets--;
			}
			else if (tempString[i] == '\"')
			{
				instring = !instring;
			}

		}

		Tile temp;

		for (int i = 0; i < coords.Count - 1; i++)
        {

			if (i == 0)
            {
				Debug.Log(tempString.Substring(coords[i], coords[i + 1] - coords[i] - 1));
				Debug.Log(JsonUtility.ToJson(tiles[i]));
				temp = JsonUtility.FromJson<Tile>(tempString.Substring(coords[i], coords[i + 1] - coords[i] - 1));
				tiles[i] = temp;

			}
            else
            {
				tiles[i] = JsonUtility.FromJson<Tile>(tempString.Substring(coords[i] + 2, coords[i + 1] - (coords[i] + 1)));
			}
			
        }

    }

}
