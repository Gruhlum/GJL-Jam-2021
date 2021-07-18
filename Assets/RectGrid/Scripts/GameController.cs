using HecTecGames.SoundSystem;
using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public class GameController : MonoBehaviour
	{
        [SerializeField] private RectGrid grid = default;
        [SerializeField] private GridLoader gridLoader = default;
        [SerializeField] private Sprite goldenTile = default;
        [SerializeField] private Sprite greenTile = default;

        [SerializeField] private List<SavedGrid> savedGrids = default;

        [SerializeField] private TileUnit blocker = default;

        public PlayerData PlayerData
        {
            get
            {
                return playerData;
            }
            private set
            {
                playerData = value;
            }
        }
        [SerializeField] private PlayerData playerData = default;

        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
            }
        }
        [SerializeField] private int currentLevel = 0;

        public static bool AllowInput
        {
            get
            {
                if (blockingScripts.Count == 0)
                {
                    return true;
                }
                else return false;
            }
        }
        public static List<MonoBehaviour> blockingScripts = new List<MonoBehaviour>();

        public CappedInt EnemiesKilled
        {
            get
            {
                return enemiesKilled;
            }
            set
            {
                enemiesKilled = value;
            }
        }
        [SerializeField] private CappedInt enemiesKilled = default;

        private Tile end;

        [SerializeField] private GameObject winScreen = default;

        [SerializeField] private SoundClip levelCompleteSound = default;

        private void Awake()
        {
            gridLoader.GridLoaded += GridLoader_GridLoaded;
            EnemiesKilled.ValueChanged += EnemiesKilled_ValueChanged;
            blockingScripts.Clear();
        }
        private void Start()
        {
            gridLoader.LoadGrid(GetNextLevel());
        }

        private void OnDisable()
        {
            gridLoader.GridLoaded -= GridLoader_GridLoaded;
            EnemiesKilled.ValueChanged -= EnemiesKilled_ValueChanged;
        }

        private void EnemiesKilled_ValueChanged(int value)
        {
            if (value >= EnemiesKilled.MaxValue)
            {
                EnableGoal();
            }
        }

        private SavedGrid GetNextLevel()
        {
            CurrentLevel++;
            if (savedGrids.Count <= CurrentLevel - 1)
            {
                ShowEnd();
                return null;
            }
            return savedGrids[CurrentLevel - 1];
        }
        private void ShowEnd()
        {
            winScreen.gameObject.SetActive(true);
        }
        private void GridLoader_GridLoaded(GridLoadData data)
        {
            end = data.End;
            end.SetSprite(goldenTile);
            data.Player.Tile.SetSprite(greenTile);
            end.AddEffect(new EndGameEffect(this));

            EnemiesKilled.Value = 0;
            EnemiesKilled.MaxValue = data.TileUnits.Count;
            blocker.gameObject.SetActive(true);
            end.Unit = blocker;
            blocker.transform.position = end.transform.position;

            foreach (var tile in grid.GetAllTiles())
            {
                if (tile.Unit == null || tile.Unit is Player || tile.Unit == blocker)
                {
                    continue;
                }
                else tile.Unit.UnitDied += Unit_UnitDied;
            }
        }

        private void Unit_UnitDied(TileUnit obj)
        {
            EnemiesKilled.Value++;
            //Debug.Log(EnemiesKilled.ToString());
        }

        public void EnableGoal()
        {
            end.Unit = null;
            blocker.gameObject.SetActive(false);
        }

        public void GoalReached(TileUnit unit)
        {
            if (unit is Player)
            {
                StartCoroutine(LevelTransition());
            }
        }
        IEnumerator LevelTransition()
        {
            levelCompleteSound.PlaySound();
            yield return new WaitForSeconds(0.5f);
            grid.RemoveAllTiles();
            yield return new WaitForSeconds(1.75f);
            gridLoader.LoadGrid(GetNextLevel());
        }
    }
}