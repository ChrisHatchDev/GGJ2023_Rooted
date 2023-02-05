using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Base BaseInScene;
    public Image BaseHealthbarFill;

    void Update()
    {
        BaseHealthbarFill.fillAmount = BaseInScene.Health / BaseInScene.MaxHealth;
    }
}
