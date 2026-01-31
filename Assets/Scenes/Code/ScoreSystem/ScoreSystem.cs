using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Code.ScoreSystem
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField]
        private List<Level> levels;
        
        [SerializeField]
        private List<ScoreType> scoreTypes;

        private bool _timerOn;
        
        public float CurrentTimeLeft { get; private set; }
        

        public void EndLevel(Level level, int timeLeftMs)
        {
            foreach (var levelToChange in levels)
            {
                if (levelToChange != level) continue;
                
                levelToChange.EndLevel(timeLeftMs);
                EndTimer();
                break;
            }
        }

        public void StartLevel(Level level)
        {
            _timerOn = true;
            CurrentTimeLeft = level.TimeLimitMS;
            
            StartCoroutine(TimerCoroutine());
        }

        public void SaveTotalScore()
        {
            var finalScore = 0;
            foreach (var level in levels)
            {
                if (!level.LevelEnded) continue;
                
                finalScore += level.Score;
            }
            
            //GetUserName
            //SaveScore
        }

        private void EndTimer()
        {
            _timerOn = false;
            StopCoroutine(TimerCoroutine());
        }
        
        private IEnumerator TimerCoroutine()
        {
            while (_timerOn)
            {
                CurrentTimeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        
    }
}