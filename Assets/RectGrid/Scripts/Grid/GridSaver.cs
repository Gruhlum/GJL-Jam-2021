using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Exile
{
#if UNITY_EDITOR
    public class GridSaver : MonoBehaviour
	{
		[TextArea]
		public string folderLocation = "Assets/HexagonPackage/Scripts/SavedGrids/Grids";
		public new string name;

        public bool OverrideSave
        {
            get
            {
                return overrideSave;
            }
            private set
            {
                overrideSave = value;
            }
        }
        private bool overrideSave;

        public RectGrid grid;
        private RectGrid oldGrid;

        [SerializeField] private GridEditor gridEditor = default;

        private void Awake()
        {
            Application.quitting += Application_quitting;
        }

        private void Application_quitting()
        {
            SavePositions(name, true);
        }
        private void OnValidate()
        {
            if ((name == "" && grid != null) || (oldGrid != grid && oldGrid != null && name == oldGrid.name))
            {
                name = grid.name;
                oldGrid = grid;
            }
        }

        private bool AssetAlreadyExist(string name)
        {
            string[] results = AssetDatabase.FindAssets(name, new string[] { folderLocation });
            //Debug.Log(results.Length);
            if (results.Length == 0)
            {
                return false;
            }
            return true;
        }
        [ContextMenu("Save")]
        private void Save()
        {
            SavePositions(name);
        }
        public void SavePositions(string name, bool forceSave = false)
        {
            if (name == "")
            {
                name = "unnamed";
            }
            if (forceSave == false && overrideSave == false && AssetAlreadyExist(name))
            {
                Debug.Log(name + " already exists. Click again to override.");
                overrideSave = true;
                return;
            }
            overrideSave = false;

            SavedGrid savedGrid = ScriptableObject.CreateInstance<SavedGrid>();
            savedGrid.SaveGridData(grid.Tiles, gridEditor.ObjectDatas, gridEditor.UnitDatas, gridEditor.End);
           
            AssetDatabase.CreateAsset(savedGrid, folderLocation + "/" + name + ".asset");
            Debug.Log("Save Successful");
        }
    }
#endif
}