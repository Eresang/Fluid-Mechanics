using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class WaterMesher : MonoBehaviour
{
	public Shader shader;
	public Texture waterMix;

	public int divisions;
	public int tileScale = 1;

	private Material pMaterial;
	private MeshRenderer pRenderer;
	private float timer = 0f;
	public float timeRate = 1f;

	void Awake ()
	{
		pRenderer = GetComponent<MeshRenderer> ();
		pMaterial = new Material (shader);

		if (pMaterial != null) {
			pMaterial.hideFlags = HideFlags.HideAndDontSave;
			pMaterial.SetTexture ("_gsWaterMix", waterMix);
		}

		if (pRenderer != null)
			pRenderer.material = pMaterial;
	}

	void Update ()
	{
		timer = (timer + (Time.deltaTime * timeRate)) % (float)(Math.PI * 256f);

		if (pMaterial != null) {
			pMaterial.SetFloat ("_gsTime", timer);
			//pMaterial.SetTexture ("_gsWaterMix", waterMix);

			if (tileScale < 1 || divisions < 1 || transform.localScale.x < 1f || transform.localScale.z < 1f) {
				pMaterial.SetFloat ("_gsXsize", 1f);
				pMaterial.SetFloat ("_gsYsize", 1f);
			} else {
				pMaterial.SetFloat ("_gsXsize", transform.localScale.x * divisions / tileScale);
				pMaterial.SetFloat ("_gsYsize", transform.localScale.z * divisions / tileScale);
			}
		}
	}
}
