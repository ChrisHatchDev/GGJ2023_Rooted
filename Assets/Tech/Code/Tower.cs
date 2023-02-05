using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

public class Tower : IPowerSource
{
    public Animator Anim;

    public TMPro.TMP_Text StatusText;
    // public bool PoweredByTower = false;
    // public bool PoweredByBase = false;

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
    
    public TowerLineController LineController;
    public Transform LineStartPoint;
    public Transform LineEndPoint; // Should be the other tower
    public GameObject PowerSource;  // This should be a union between towers and bases because those will be the only power sources
    public bool HasPower = false;

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
        string statusText = HasPower ? "POWERED" : "NOT POWRED";
        string movingText = Moving ? "Moving" : "Stationary";

        StatusText.text = $"{statusText}\n{movingText}";

        if (LineController)
        {
            LineController.SetLinePoints(LineStartPoint.transform.position, LineEndPoint.transform.position);
        }
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
        Renderer.materials = new Material[]{HasPower ? PoweredMat : NotPoweredMat};
    }

    public void SetPoweredByTower(bool poweredByTower, bool poweredByBase)
    {
        Renderer.materials = new Material[]{HasPower ? PoweredMat : NotPoweredMat};
    }

    public void OnTowerPowerEnter(Collider other)
    {
        HasPower = true;
        SetPowerStatusVisuals();
    }

    public void OnTowerPowerExit()
    {
        HasPower = false;
        SetPowerStatusVisuals();
    }

    public void OnBasePowerEnter(Collider other)
    {
        HasPower = true;
        Base _base = other.gameObject.GetComponentInParent<Base>();
        _base.AddTowerInRange(this);
        SetPowerStatusVisuals();
    }

    public void OnBasePowerExit(Collider other)
    {
        Base _base = other.gameObject.GetComponentInParent<Base>();
        _base.RemoveTowerInRange(this);

        HasPower = false;
        SetPowerStatusVisuals();
    }

    public override PowerSourceType GetPowerType()
    {
        return PowerType;
    }
}
