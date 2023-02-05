using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

public class Tower : MonoBehaviour
{
    public Animator Anim;

    public TMPro.TMP_Text StatusText;
    public bool PoweredByTower = false;
    public bool PoweredByBase = false;

    [Space(10)]
    public NavMeshAgent Agent;
    public bool Moving = false;

    [Header("Material Logic")]
    public SkinnedMeshRenderer Renderer;
    public Material PoweredMat;
    public Material NotPoweredMat;

    public MeshRenderer DamageRangeRenderer;

    public Dictionary<GameObject, Enemy> EnemiesInRange = new Dictionary<GameObject, Enemy>();
    public int WeaponDamage = 5;
    public int NumEnemiesToShoot = 3;
    

    private void Start()
    {
        StartCoroutine(ShootCycle());
    }

    public void ShowDamageVisuals()
    {
        DamageRangeRenderer.enabled = true;
    }

    public void HideDamageVisuals()
    {
        DamageRangeRenderer.enabled = false;
    }

    public void AddEnemeyInRange(Enemy enemy)
    {
        if (!EnemiesInRange.ContainsKey(enemy.gameObject))
        {
            EnemiesInRange.TryAdd(enemy.gameObject, enemy);
        }
    }

    public void RemoveEnemeyInRange(Enemy enemy)
    {
        if (EnemiesInRange.ContainsKey(enemy.gameObject))
        {
            EnemiesInRange.Remove(enemy.gameObject);
        }
    }

    void Update()
    {
        string statusText = (PoweredByTower || PoweredByBase) ? "POWERED" : "NOT POWRED";
        string movingText = Moving ? "Moving" : "Stationary";

        StatusText.text = $"{statusText}\n{movingText}";
    }

    private IEnumerator ShootCycle()
    {
        yield return new WaitForSeconds(0.5f);
        ShootAllEnemiesInRange();
        //ShootNEnemiesInRange(NumEnemiesToShoot);
        
        StartCoroutine(ShootCycle());
    }

    public void ShootNEnemiesInRange(int n){
        Anim.SetBool("Shooting", EnemiesInRange.Count > 0);
        for(int i =0; i<n; i++){
            List<Enemy> values = EnemiesInRange.Values.ToList();
            values[Random.Range(0, values.Count)].Damage(WeaponDamage);
        }
    }
    public void ShootAllEnemiesInRange()
    {
        Anim.SetBool("Shooting", EnemiesInRange.Count > 0);
        foreach (var enemeyPair in EnemiesInRange)
        {
            enemeyPair.Value.Damage(Random.Range(3,WeaponDamage));
        }
    }

    public void SetPowerStatusVisuals()
    {
        Renderer.materials = new Material[]{(PoweredByTower || PoweredByBase) ? PoweredMat : NotPoweredMat};
    }

    public void SetPoweredByTower(bool poweredByTower, bool poweredByBase)
    {
        Renderer.materials = new Material[]{(PoweredByTower || PoweredByBase) ? PoweredMat : NotPoweredMat};
        PoweredByBase = poweredByBase;
        PoweredByTower = poweredByTower;
    }

    public void OnTowerPowerEnter(Collider other)
    {
        PoweredByTower = true;
        SetPowerStatusVisuals();
    }

    public void OnTowerPowerExit()
    {
        PoweredByTower = false;
        SetPowerStatusVisuals();
    }

    public void OnBasePowerEnter(Collider other)
    {
        PoweredByBase = true;
        Base _base = other.gameObject.GetComponentInParent<Base>();
        _base.AddTowerInRange(this);
        SetPowerStatusVisuals();
    }

    public void OnBasePowerExit(Collider other)
    {
        Base _base = other.gameObject.GetComponentInParent<Base>();
        _base.RemoveTowerInRange(this);

        PoweredByBase = false;
        SetPowerStatusVisuals();
    }
}
