using System;
using System.Collections;
using Assets.Code.Service;
using Systems.CentralizeEventSystem;
using UnityEngine;
using UnityEngine.Serialization;

public delegate void OnGameStart();
public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Tabs")] 
    [SerializeField] 
    private CanvasGroup _mainTab;
    [SerializeField] 
    private CanvasGroup _settingsTab;
    [SerializeField]
    private CanvasGroup _creditsTab;
    [SerializeField]
    private CanvasGroup _highScoreTab;
    private CanvasGroup _currentTab;
    
    [Header("Properties")] 
    [SerializeField][Range(0f, 1f)]
    private float _fadeTimeInSeconds = 0.5f;
    [SerializeField][Range(0f, 1f)]
    private float _tabAppearenceDelay = 0.2f;
    private bool bIsAnyCoroutineActive = false;
    private CentralizeEventSystem _centralizeEventSystem => ServiceProvider.Instance.GetService<CentralizeEventSystem>();
    public void Start()
    {
        InitializeTabs();
    }
    
    #region ButtonsFunctions
    public void StartGameButton()
    {
        if (bIsAnyCoroutineActive)
            return;
        
        bIsAnyCoroutineActive = true;
        StartCoroutine(StartGameButton_Implementation());
    }
    public void SettingsButton()
    {
        if (bIsAnyCoroutineActive)
            return;
        
        bIsAnyCoroutineActive = true;
        StartCoroutine(SettingsButton_Implementation());
    }
    public void CloseGameButton()
    {
        if (bIsAnyCoroutineActive)
            return;
        
        bIsAnyCoroutineActive = true;
        StartCoroutine(CloseGameButton_Implementation());
    }

    public void CreditsButton()
    {
        if (bIsAnyCoroutineActive)
            return;
        
        bIsAnyCoroutineActive = true;
        StartCoroutine(CreditsButton_Implementation());
    }

    public void HighScoreButton()
    {
        if (bIsAnyCoroutineActive)
            return;
        
        bIsAnyCoroutineActive = true;
        
        StartCoroutine(HighScoreButton_Implementation());
    }
    
    public void BackButton()
    {
        if (bIsAnyCoroutineActive)
            return;
        
        bIsAnyCoroutineActive = true;
        
        StartCoroutine(BackButton_Implementation());
    }
    #endregion
    
    #region ButtinsFunctionsImplementations
    private IEnumerator StartGameButton_Implementation()
    {
        yield return StartCoroutine(FadeOutTab(_mainTab, false));
        _centralizeEventSystem.Get<OnGameStart>()?.Invoke();
        _mainTab.gameObject.SetActive(false);
        bIsAnyCoroutineActive = false;
    }

    private IEnumerator SettingsButton_Implementation()
    {
        yield return  StartCoroutine(TabTransitionCoroutine(_currentTab, _settingsTab, true));
        _currentTab = _settingsTab;
        bIsAnyCoroutineActive = false;
    }
    private IEnumerator CloseGameButton_Implementation()
    {
        yield return null;
        Application.Quit();
        bIsAnyCoroutineActive = false;
    }

    private IEnumerator CreditsButton_Implementation()
    {
        yield return StartCoroutine(TabTransitionCoroutine(_currentTab, _creditsTab, true));
        _currentTab = _creditsTab;
        bIsAnyCoroutineActive = false;
    }

    private IEnumerator BackButton_Implementation()
    {
        yield return StartCoroutine(TabTransitionCoroutine(_currentTab, _mainTab, true));
        _currentTab = _mainTab;
        bIsAnyCoroutineActive = false;
    }
    private IEnumerator HighScoreButton_Implementation()
    {
        yield return StartCoroutine(TabTransitionCoroutine(_currentTab, _highScoreTab, true));
        _currentTab = _highScoreTab;
        bIsAnyCoroutineActive = false;
    }
    #endregion

    #region FadeCoroutines

    private IEnumerator TabTransitionCoroutine(CanvasGroup tabOut, CanvasGroup tabIn, bool bDeactivateObject)
    {
        yield return StartCoroutine(FadeOutTab(tabOut, bDeactivateObject));
        yield return new WaitForSeconds(_tabAppearenceDelay);
        yield return StartCoroutine(FadeInTab(tabIn));
    }
    private IEnumerator FadeOutTab(CanvasGroup tabOut, bool bDeactivateObject)
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime / _fadeTimeInSeconds;
            tabOut.alpha = t;
            yield return null;
        }

        tabOut.alpha = 0;
        if (bDeactivateObject)
            tabOut.gameObject.SetActive(false);
    }
    private IEnumerator FadeInTab(CanvasGroup tabIn)
    {
        float t = 0;
        tabIn.gameObject.SetActive(true);
        while (t < 1)
        {
            t += Time.deltaTime / _fadeTimeInSeconds;
            tabIn.alpha = t;
            yield return null;
        }
        tabIn.alpha = 1;
    }

    #endregion
    private void InitializeTabs()
    {
        _settingsTab.alpha = 0f;
        _settingsTab.gameObject.SetActive(false);
        
        _creditsTab.alpha = 0f;
        _creditsTab.gameObject.SetActive(false);
        
        _highScoreTab.alpha = 0f;
        _highScoreTab.gameObject.SetActive(false);

        _currentTab = _mainTab;
    }
}

