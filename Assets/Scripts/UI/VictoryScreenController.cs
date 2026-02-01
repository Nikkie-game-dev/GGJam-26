using Code.FileManager;
using Code.Manager;
using Code.Player;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryScreenController : MonoBehaviour
{
    [Header("Save")]
    [SerializeField] private FileData playersFileData;

    [Header("UI")]
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text hintText;

    [Header("Scenes")]
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private PlayerDataHandler _playerDataHandler;
    private int _finalScore;
    private bool _submitted;

    private void Awake()
    {
        _playerDataHandler = new PlayerDataHandler(playersFileData);

        if (root != null)
            root.SetActive(false);
    }

    public void Show(int finalScore)
    {
        _finalScore = finalScore;
        _submitted = false;

        if (root != null) root.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Score: {_finalScore}";

        if (hintText != null)
            hintText.text = "Enter your name";

        if (nameInput != null)
        {
            nameInput.interactable = true;
            nameInput.text = "";
            nameInput.Select();
            nameInput.ActivateInputField();
        }

        Time.timeScale = 0f;
    }

    public void OnSubmit()
    {
        if (_submitted) return;

        string playerName = SanitizeName(nameInput != null ? nameInput.text : "");
        if (string.IsNullOrWhiteSpace(playerName))
        {
            if (hintText != null) hintText.text = "Name required";
            return;
        }

        _playerDataHandler.SavePlayerData(playerName, _finalScore);
        _submitted = true;

        if (nameInput != null) nameInput.interactable = false;
        if (hintText != null) hintText.text = "Saved!";

        var highscoreManager = FindFirstObjectByType<HighscoreManager>();
        if (highscoreManager != null)
            highscoreManager.RefreshData();
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private string SanitizeName(string input)
    {
        if (string.IsNullOrEmpty(input)) return "";
        string s = input.Trim();
        if (s.Length > 12) s = s.Substring(0, 12);
        return s;
    }
}