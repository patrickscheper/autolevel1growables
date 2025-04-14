using ColossalFramework;
using ICities;
using UnityEngine;

namespace AutoLevel1Growables
{
    public class AutoLevel1GrowablesMod : IUserMod
    {
        public string Name => "Auto Level 1 Growables Mod";

        public string Description => "This mod ensures that all growables placed are set to level 1. This allows those growables to naturally grow.";
    
    }

    public class AutoLevel1GrowablesLoading : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame)
            {
                BuildingManager.instance.EventBuildingCreated += OnBuildingCreated;
            }
        }

        public override void OnLevelUnloading()
        {
            BuildingManager.instance.EventBuildingCreated -= OnBuildingCreated;
        }

        private void OnBuildingCreated(ushort buildingID)
        {
            Singleton<SimulationManager>.instance.AddAction(delegate {
     
  
            Building building = BuildingManager.instance.m_buildings.m_buffer[buildingID];
                BuildingInfo info = building.Info;

                if (info == null) return;

                if (building.m_level != 0)
                {
                    building.m_level = 0;
                    BuildingManager.instance.m_buildings.m_buffer[buildingID] = building;

                    Debug.Log($"[AutoLevel1Growables] Set building {buildingID} to level 1.");
                }
            });
        }
    }
}

