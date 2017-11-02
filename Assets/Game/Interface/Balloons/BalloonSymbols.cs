using UnityEngine;
using System.Collections;

public class BalloonSymbols : MonoBehaviour
{
	private static GameObject balloonObject;
	public GameObject balloonPrefab;

	public static ObjectBalloon GiveBalloon (GridCell parent)
	{
		RemoveBalloon (parent);

		if (balloonObject != null)
			parent.balloon = (Instantiate (balloonObject) as GameObject).GetComponent<ObjectBalloon> ();

		if (parent.balloon) {
			parent.balloon.transform.parent = parent.grid.transform;
			parent.balloon.baseLocation = parent.localGridPosition;
		}

		return parent.balloon;
	}

	public static void RemoveBalloon (GridCell parent)
	{
		if (parent.balloon != null)
			Destroy (parent.balloon.gameObject);
	}

	public static void SetSymbol (string name, ObjectBalloon balloon)
	{
		Sprite n = GetSymbol (name);

		if (n != null) {
			balloon.SetSymbol (n);
		} else {
			RuntimeAnimatorController r = GetAnimatedSymbol (name);
			if (r != null)
				balloon.SetAnimatedSymbol (r);
		}
	}

	public static Sprite GetSymbol (string name)
	{
		return Resources.Load<Sprite> ("Symbols/Sprites/" + name);
	}

	public static RuntimeAnimatorController GetAnimatedSymbol (string name)
	{
		return Resources.Load<RuntimeAnimatorController> ("Symbols/Animated/" + name);
	}

	void Awake ()
	{
		balloonObject = balloonPrefab;
	}
}
