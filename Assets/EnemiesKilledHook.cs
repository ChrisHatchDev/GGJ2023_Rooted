using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesKilledHook : MonoBehaviour
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
        Text.text = $"Enemies Killed: {EnemySpawner.NumberOfEnemiesKilled}";
    }
}
