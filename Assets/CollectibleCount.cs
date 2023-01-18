using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleCount : MonoBehaviour
{

    TMPro.TMP_Text text;

    int count;
    public GameObject pallet1;
    public GameObject pallet2;

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

    void Update()
    {
        if (count >= 15)
        {
            pallet1.SetActive(false);
            pallet2.SetActive(false);
            Debug.Log("apu les pallets");
            text.text = "Find the exit !";
        }
    }
}
