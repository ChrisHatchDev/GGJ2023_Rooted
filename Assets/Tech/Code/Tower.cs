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
    public IPowerSource PowerSource;  // This should be a union between towers and bases because those will be the only power sources
    public Dictionary<GameObject, Tower> TowersInRange = new Dictionary<GameObject, Tower>();


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

        // if (LineController)
        // {
            Debug.Log("we drawing the line controller bruh");
            LineController.SetLinePoints(transform.position, PowerSource.transform.position);
        // }
    }

    private IEnumerator ShootCycle()
    {
        yield return new WaitForSeconds(0.5f);
        ShootAllEnemiesInRange();
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

    public void OnPowerSourceEnter(Collider other)
    {
        Debug.Log("OnTowerPowerEnter");
        PowerSource = other.gameObject.GetComponentInParent<IPowerSource>();
        HasPower = PowerSource.HasPower;  // maybe this needs to be checked on update to handle when a child tower gets cutoff down the line
        SetPowerStatusVisuals();
    }

    public void OnPowerSourceExit(Collider other)
    {
        Debug.Log("OnTowerPowerExit");
        HasPower = false;  // maybe this should be set to PowerSource.HasPower but I'm really not sure what the move is here
        PowerSource = null;  // I'm not sure if this event means that the power has actually been cut off
        SetPowerStatusVisuals();
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
    public override PowerSourceType GetPowerType()
    {
        return PowerType;
    }
}
