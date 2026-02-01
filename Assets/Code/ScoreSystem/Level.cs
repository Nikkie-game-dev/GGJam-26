using System;
using UnityEngine;

namespace Code.ScoreSystem
{
    [Serializable]
    public class Level
    {
        [SerializeField]
        private int timeLimitMS;
        
        public int TimeLimitMS
        {
            get => timeLimitMS;
            private set => timeLimitMS = value;
        }

        public int Score { get; private set; }
        public bool LevelEnded { get; private set; }

        public void AddScore(ScoreType scoreType) => Score += scoreType.ScoreAmount;
        public void RemoveScore(ScoreType scoreType) => Score -= scoreType.ScoreAmount;

        public void EndLevel(int timeLeftMs)
        {
            if (TimeLimitMS <= timeLeftMs)
            {
                Score *= TimeLimitMS / (TimeLimitMS + timeLeftMs);
            }

            LevelEnded = true;
        }
    }
}