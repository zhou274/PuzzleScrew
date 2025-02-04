using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace NultBolts
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;
        private const string ItemKey = "item_";
        [SerializeField] private Item[] listItem;

        public bool isActiveItem;

        private void Awake()
        {
            Instance = this;
        }

        public int ItemCount(Item.ItemType itemType)
        {
            return PlayerPrefs.GetInt(ItemKey + itemType.ToString().ToLower());
        }

        private void SetItemAmount(Item.ItemType itemType, int val)
        {
            PlayerPrefs.SetInt(ItemKey + itemType.ToString().ToLower(), val);
        }

        public void AddItem(Item.ItemType itemType, int val)
        {
            SetItemAmount(itemType, ItemCount(itemType) + val);
        }

        public Item GetItem(Item.ItemType itemType)
        {
            return listItem.Single(x => x.itemType == itemType);
        }

        public void SelectItem(Item.ItemType itemType)
        {
            listItem.Single(x => x.itemType == itemType).SelectItem();
        }

        public void UnSelectAllItem()
        {
            for (int i = 0; i < listItem.Length; i++)
            {
                listItem[i].UnSelectItem();
            }
        }
    }
}
