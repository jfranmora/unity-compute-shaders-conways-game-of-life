using DG.Tweening;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Camera camera;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			camera.orthographicSize = .025f;

		if (Input.GetKeyDown(KeyCode.Return))
			camera.DOOrthoSize(5f, 20f).SetEase(Ease.InSine);
	}
}