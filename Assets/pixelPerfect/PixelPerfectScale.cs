using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfectScale : MonoBehaviour
{
	public int pixelsPerUnit = 32;

	public bool preferUncropped = true;

	public Camera mainCamera;
	public RenderTexture virtScreen;
	public Material virtScreenMat;

	private float screenPixelsY = 0;
	
	private bool currentCropped = false;

	void Start() {
		int screenVerticalPixels = Mathf.RoundToInt(mainCamera.orthographicSize * 2 * pixelsPerUnit);
		//screenVerticalPixels = Screen.height;
		redoRenderTexture (screenVerticalPixels);
		redoScale (screenVerticalPixels);
	}

	void Update()
	{
		int screenVerticalPixels = Mathf.RoundToInt(mainCamera.orthographicSize * 2 * pixelsPerUnit);
		//screenVerticalPixels = Screen.height;

		if(screenPixelsY != (float)Screen.height || currentCropped != preferUncropped)
		{
			redoScale (screenVerticalPixels);
		}

		if (mainCamera.targetTexture == null || screenVerticalPixels != virtScreen.height) {
			redoRenderTexture (screenVerticalPixels);
			redoScale (screenVerticalPixels);
		}
	}

	void redoRenderTexture(int screenVerticalPixels) {
		if (mainCamera.targetTexture != null) {
			mainCamera.targetTexture.Release ();
			//Debug.Log ("deleted old virtual screen");
		}
		virtScreen = new RenderTexture (screenVerticalPixels * 2, screenVerticalPixels, 24);
		virtScreen.filterMode = FilterMode.Point;

		mainCamera.targetTexture = virtScreen;
		virtScreenMat.SetTexture ("_MainTex", virtScreen);

		Debug.Log ("Generated new virtual screen with " + virtScreen.width + " * " + virtScreen.height + " dimensions");
	}

	void redoScale(int screenVerticalPixels) {
		screenPixelsY = (float)Screen.height;
		currentCropped = preferUncropped;

		float screenRatio = screenPixelsY/screenVerticalPixels;
		float ratio;

		if(preferUncropped)
		{
			ratio = Mathf.Floor(screenRatio)/screenRatio;
		}
		else
		{
			ratio = Mathf.Ceil(screenRatio)/screenRatio;
		}

		transform.localScale = Vector3.one*ratio;
		//transform.localScale = Vector3.one;
	}
}
