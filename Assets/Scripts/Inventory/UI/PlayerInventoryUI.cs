using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BGS.Task
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField][Range(16, 32)] private int initialSlots = 16;
        
        [SerializeField] private UIPlayerInventorySlot hotbarSlotPrefab;
        [SerializeField] private UIPlayerInventorySlot slotPrefab;
        [SerializeField] private RectTransform slotsParent;
        
        [SerializeField] private TextMeshProUGUI selectedItemNameText;
        
        [SerializeField] private PlayerInventory controller;
        
        private List<UIPlayerInventorySlot> hotbarSlots = new();
        private List<UIPlayerInventorySlot> slots = new();
       
        private bool showingNormalInventory = true;
        
        private UIPlayerInventorySlot selectedHotbarSlot;
        private UIPlayerInventorySlot selectedSlot;

        private void Awake()
        {
            showingNormalInventory = true;
        }

        private void Start()
        {
            Initialize();

            for (int i = 0; i < hotbarSlots.Count; i++)
            {
                hotbarSlots[i].Select(i==0);
            }
            foreach (var t in slots)
            {
                t.Select(false);
            }
            
            controller.OnInventoryChanged += UpdateSlots;
        }

        private void OnDestroy()
        {
            controller.OnInventoryChanged -= UpdateSlots;
        }

        private void UpdateSlots(Dictionary<Item,int> dict)
        {
            foreach (var kv in dict)
            {
                var slot = slots.FirstOrDefault(x => x.Item == kv.Key);
                if (slot != null)
                {
                    slot.AssignItem(kv.Key, kv.Value);
                }
                else
                {
                    slot = GetFirstEmptySlot();
                    if (slot != null)
                    {
                        slot.AssignItem(kv.Key, kv.Value);
                    }
                }
            }

            UpdateHotbar(dict);
        }

        private void UpdateHotbar(Dictionary<Item, int> dict)
        {
            foreach (var slot in slots.ToList())
            {
                if (slot.IsEmpty || !slot.Item.EquippableInHotbar) continue;
                var hotbarSlot = hotbarSlots.FirstOrDefault(x => x.Item == slot.Item);
                if (hotbarSlot != null)
                {
                    hotbarSlot.AssignItem(slot.Item, dict.FirstOrDefault(x => x.Key == slot.Item).Value);
                }
                else
                {
                    hotbarSlot = hotbarSlots.FirstOrDefault(x => x.Item == null);
                    if (hotbarSlot != null)
                    {
                        hotbarSlot.AssignItem(slot.Item, dict.FirstOrDefault(x => x.Key == slot.Item).Value);
                    }
                }
                slot.Clear();
            }
        }

        
        private UIPlayerInventorySlot GetFirstEmptySlot()
        {
            return slots.FirstOrDefault(x => x.Item == null);
        }

        public void Initialize()
        {
            for (int i = 0; i < 8; i++)
            {
                var slot = Instantiate(hotbarSlotPrefab, slotsParent);
                slot.InitializeHotbarSlot(i+1);
                hotbarSlots.Add(slot);
            }
            for (int i = 0; i < initialSlots; i++)
            {
                var slot = Instantiate(slotPrefab, slotsParent);
                slot.Clear();
                slots.Add(slot);
            }
            showingNormalInventory = true;
            ShowNormalInventory();
        }

        public void ForceClose()
        {
            showingNormalInventory = true;
            ShowNormalInventory();
        }
        
        public void ShowNormalInventory()
        {
            showingNormalInventory = !showingNormalInventory;

            if (showingNormalInventory)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].Show(showingNormalInventory, 0.075f, i * 0.0075f);
                }
            }
            else
            {
                for (int i = slots.Count - 1; i >= 0; i--)
                {
                    slots[i].Show(showingNormalInventory, 0.075f, (slots.Count - 1 - i) * 0.0075f);
                }
            }
            UpdateHotbar(controller.Items);
        }        
        
        private void SelectSlot(UIPlayerInventorySlot slotToSelect)
        {
            foreach (var slot in hotbarSlots)
            {
                slot.Select(slot == slotToSelect);
            }
            selectedItemNameText.text = slotToSelect.Item != null ? slotToSelect.Item.DisplayName : "";
        }

        public void SendKey(int key)
        {
            selectedHotbarSlot = hotbarSlots[key - 1];
            selectedSlot = null;
            if (selectedHotbarSlot.Selected)
            {
                selectedHotbarSlot.Use();
            }
            SelectSlot(selectedHotbarSlot);
        }
        
    }
}
