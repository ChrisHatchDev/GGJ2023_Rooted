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
    public NavMeshObstacle NavObstacle;
    public bool Moving = false;
    [Header("Material Logic")]
    public SkinnedMeshRenderer Renderer;
    public Material PoweredMat;
    public Material NotPoweredMat;
    public MeshRenderer DamageRangeRenderer;
    public MeshRenderer PowerRangeRenderer;

    public Dictionary<GameObject, Enemy> EnemiesInRange = new Dictionary<GameObject, Enemy>();
    public int WeaponDamage = 5;
    public int NumEnemiesToShoot = 3;
    public TowerLineController LineController;
    public IPowerSource PowerSource;
    public Dictionary<GameObject, IPowerSource> PowerSourcesInRange = new Dictionary<GameObject, IPowerSource>();
    private bool _recentlyMoved;

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

    public void ShowPowerRangeVisuals()
    {
        PowerRangeRenderer.enabled = true;
    }

    public void HidePowerRangeVisuals()
    {
        PowerRangeRenderer.enabled = false;
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
        HasPower = HasValidPower();


        string statusText = HasPower ? "POWERED" : "NOT POWRED";
        string movingText = Moving ? "Moving" : "Stationary";

        StatusText.text = $"{statusText}\n{movingText}";

        if (HasPower)
        {
            Debug.Log("we have a valid powersource");
            LineController.SetLinePoints(LineConnectionPoint.position, PowerSource.LineConnectionPoint.transform.position);
            LineController.Show();
        }
        else
        {
            LineController.Hide();
        }

        if (_recentlyMoved == false && Agent.enabled && Agent.remainingDistance < 0.1f)
        {
            Agent.enabled = false;
            NavObstacle.enabled = true;
        }

        SetPowerStatusVisuals();
    }

    public void OnPickUp()
    {
        ShowPowerRangeVisuals();
    }

    public void OnPlaced(Vector3 placedPos)
    {
        Debug.Log($"Placed Turret now to: {placedPos}");

        NavObstacle.enabled = false;
        Agent.enabled = true;
        Agent.SetDestination(placedPos);
        HidePowerRangeVisuals();

        _recentlyMoved = true;

        StartCoroutine(WaitToRenableAgent());
    }

    IEnumerator WaitToRenableAgent()
    {
        yield return new WaitForSeconds(1.0f);
        _recentlyMoved = false;
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
        if(HasPower)
        {
            Anim.SetBool("Shooting", EnemiesInRange.Count > 0);
            foreach (var enemeyPair in EnemiesInRange)
            {
                enemeyPair.Value.Damage(Random.Range(3,WeaponDamage));
            }
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

    bool HasValidPower()
    {
        foreach (KeyValuePair<GameObject, IPowerSource> item in PowerSourcesInRange)
        {
            if (item.Value.HasPower)
            {
                PowerSource = item.Value;
                return true;
            }
        }

        PowerSource = null;
        return false;
    }

    public void OnPowerSourceEnter(Collider other)
    {
        Debug.Log("OnTowerPowerEnter");
        AddPowerSourceInRange(other.gameObject.GetComponentInParent<IPowerSource>());
    }

    public void OnPowerSourceExit(Collider other)
    {
        Debug.Log("OnTowerPowerExit");
        RemovePowerSourceInRange(other.gameObject.GetComponentInParent<IPowerSource>());
    }

    public void AddPowerSourceInRange(IPowerSource powerSource)
    {
        if (!PowerSourcesInRange.ContainsKey(powerSource.gameObject))
        {
            PowerSourcesInRange.TryAdd(powerSource.gameObject, powerSource);
        }
    }

    public void RemovePowerSourceInRange(IPowerSource powerSource)
    {
        if (PowerSourcesInRange.ContainsKey(powerSource.gameObject))
        {
            PowerSourcesInRange.Remove(powerSource.gameObject);
        }
    }
    public override PowerSourceType GetPowerType()
    {
        return PowerType;
    }
}
