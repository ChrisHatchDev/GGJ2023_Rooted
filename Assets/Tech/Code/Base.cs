using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

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

public class Base : MonoBehaviour
{
    // 1-10. 10 Being full health
    public float MaxHealth = 1000;
    public float Health = 1000;
    public Dictionary<GameObject, Tower> TowersInRange = new Dictionary<GameObject, Tower>();
    public int attack_damage = 5;

    public Image HealthbarFill;

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
    }

    private void DestroyBase()
    {
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
}