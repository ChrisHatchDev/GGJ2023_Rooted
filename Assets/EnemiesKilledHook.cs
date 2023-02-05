using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesKilledHook : MonoBehaviour
{
    public TMP_Text Text;

    private void OnValidate()
    {
        Text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Text.text = $"Enemies Killed: {EnemySpawner.NumberOfEnemiesKilled}";
    }
}
