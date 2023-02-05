using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(Base))]
public class BaseEditor: Editor
{
    private void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Base _target = target as Base;
        GUILayout.Label($"Towers in Range (${_target.TowersInRange.Count})");
    }
}
#endif

public class Base : IPowerSource
{
    // 1-10. 10 Being full health
    public float MaxHealth = 1000;
    public float Health = 1000;
    public Dictionary<GameObject, Tower> TowersInRange = new Dictionary<GameObject, Tower>();
    public int attack_damage = 5;
    public Image HealthbarFill;
    public List<LineRenderer> ListOfTowerLines;

    public MeshRenderer PowerRangeVisuals;

    private void Start()
    {
        Health = MaxHealth;
    }

    public void Damage(int damage){
        Health -= damage;
    }
    public void Update(){
        if(this.Health <= 0){
            Debug.Log("it's dead jim");
            DestroyBase();
            return;
        }

        HealthbarFill.fillAmount = (Health / MaxHealth);
        foreach(var t in TowersInRange){
            Debug.DrawLine(transform.position, t.Value.transform.position, Color.green);
        }
    }

    private void DestroyBase()
    {
        MainUI.ShowGameOverScreen();
        Destroy(gameObject);
    }

    public void AddTowerInRange(Tower tower)
    {
        if (!TowersInRange.ContainsKey(tower.gameObject))
        {
            TowersInRange.TryAdd(tower.gameObject, tower);
        }
    }

    public void RemoveTowerInRange(Tower tower)
    {
        if (TowersInRange.ContainsKey(tower.gameObject))
        {
            TowersInRange.Remove(tower.gameObject);
        }
    }

    public void ShowPowerRangeVisuals()
    {
        PowerRangeVisuals.enabled = true;
    }

    public void HidePowerRangeVisuals()
    {
        PowerRangeVisuals.enabled = false;
    }

    public override PowerSourceType GetPowerType()
    {
        return PowerType;
    }
    public override bool HasValidPower(IPowerSource ps, List<IPowerSource> seen)
    {
        return true;
    }
}