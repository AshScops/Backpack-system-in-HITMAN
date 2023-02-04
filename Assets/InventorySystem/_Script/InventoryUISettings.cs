using FairyGUI;
using inventory_item;
using inventory_module;
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

                ItemBase item = Inventory.Instance.GetItemByIndex(index);
                int count = Inventory.Instance.GetCountByIndex(index);
                GButton gButton = obj.asButton;
                gButton.GetChild("item_name").asTextField.text = item.item_name + " X" + count;
                gButton.GetChild("item_image").asLoader.texture = new NTexture(item.item_image.texture);

                gButton.onClick.Set(() =>
                {
                    if (current_button != null)
                        current_button.GetTransition("on_select").PlayReverse();
                    gButton.GetTransition("on_select").Play();

                    GComponent com = inventory.GetChild("item_info").asCom;
                    com.GetChild("item_name").asTextField.text = item.item_name;
                    com.GetChild("item_description").asTextField.text = item.item_description;

                    current_button = gButton;
                    hold_button.touchable = true;
                });
            };
            gList.numItems = Inventory.Instance.GetItemTypesCount();

            hold_button = inventory.GetChild("button").asButton;
            hold_button.GetChild("text").asTextField.text = "设置为当前物品";
            hold_button.touchable = false;
            hold_button.onClick.Set(() =>
            {
                GList gList = inventory.GetChild("list").asList;
                ItemBase targetItem = Inventory.Instance.GetItemByIndex(gList.GetChildIndex(current_button));
                Inventory.Instance.ChangeCurrentItem(targetItem);
                Debug.Log("change current_item to: " + Inventory.Instance.current_item.item_name);
            });

            menu_root.visible = false;
            inventory.visible = false;

            Inventory.Instance.onHoldChange.AddListener(UpdateHoldStateHUD);
            Inventory.Instance.onItemChange.AddListener(UpdateItemDispalyHUD);

            InventoryModule.Instance.openInventory.AddListener(OpenInventory);
            InventoryModule.Instance.closeInventory.AddListener(CloseInventory);
            //TODO:销毁时解绑
        }

        private void OpenInventory()
        {
            InventoryUISettings.Instance.menu_root.visible = true;
            InventoryUISettings.Instance.inventory.visible = true;
            UpdateItemsList();
        }

        private void CloseInventory()
        {
            InventoryUISettings.Instance.menu_root.visible = false;
            InventoryUISettings.Instance.inventory.visible = false;
        }

        private void UpdateItemsList()
        {
            GList gList = inventory.GetChild("list").asList;
            gList.numItems = Inventory.Instance.GetItemTypesCount();
        }

        private void UpdateHoldStateHUD()
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

        private void UpdateItemDispalyHUD(ItemBase targetItem)
        {
            GComponent com = HUDSettings.Instance.hud_root.GetChild("item_image").asCom;
            GLoader gLoader = com.GetChild("item_image").asLoader;

            if (targetItem == null)
            {
                gLoader.url = "";
            }
            else
            {
                gLoader.texture = new NTexture(targetItem.item_image.texture);
            }
        }
    }

}
