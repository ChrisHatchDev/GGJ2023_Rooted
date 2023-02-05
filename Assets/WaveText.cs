using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    public TMP_Text Text;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        Text = GetComponent<TMP_Text>();
    }
    #endif

    void Update()
    {
        Text.text = $"Wave {EnemySpawner.CurrentWave}";
    }
}
