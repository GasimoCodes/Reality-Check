using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    void Start()
    {
        ScoreManager.Instance.registerStar(this.gameObject);
    }

    public void Collect()
    {
        ScoreManager.Instance.collectStar(this.gameObject);

    }
}
