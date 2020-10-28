using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleMapEditor : MonoBehaviour
{

	public Color[] colors;

	public GridH grid;

	private Color activeColor;

	private RaycastHit point1;
	private RaycastHit point2;
	private bool down = false;
	private int on = 0;
	private bool swotch = true;
	private bool anitLine = false;

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
				grid.EditCells(point1.point, hit.point, activeColor);
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
				Debug.Log("on");
			}
			else
            {
				DrawBox(corners[1], corners[2], corners[3], corners[0]);
				on++;
				Debug.Log("off");
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
		point1 = new RaycastHit();
		point2 = point1;
		LR.enabled = false;
		anitLine = false;
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			grid.EditCell(hit.point, activeColor);
		}
	}

	public void SelectColor(int index)
	{
		grid.testIndex = index;
		activeColor = colors[index];
	}

	public void Save()
	{
		string path = "C:/Users/carso/Maps/test.map";
		string jsonString = grid.ToJsonString();
		Debug.Log(jsonString);

		System.IO.File.WriteAllText(path, jsonString);

	}

	public void Load()
	{
		string path = "C:/Users/carso/Maps/test.map";
		string json = System.IO.File.ReadAllText(path);
		Debug.Log(json);
		Debug.Log(grid.tiles[0].color);

		grid.Load(json);

		//Tile[] hopeful = new Tile[36];

		//JsonUtility.FromJsonOverwrite(json, grid.tiles);

		Debug.Log(grid.tiles[0].color);

	}

}
