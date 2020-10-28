using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coordnaites
{
	[SerializeField] private int x;
	[SerializeField] private int z;
	public int X 
	{ 
		get
        {
			return x;
        }
	}

	public int Z 
	{ 
		get
        {
			return z;
		}
	}

	public int Y
	{
		get
		{
			return -X - Z;
		}
	}

	public Coordnaites(int x, int z)
	{
		this.x = x;
		this.z = z;
	}
	public static Coordnaites BadToGood(int x, int z)
	{
		return new Coordnaites(x - (z / 2), z);
	}
	public static Coordnaites FromPosition(Vector3 position)
	{
		float x = position.x / (MathH.innerRadius * 2f);
		float y = -x;

		float offset = position.z / (MathH.outerRadius * 3f);
		x -= offset;
		y -= offset;

		int iX = Mathf.RoundToInt(x);
		int iY = Mathf.RoundToInt(y);
		int iZ = Mathf.RoundToInt(-x - y);

		if (iX + iY + iZ != 0)
		{
			float dX = Mathf.Abs(x - iX);
			float dY = Mathf.Abs(y - iY);
			float dZ = Mathf.Abs(-x - y - iZ);

			if (dX > dY && dX > dZ)
			{
				iX = -iY - iZ;
			}
			else if (dZ > dY)
			{
				iZ = -iX - iY;
			}
		}
			return new Coordnaites(iX, iZ);

	}

	public override string ToString()
	{
		return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
	}
}
