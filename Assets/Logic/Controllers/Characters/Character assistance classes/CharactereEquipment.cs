using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharactereEquipment
{
    [Header("Weapons.")]

    [SerializeField] public Weapon mainHandWeapon;

    [SerializeField] public Weapon offHandWeapon;

    [Header("Armor parts.")] 
    [SerializeField] public Armor breastplate;

    [Header("Backpack.")] 
    [SerializeField]
    public float money = 100f;

    [SerializeField]
    public List<ItemSlot> equipment = new List<ItemSlot>();

    [SerializeField]
    public int projectilesCount = 10;

    public List<BaseProjectile> characterProjectiles = new List<BaseProjectile>();

    //TMP
    public void GenerateProjectiles<T>() where T : BaseProjectile
    {
        for (int i = 0; i < projectilesCount; i++)
        {
            GameObject newProjectile = new GameObject();

            newProjectile.SetActive(false);
            newProjectile.AddComponent<CapsuleCollider>();

            BaseProjectile projectileComponent = newProjectile.AddComponent<T>();

            characterProjectiles.Add(projectileComponent);  
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

    public ItemSlot(string itemName) : this()
    {
        this.itemName = itemName;
        itemCount = 1;
    }
}