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
            Destroy(gameObject);
        }

        HealthbarFill.fillAmount = (Health / MaxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tower")
        {
            if (!TowersInRange.ContainsKey(other.gameObject))
            {
                Tower newTower = other.GetComponent<Tower>();
                newTower.SetPowerStatus(true);
                TowersInRange.TryAdd(other.gameObject, newTower);
            }
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tower")
        {
            Debug.Log("Tower is Leaving base!");
            if (TowersInRange.ContainsKey(other.gameObject))
            {
                TowersInRange.GetValueOrDefault(other.gameObject).SetPowerStatus(false);
                TowersInRange.Remove(other.gameObject);
            }
        } 
    }
}