using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {

    private SpriteRenderer SR;

	// Use this for initialization
	void Start () {
        SR = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 10 == 0)
        {
            if (SR.color.a == 1)
            {
                SR.color = new Color(1, 1, 1, 0);
            }
            else
            {
                SR.color = new Color(1, 1, 1, 1);
            }
        }
        GameObject.Destroy(this.gameObject,1f);
	}
}
