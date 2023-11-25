using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMessage : MonoBehaviour
{

    private static PlayerMessage instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        texter = messageContainer.GetComponentInChildren<TextMeshProUGUI>();
        texter.text = "";

    }

    public static PlayerMessage Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("PlayerMessage instance is null.");
            }
            return instance;
        }
    }

    float messageTimeLength = 5;
    
    public GameObject messageContainer;
    TextMeshProUGUI texter;



    public void SetMessage(string message)
    {
        texter.DOKill(true);
        this.DOKill(false);
        
        texter.maxVisibleCharacters = 0;
        texter.text = message;
        texter.DOFade(1, 0);
        
        // TODO: Kill this when we interact again, Idk how to reference this tween properly but the method is DOKILL()
        DOTween.To(() => texter.maxVisibleCharacters, x => texter.maxVisibleCharacters = x, message.Length, 1).OnComplete(() => {
            texter.DOFade(0, 5);
        });

    }


}
