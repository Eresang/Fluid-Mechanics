using UnityEngine;
using System.Collections;

public class ObjectBalloon : MonoBehaviour
{
	private SpriteRenderer balloon, symbol;
	private Animator animator;
	public Vector3 baseLocation;
	public bool connectUp, connectRight, centered;

	public void Orientate ()
	{
		balloon.flipY = connectUp;
		balloon.flipX = connectRight;

		Vector3 newPos = Vector3.zero;

		if (connectUp)
			newPos.y = -0.1875f;
		symbol.transform.localPosition = newPos;

		newPos.y = 0f;
		newPos.x = 0.04f;
		newPos.z = 0.46875f; 

		if (connectRight)
			newPos.x *= -1f;

		if (centered)
			newPos.x *= 0f;

		balloon.transform.localPosition = newPos + baseLocation;
	}

	void Awake ()
	{
		balloon = GetComponent<SpriteRenderer> ();

		Transform c = transform.GetChild (0);
		if (c != null) {
			symbol = c.GetComponent<SpriteRenderer> ();
			animator = c.GetComponent<Animator> ();
		}
	}

	void Start ()
	{
		if ((balloon == null) || (symbol == null) || (animator == null))
			Destroy (this.gameObject);
		else
			Orientate ();
	}

	public void SetSymbol (Sprite newSprite)
	{
		animator.enabled = false;
		symbol.sprite = newSprite;
	}

	public void SetAnimatedSymbol (RuntimeAnimatorController newSprite)
	{
		animator.enabled = true;
		animator.runtimeAnimatorController = newSprite;
	}
}
