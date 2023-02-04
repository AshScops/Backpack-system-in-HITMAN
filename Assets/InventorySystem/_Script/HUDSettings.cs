using FairyGUI;
using inventory_module;
using UnityEngine;

namespace inventory
{
    public class HUDSettings : Singleton<HUDSettings>
    {
        public GComponent hud_root;
        private string fgui_package_path = "Assets/InventorySystem/UI/";
        private string fgui_package_name = "HUD";
        public Vector2 screenSize = new Vector2(1920f, 1080f);

        public void Init()
        {
            //初始化FGUI,直接生成
            UIPackage.AddPackage(fgui_package_path + fgui_package_name);

            GameObject root_go = new GameObject("hud_root_go");
            root_go.layer = LayerMask.NameToLayer("UI");
            UIPanel ui_panel = root_go.AddComponent<UIPanel>();
            ui_panel.packageName = "HUD";
            ui_panel.componentName = "GameHUD";
            ui_panel.container.renderMode = RenderMode.ScreenSpaceOverlay;
            ui_panel.CreateUI();

            hud_root = ui_panel.ui;
            hud_root.Center();

            InventoryModule.Instance.openInventory.AddListener(() =>
            {
                hud_root.visible = false;
            });

            InventoryModule.Instance.closeInventory.AddListener(() =>
            {
                hud_root.visible = true;
            });
        }
    }

}
