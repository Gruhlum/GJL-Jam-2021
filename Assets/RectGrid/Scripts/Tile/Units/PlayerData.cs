using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[CreateAssetMenu(menuName = "PlayerData")]
	public class PlayerData : ScriptableObject
	{
        public MyInt Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }
        [SerializeField] private MyInt health = default;
        public MyInt Coins
        {
            get
            {
                return coins;
            }
            set
            {
                coins = value;
            }
        }
        [SerializeField] private MyInt coins = default;

        public MyInt Bombs
        {
            get
            {
                return bombs;
            }
            set
            {
                bombs = value;
            }
        }
        [SerializeField] private MyInt bombs = default;

        public void ResetData()
        {
            Health.Value = 3;
            Coins.Value = 0;
            Bombs.Value = 0;
        }
    }
}