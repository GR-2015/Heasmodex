using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharactereEquipment
{
    [Header("Wallet")]
    [SerializeField] public float money = 100f;

    [Header("Weapons.")] 
    [SerializeField] public GameObject ActiveProjectile = null;
    [SerializeField] public Weapon mainHandWeapon;
    [SerializeField] public Weapon offHandWeapon;

    [Header("Armor parts.")]
    [SerializeField] public Armor breastplate;

    [Header("Backpack.")]
    [SerializeField] public List<ItemSlot> equipment = new List<ItemSlot>();

    [SerializeField] public int projectilesCount = 10;

    public List<BaseProjectile> characterProjectiles = new List<BaseProjectile>();

    //TMP
    public void GenerateProjectiles<T>(
        LayerMask enemyLayerMask,
        GameObject ownerGameObject) where T : BaseProjectile
    {
        for (int i = 0; i < projectilesCount; i++)
        {
            //GameObject newProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //newProjectile.SetActive(false);
            //newProjectile.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            //newProjectile.gameObject.name = "Projectile";
            
            //BaseProjectile projectileComponent = newProjectile.AddComponent<T>();
            //projectileComponent.Velocity = .01f;
            //characterProjectiles.Add(projectileComponent);
            GameObject newProjectile = GameObject.Instantiate(ActiveProjectile);
            newProjectile.SetActive(false);

            BaseProjectile newProjectileComponent = newProjectile.GetComponent<BaseProjectile>();
            newProjectileComponent.EnemyLayerMask = enemyLayerMask;
            newProjectileComponent.Owner = ownerGameObject;

            characterProjectiles.Add(newProjectileComponent);
        }
    }

    public void Store(GameObject newItem)
    {
        ItemSlot[] itemSlots = equipment.Where(e => e.itemName == newItem.name).ToArray();
        if (itemSlots.Length > 0)
        {
            itemSlots[0].itemCount++;
        }
        else
        {
            equipment.Add(new ItemSlot(newItem.name));
        }
    }
}

[Serializable]
public struct ItemSlot
{
    public string itemName;
    public int itemCount;

    public ItemSlot(string itemName)
        : this()
    {
        this.itemName = itemName;
        itemCount = 1;
    }
}