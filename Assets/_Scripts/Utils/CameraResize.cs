using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {
	void Start () {
        float cameraSize = 1;
        float bgSize = 1;
        float sWidth = Screen.width;
        float sHeight = Screen.height;
        float defaultScale = 480f / 800f;
        float sScale = sWidth / sHeight;
        if (sScale > defaultScale)
        {
            bgSize = sScale / defaultScale;
        }
        else if (sScale < defaultScale)
        {
            cameraSize = defaultScale / sScale;
            bgSize = cameraSize;
        }
        Camera.main.orthographicSize = cameraSize * Camera.main.orthographicSize;
	}
}
