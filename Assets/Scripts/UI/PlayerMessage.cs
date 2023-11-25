using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMessage : MonoBehaviour
{

    float messageTimeLength = 5;
    
    public GameObject messageContainer;
    TextMeshProUGUI texter;

    private void Awake()
    {
        texter = messageContainer.GetComponentInChildren<TextMeshProUGUI>();
        texter.text = "";
    }


    public void SetMessage(string message)
    {
        texter.DOKill(true);
        this.DOKill(false);
        
        texter.maxVisibleCharacters = 0;
        texter.text = message;
        texter.DOFade(1, 0);
        
        // TODO: Kill this when we interact again, Idk how to reference this tween properly but the method is DOKILL()
        DOTween.To(() => texter.maxVisibleCharacters, x => texter.maxVisibleCharacters = x, message.Length, 2).OnComplete(() => {
            texter.DOFade(0, 5);
        });

    }


}
