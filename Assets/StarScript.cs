using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    void Start()
    {
        ScoreManager.Instance.registerStar(this.gameObject);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 6)
		{
			ScoreManager.Instance.collectStar(this.gameObject);
		}
	}
}
