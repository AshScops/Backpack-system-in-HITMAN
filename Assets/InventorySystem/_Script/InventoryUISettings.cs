using FairyGUI;
using inventory_item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace inventory
{
    public class InventoryUISettings : Singleton<InventoryUISettings>
    {
        private GButton current_button = null;
        private GButton hold_button = null;

        public GComponent menu_root;//Should be menu_root
        public GComponent inventory;
        public string fgui_package_path = "Assets/InventorySystem/UI/";
        public string fgui_package_name = "Inventory";
        public Vector2 screenSize = new Vector2(1920f , 1080f);

        public void Init()
        {
            //初始化FGUI,直接生成
            UIPackage.AddPackage(fgui_package_path + fgui_package_name);

            GameObject root_go = new GameObject("inventory_root_go");
            root_go.layer = LayerMask.NameToLayer("UI");
            UIPanel ui_panel = root_go.AddComponent<UIPanel>();
            ui_panel.packageName = "Inventory";
            ui_panel.componentName = "TopButtonList";
            ui_panel.container.renderMode = RenderMode.ScreenSpaceOverlay;
            ui_panel.CreateUI();

            menu_root = ui_panel.ui;
            inventory = (GComponent)UIPackage.CreateObject(fgui_package_name, "Inventory");
            menu_root.AddChild(inventory);
            menu_root.Center();

            GList gList = inventory.GetChild("list").asList;
            gList.SetVirtual();
            gList.itemRenderer = (index, obj) => {

                (string itemType, Queue<ItemBase> queue) = Inventory.Instance.items.ElementAt(index);
                GButton gButton = obj.asButton;
                gButton.GetChild("item_name").asTextField.text = queue.Peek().item_name + " X" + queue.Count;
                gButton.GetChild("item_image").asLoader.texture = new NTexture(queue.Peek().item_image.texture);

                gButton.onClick.Set(() =>
                {
                    if (current_button != null)
                        current_button.GetTransition("on_select").PlayReverse();
                    gButton.GetTransition("on_select").Play();

                    GComponent com = inventory.GetChild("item_info").asCom;
                    com.GetChild("item_name").asTextField.text = queue.Peek().item_name;
                    com.GetChild("item_description").asTextField.text = queue.Peek().item_description;

                    current_button = gButton;
                    hold_button.touchable = true;
                });
            };
            gList.numItems = Inventory.Instance.items.Count;

            hold_button = inventory.GetChild("button").asButton;
            hold_button.GetChild("text").asTextField.text = "设置为当前物品";
            hold_button.touchable = false;
            hold_button.onClick.Set(() =>
            {
                GList gList = inventory.GetChild("list").asList;
                ItemBase targetItem = Inventory.Instance.items.ElementAt(gList.GetChildIndex(current_button)).Value.Peek();
                Inventory.Instance.ChangeCurrentItem(targetItem);
                Debug.Log("change current_item to: " + Inventory.Instance.current_item.item_name);
            });

            menu_root.visible = false;
            inventory.visible = false;
        }

        public void UpdateItemsList()
        {
            GList gList = inventory.GetChild("list").asList;
            gList.numItems = Inventory.Instance.items.Count;
        }


        public void UpdateHoldStateHUD()
        {
            GComponent com = HUDSettings.Instance.hud_root.GetChild("item_image").asCom;
            GLoader gLoader = com.GetChild("item_image").asLoader;
            if(Inventory.Instance.hold_in_hand)
            {
                gLoader.alpha = 1f;
                gLoader.color = Color.white;
            }
            else
            {
                gLoader.alpha = 0.4f;
                gLoader.color = Color.black;
            }
        }

        public void UpdateItemDispalyHUD()
        {
            GComponent com = HUDSettings.Instance.hud_root.GetChild("item_image").asCom;
            GLoader gLoader = com.GetChild("item_image").asLoader;

            if (Inventory.Instance.current_item == null)
            {
                gLoader.url = "";
            }
            else
            {
                gLoader.texture = new NTexture(Inventory.Instance.current_item.item_image.texture);
            }

        }
    }

}
