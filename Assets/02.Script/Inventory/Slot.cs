﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Button itemImage;

    [SerializeField] private Text textCount;

    [SerializeField] private GameObject CheackObject;
    [SerializeField] private Text CheackText;
    [SerializeField] private Button consent;
    [SerializeField] private Button denial;
    [SerializeField] private Button Esc;
    [SerializeField] private GameObject player;
    private void SetActive(bool val)
    {
        itemImage.gameObject.SetActive(val);
    }
    public void Additem(Item _item,int _Count=1)
    {
        item = _item;
        itemCount = _Count;
        itemImage.image.sprite = _item.image;

        if(item.Gettype()!=Item.type.Weapon)
        {
            textCount.gameObject.SetActive(true);
            textCount.text = itemCount.ToString();
        }
        else
        {
            textCount.text = "0";
            textCount.gameObject.SetActive(false);
        }
        itemImage.onClick.RemoveAllListeners();
        itemImage.onClick.AddListener(Execute);
        SetActive(true);
    }
    public void SetSlotCount(int _Count)
    {
        itemCount += _Count;
        textCount.text = itemCount.ToString();

        if(itemCount <=0)
        {
            ClearSlot();
        }
        
    }
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.image.sprite = null;
        SetActive(false);
        textCount.text = "0";
        textCount.gameObject.SetActive(false);

    }
    void Execute()
    {
        if(item.Gettype() == Item.type.Portion || item.Gettype() == Item.type.AdditionHp || item.Gettype() == Item.type.AdditionPower)
        {
            InventorySystem.temporaryHealamount = item.Get();
            CheackText.text = "아이템을 사용하시겠습니까?";
            consent.onClick.RemoveAllListeners();
            consent.onClick.AddListener(Portionconsent);
            denial.onClick.RemoveAllListeners();
            denial.onClick.AddListener(Portiondenial);
            Esc.onClick.RemoveAllListeners();
            Esc.onClick.AddListener(Portiondenial);
            CheackObject.SetActive(true);
        }
        
    }
    public void weapondenial()
    {
        InventorySystem.temporaryDamaage = 0;
        CheackObject.SetActive(false);
    }
    public void Portionconsent()
    {
        if(item.Gettype() == Item.type.Portion)
            player.GetComponent<PlayerState>().Heal(InventorySystem.temporaryHealamount);
        if (item.Gettype() == Item.type.AdditionHp)
            player.GetComponent<PlayerState>().HpAdd(item.Get());
        SetSlotCount(-1);
        InventorySystem.temporaryHealamount = 0;
        CheackObject.SetActive(false);
    }
    public void Portiondenial()
    {
        InventorySystem.temporaryHealamount = 0;
        CheackObject.SetActive(false);
    }
}
