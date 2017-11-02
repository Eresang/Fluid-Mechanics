using UnityEngine;

public enum Direction2D
{
	None = -1,
	Up = 0,
	Down = 1,
	Right = 2,
	Left = 3
}

public struct GridPosition
{
	public int x, z;

	public static Direction2D ShortestPath (GridPosition from, GridPosition to)
	{
		int dX = from.x - to.x;
		int dZ = from.z - to.z;

		if (Mathf.Abs (dX) > Mathf.Abs (dZ)) {
			if (dX < 0)
				return Direction2D.Right;
			else
				return Direction2D.Left;
		} else if (dZ != 0) {
			if (dZ < 0)
				return Direction2D.Up;
			else
				return Direction2D.Down;
		} else
			return Direction2D.None;
	}

	public static Direction2D LongerPath (GridPosition from, GridPosition to)
	{
		int dX = from.x - to.x;
		int dZ = from.z - to.z;

		if (Mathf.Abs (dX) > Mathf.Abs (dZ)) {
			if (dZ < 0)
				return Direction2D.Up;
			else
				return Direction2D.Down;
		} else if (dX != 0) {
			if (dX < 0)
				return Direction2D.Right;
			else
				return Direction2D.Left;
		} else
			return Direction2D.None;
	}

	public GridPosition (int positionX, int positionZ)
	{
		x = positionX;
		z = positionZ;
	}

	public static GridPosition Up ()
	{
		return new GridPosition (0, 1);
	}

	public static GridPosition Down ()
	{
		return new GridPosition (0, -1);
	}

	public static GridPosition Right ()
	{
		return new GridPosition (1, 0);
	}

	public static GridPosition Left ()
	{
		return new GridPosition (-1, 0);
	}

	public static GridPosition Zero ()
	{
		return new GridPosition (0, 0);
	}

	public static Vector3 ToVector3 (Direction2D direction)
	{
		GridPosition vector = Direction (direction);
		return new Vector3 (vector.x, 0.0f, vector.z);
	}

	public static GridPosition Direction (Direction2D direction)
	{
		switch (direction) {
		case Direction2D.Up:
			return Up ();

		case Direction2D.Down:
			return Down ();

		case Direction2D.Right:
			return Right ();

		case Direction2D.Left:
			return Left ();

		default:
			return Zero ();
		}
	}

	public static Direction2D Flip (Direction2D direction)
	{
		if (direction == Direction2D.None)
			return direction;

		int d = (int)direction;

		if (d == 1)
			return (Direction2D)d - 1;
		else
			return (Direction2D)d + 1;
	}
}
