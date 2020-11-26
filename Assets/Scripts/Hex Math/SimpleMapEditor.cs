using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleMapEditor : MonoBehaviour
{
	[SerializeField] public GameObject ItemMenuPrefab;
	[SerializeField] public GameObject ItemMenu;
	[SerializeField] public GameObject HintMenuPrefab;
	[SerializeField] public GameObject HintMenu;
	[SerializeField] public GameObject PinMenuPrefab;
	[SerializeField] public GameObject PinMenu;
	[SerializeField] public GameObject CharacterMenuPrefab;
	[SerializeField] public GameObject CharacterMenu;
	[SerializeField] public GameObject ActionMenu;
	[SerializeField] public Tile MultiTile;
	[SerializeField] public Player player;
	public Color[] colors;

	public GridH grid;

	private Color activeColor;

	public Tile SelectedTile;
	private RaycastHit point1;
	private RaycastHit point2;
	private bool down = false;
	private int on = 0;
	private bool swotch = true;
	private bool anitLine = false;
	//private CharacterSheet movingBoi;
	private List<Tile> SelectedTiles = new List<Tile>();

	private Vector3[] corners = new Vector3[4];

	private Camera mainCamera;

	[SerializeField] LineRenderer LR;

	void Awake()
	{
		SelectColor(0);
		LR = GetComponent<LineRenderer>();
		mainCamera = Camera.main;
	}

	void Update()
	{

		if (Input.GetMouseButtonDown(2) && !EventSystem.current.IsPointerOverGameObject())
        {
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				grid.ShowToolTip(hit.point);
			}
		}

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			HandleInput();
		}
		if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
			LR.enabled = true;
			anitLine = false;
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				point1 = hit;
				down = true;
			}
			
		}
		if (Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject() && down)
        {
			down = false;
			anitLine = true;
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				MultiTile.Clear();
				SelectedTile = MultiTile;
				UpdateMenu();
				point2 = hit;
				SelectedTiles.Clear();

				//Vector3 position1 = transform.InverseTransformPoint(point1.point);
				//Vector3 position2 = transform.InverseTransformPoint(point2.point);
				Coordnaites coords1 = Coordnaites.FromPosition(point1.point);
				Coordnaites coords2 = Coordnaites.FromPosition(point2.point);

				int highX = (coords1.X < coords2.X ? coords2.X : coords1.X);
				int lowX = (coords1.X < coords2.X ? coords1.X : coords2.X);
				int highZ = (coords1.Z < coords2.Z ? coords2.Z : coords1.Z);
				int lowZ = (coords1.Z < coords2.Z ? coords1.Z : coords2.Z);

				foreach (Tile tile in grid.tiles)
				{
					if (tile.coordinates.X <= highX && tile.coordinates.X >= lowX && tile.coordinates.Z <= highZ && tile.coordinates.Z >= lowZ)
					{
						SelectedTiles.Add(tile);
					}
				}
				Debug.Log(SelectedTiles.Count());
			}
			point1 = new RaycastHit();
			
		}
		if(down)
        {
			// do line renderer stuff

			DrawHighlight();

		}

		if(anitLine)
        {

			if(swotch)
			{
				DrawBox(corners[0], corners[1], corners[2], corners[3]);
				on++;
			}
			else
            {
				DrawBox(corners[1], corners[2], corners[3], corners[0]);
				on++;
			}
			if (on % 50 == 0)
            {
				swotch = !swotch;
            }

        }

	}

	private void DrawHighlight()
    {
		Ray inputRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			point2 = hit;
			//Vector3 position1 = transform.InverseTransformPoint(point1.point);
			//Vector3 position2 = transform.InverseTransformPoint(hit.point);
			Coordnaites coords1 = Coordnaites.FromPosition(point1.point);
			Coordnaites coords2 = Coordnaites.FromPosition(hit.point);


			int highX = (coords1.X < coords2.X ? coords2.X : coords1.X);
			int lowX = (coords1.X < coords2.X ? coords1.X : coords2.X);
			int highZ = (coords1.Z < coords2.Z ? coords2.Z : coords1.Z);
			int lowZ = (coords1.Z < coords2.Z ? coords1.Z : coords2.Z);
			//int highY = (coords1.Y < coords2.Y ? coords2.Y : coords1.Y);
			//int lowY = (coords1.Y < coords2.Y ? coords1.Y : coords2.Y);

			int indexH = highX + highZ * grid.width + highZ / 2;
			int index1 = lowX + highZ * grid.width + highZ / 2;
			int indexL = lowX + lowZ * grid.width + lowZ / 2;
			int index2 = highX + lowZ * grid.width + lowZ / 2;

			corners[0] = grid.tiles[index1].transform.position;
			corners[1] = grid.tiles[indexH].transform.position;
			corners[2] = grid.tiles[index2].transform.position;
			corners[3] = grid.tiles[indexL].transform.position;

			DrawBox(corners[0], corners[1], corners[2], corners[3]);
			//LR.SetPosition(4, grid.tiles[index1].transform.position);

			

		}
	}

	private void DrawBox(Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4)
    {
		LR.SetPosition(0, c1);
		LR.SetPosition(1, c2);
		LR.SetPosition(2, c3);
		LR.SetPosition(3, c4);
    }

	void HandleInput()
	{
		if (SelectedTile != null)
        {
			SelectedTile.color = SelectedTile.savedColor;
		}
		point1 = new RaycastHit();
		point2 = new RaycastHit();
		LR.enabled = false;
		anitLine = false;
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			Coordnaites coords = Coordnaites.FromPosition(hit.point);
			SelectedTile = grid.tiles[coords.X + coords.Z * grid.width + coords.Z / 2];
			SelectedTile.savedColor = SelectedTile.color;
			SelectedTile.color = new Color(0, 0, 0);
			UpdateMenu();
		}
		grid.hexMesh.Triangulate(grid.tiles);
	}

	public void AddItem()
    {
		if(SelectedTile.Multi)
        {
			SelectedTile.AddItem(new Item());

			foreach(Tile tile in SelectedTiles)
            {
				tile.AddItem(new Item());
			}
		}
        else
        {
			SelectedTile.AddItem(new Item());
        }
		UpdateMenu();
	}

	public void AddHint()
	{
		if (SelectedTile.Multi)
		{
			SelectedTile.AddHint(new Hint());

			foreach (Tile tile in SelectedTiles)
			{
				tile.AddHint(new Hint());
			}
		}
		else
		{
			SelectedTile.AddHint(new Hint());
		}
		UpdateMenu();
	}

	public void AddPin()
	{
		if (SelectedTile.Multi)
		{
			SelectedTile.AddPin(new Pin());

			foreach (Tile tile in SelectedTiles)
			{
				tile.AddPin(new Pin());
			}
		}
		else
		{
			SelectedTile.AddPin(new Pin());
		}
		UpdateMenu();
	}

	public void SetColor(Color color)
    {
		SelectedTile.color = color;
		SelectedTile.savedColor = color;
		if(SelectedTile.Multi)
        {
			foreach (Tile tile in SelectedTiles)
            {
				tile.color = color;
				tile.savedColor = color;
            }
        }
		grid.hexMesh.Triangulate(grid.tiles);
		player.Send(JsonUtility.ToJson(grid.GetSaveData()));
    }

	public void GetColor(ColorPicker picker)
    {
		//picker.NotifyColor(SelectedTile.color);
    }

	public void AddCharacter()
	{
		if (SelectedTile.Multi)
		{
			SelectedTile.AddOccupant(new CharacterSheet());

			foreach (Tile tile in SelectedTiles)
			{
				tile.AddOccupant(new CharacterSheet());
			}
		}
		else
		{
			SelectedTile.AddOccupant(new CharacterSheet());
		}
		UpdateMenu();
	}

	//public void Move()
 //   {
	//	movingBoi = SelectedTile.GetOccupants()[ActionMenu.GetComponentInChildren<Dropdown>().value];
	//	moving = true;
 //   }

	public void UpdateMenu()
    {
		// item update
		{ 
			foreach (Transform child in ItemMenu.transform)
			{
				if (child.gameObject.name != "Add Item Panel")
				{
					Destroy(child.gameObject);
				}
			}
			int count = 0;
			foreach (Item item in SelectedTile.GetItems())
			{
				GameObject newpannel = Instantiate(ItemMenuPrefab, ItemMenu.transform);
				List<InputField> feilds = newpannel.GetComponentsInChildren<InputField>().ToList<InputField>();
				feilds[0].text = item.Name;
				feilds[1].text = $"{item.Value}";
				feilds[2].text = $"{item.TL}";
				feilds[3].text = $"{item.Quantity}";
				feilds[0].onEndEdit.AddListener(item.SetName);
				feilds[1].onEndEdit.AddListener(item.SetValue);
				feilds[2].onEndEdit.AddListener(item.SetTL);
				feilds[3].onEndEdit.AddListener(item.SetQuantity);
				if (SelectedTile.Multi)
				{
					foreach (Tile tile in SelectedTiles)
					{
						feilds[0].onEndEdit.AddListener(tile.GetItems()[tile.GetItems().Count - ((SelectedTile.GetItems().Count - count))].SetName);
						feilds[1].onEndEdit.AddListener(tile.GetItems()[tile.GetItems().Count - ((SelectedTile.GetItems().Count - count))].SetValue);
						feilds[2].onEndEdit.AddListener(tile.GetItems()[tile.GetItems().Count - ((SelectedTile.GetItems().Count - count))].SetTL);
						feilds[3].onEndEdit.AddListener(tile.GetItems()[tile.GetItems().Count - ((SelectedTile.GetItems().Count - count))].SetQuantity);
					}
				}
				count++;
			}

			List<Text> texts = ItemMenu.GetComponentsInChildren<Text>().ToList<Text>();
			List<Text> justrealtexts = new List<Text>();
			foreach (Text text in texts)
			{
				if (text.name != "Text")
				{
					justrealtexts.Add(text);
				}
			}
		}

		// hint update
		{
			foreach (Transform child in HintMenu.transform)
			{
				if (child.gameObject.name != "Add Hint Panel")
				{
					Destroy(child.gameObject);
				}
			}
			int count = 0;
			foreach (Hint hint in SelectedTile.GetHints())
			{
				GameObject newpannel = Instantiate(HintMenuPrefab, HintMenu.transform);
				List<InputField> feilds = newpannel.GetComponentsInChildren<InputField>().ToList<InputField>();
				feilds[0].text = hint.Title;
				feilds[1].text = hint.Messgae;
				feilds[0].onEndEdit.AddListener(hint.SetTitle);
				feilds[1].onEndEdit.AddListener(hint.SetMessage);
				if (SelectedTile.Multi)
				{
					foreach (Tile tile in SelectedTiles)
					{
						feilds[0].onEndEdit.AddListener(tile.GetHints()[tile.GetHints().Count - ((SelectedTile.GetHints().Count - count))].SetTitle);
						feilds[1].onEndEdit.AddListener(tile.GetHints()[tile.GetHints().Count - ((SelectedTile.GetHints().Count - count))].SetMessage);
					}
				}
				count++;
			}

			List<Text> texts = HintMenu.GetComponentsInChildren<Text>().ToList<Text>();
			List<Text> justrealtexts = new List<Text>();
			foreach (Text text in texts)
			{
				if (text.name != "Text")
				{
					justrealtexts.Add(text);
				}
			}
		}

		// pin update
		{
			foreach (Transform child in PinMenu.transform)
			{
				if (child.gameObject.name != "Add Pin Panel")
				{
					Destroy(child.gameObject);
				}
			}
			int count = 0;
			foreach (Pin pin in SelectedTile.GetPins())
			{
				GameObject newpannel = Instantiate(PinMenuPrefab, PinMenu.transform);
				List<InputField> feilds = newpannel.GetComponentsInChildren<InputField>().ToList<InputField>();
				feilds[0].text = pin.Title;
				feilds[1].text = pin.Messgae;
				feilds[0].onEndEdit.AddListener(pin.SetTitle);
				feilds[1].onEndEdit.AddListener(pin.SetMessage);
				if (SelectedTile.Multi)
				{
					foreach (Tile tile in SelectedTiles)
					{
						feilds[0].onEndEdit.AddListener(tile.GetPins()[tile.GetPins().Count - ((SelectedTile.GetPins().Count - count))].SetTitle);
						feilds[1].onEndEdit.AddListener(tile.GetPins()[tile.GetPins().Count - ((SelectedTile.GetPins().Count - count))].SetMessage);
					}
				}
				count++;
			}

			List<Text> texts = PinMenu.GetComponentsInChildren<Text>().ToList<Text>();
			List<Text> justrealtexts = new List<Text>();
			foreach (Text text in texts)
			{
				if (text.name != "Text")
				{
					justrealtexts.Add(text);
				}
			}
		}

		// character update
		{
			foreach (Transform child in CharacterMenu.transform)
			{
				if (child.gameObject.name != "Add Character Panel")
				{
					Destroy(child.gameObject);
				}
			}
			//int count = 0;
			//foreach (CharacterSheet characterSheet in SelectedTile.GetOccupants())
			//{
			//	GameObject newpannel = Instantiate(CharacterMenuPrefab, CharacterMenu.transform);
			//	List<InputField> feilds = newpannel.GetComponentsInChildren<InputField>().ToList<InputField>();
			//	feilds[0].text = characterSheet.Name;
			//	feilds[1].text = $"{characterSheet.Speed}";
			//	feilds[0].onEndEdit.AddListener(characterSheet.SetName);
			//	feilds[1].onEndEdit.AddListener(characterSheet.SetSpeed);
			//	if (SelectedTile.Multi)
			//	{
			//		foreach (Tile tile in SelectedTiles)
			//		{
			//			feilds[0].onEndEdit.AddListener(tile.GetOccupants()[tile.GetOccupants().Count - ((SelectedTile.GetOccupants().Count - count))].SetName);
			//			feilds[1].onEndEdit.AddListener(tile.GetOccupants()[tile.GetOccupants().Count - ((SelectedTile.GetOccupants().Count - count))].SetSpeed);
			//		}
			//	}
			//	count++;
			//}

			List<Text> texts = CharacterMenu.GetComponentsInChildren<Text>().ToList<Text>();
			List<Text> justrealtexts = new List<Text>();
			foreach (Text text in texts)
			{
				if (text.name != "Text")
				{
					justrealtexts.Add(text);
				}
			}
		}

        // action update
        {
			Dropdown dropdown = ActionMenu.GetComponentInChildren<Dropdown>();
			dropdown.ClearOptions();
			List<Dropdown.OptionData> option = new List<Dropdown.OptionData>();
			foreach (CharacterSheet charactersheet in SelectedTile.GetOccupants())
            {
				Dropdown.OptionData data = new Dropdown.OptionData(charactersheet.Name);
				option.Add(data);
			}
			dropdown.AddOptions(option);
        }
	}

	public void SelectColor(int index)
	{
		grid.testIndex = index;
		activeColor = colors[index];
	}

}
