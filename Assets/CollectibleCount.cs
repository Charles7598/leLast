using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleCount : MonoBehaviour
{

    TMPro.TMP_Text text;

    int count;

    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }


    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    void OnCollectibleCollected()
    {
        text.text = (++count).ToString();
    }
   
}
