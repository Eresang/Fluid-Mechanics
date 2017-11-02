using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class GridReader
{
	private GridManager Parent;

	public GridReader (GridManager owner)
	{
		Parent = owner;
	}

	private int TryParse (string text, int failureValue)
	{
		int r;

		if (int.TryParse (text, out r))
			return r;
		else
			return failureValue;
	}

	private int GetMode (string line)
	{
		if (line == "[define_objects]")
			return 1;
		else if (line == "[define_grid]")
			return 2;
		else if (line == "[object_locations]")
			return 3;
		else if (line == "")
			return -1;
		else
			return 0;
	}

	private void ReadGridSetting (string line)
	{
		string[] setting = ActBasics.SplitIntoArguments (line);

		if (setting.Length > 0) {
			if (setting [0] == "Size" && setting.Length > 2) {
				Parent.SetGridDimensions (TryParse (setting [1], 0), TryParse (setting [2], 0));
			} else if (setting [0] == "Name" && setting.Length > 1) {
				Parent.gridName = setting [1];
			} else if (setting [0] == "Background" && setting.Length > 1) {
				if (Parent.background != null)
					GameObject.Destroy (Parent.background);

				GameObject newBackground = Backgrounds.GetBackground (setting [1]);
				if (newBackground != null) {
					newBackground = GameObject.Instantiate (newBackground) as GameObject;
					newBackground.transform.parent = Parent.transform;
					Parent.background = newBackground;
				}

			} else if (setting [0] == "Column" && setting.Length > 2) {
				int column = TryParse (setting [1], -1);
				float height = (1f / Parent.depth) * ((float)TryParse (setting [2], 0) / (float)Parent.cellHeight);

				GridPosition p = new GridPosition (column, 0);
				for (int i = 0; i < Parent.depth; i++) {
					p.z = i;
					GridCell c = Parent.GetCell (p);

					if (c != null) {
						Vector3 v = c.localGridPosition;
						v.z -= height;
						c.localGridPosition = v;
					}
				}
			}
		}
	}

	private void ReadObjectLocations (string line)
	{
		string[] setting = ActBasics.SplitIntoArguments (line);

		if (setting.Length > 2) {
			string name = setting [0];

			for (int i = 1; i <= Mathf.FloorToInt ((setting.Length - 1) / 2); i++) {
				Parent.GridAddGridObject (Objects.GetObject (name),
					new GridPosition (TryParse (setting [i * 2 - 1], -1), TryParse (setting [i * 2], -1)));
			}
		}
	}

	public bool ReadGrid (string fileName)
	{
		if (Parent == null)
			return false;

		string line;

		if (File.Exists (fileName)) {
			StreamReader theReader = new StreamReader (fileName, Encoding.Default);
			using (theReader) {
				int mode = 0;
				int newMode;

				Objects.ClearCustomHash ();

				do {
					line = theReader.ReadLine ();

					if (line != null) {
						newMode = GetMode (line);

						if (newMode > 0)
							mode = newMode;
						else if (newMode == 0) {
							switch (mode) {
							case 1:
								Objects.AddStringToHash (line);
								break;

							case 2:
								ReadGridSetting (line);
								break;

							case 3:
								ReadObjectLocations (line);
								break;
							}
						}
					}
				} while (line != null); 

				theReader.Close ();
			}
		} else {
			TextAsset theGrid = Resources.Load ("Levels/" + fileName) as TextAsset;
			if (theGrid == null)
				return false;

			string[] lines = theGrid.text.Split (new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

			int i = 0;
			int mode = 0;
			int newMode;

			Objects.ClearCustomHash ();

			if (lines.Length > 0)
				do {
					line = lines [i];

					if (line != null) {
						newMode = GetMode (line);

						if (newMode > 0)
							mode = newMode;
						else if (newMode == 0) {
							switch (mode) {
							case 1:
								Objects.AddStringToHash (line);
								break;

							case 2:
								ReadGridSetting (line);
								break;

							case 3:
								ReadObjectLocations (line);
								break;
							}
						}
					}
					i++;
				} while (i < lines.Length); 
		}
		return true;
	}
}
