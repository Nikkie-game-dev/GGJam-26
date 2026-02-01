using Code.FileManager;
using UnityEngine;

public class Mai : MonoBehaviour
{
    [SerializeField] private FileData _fileData;

    void Start()
    {
        Code.Player.PlayerDataHandler playerDataHandler = new Code.Player.PlayerDataHandler(_fileData);

        playerDataHandler.SavePlayerData("TESTING", 666);
        playerDataHandler.SavePlayerData("tdwd", 352);
        playerDataHandler.SavePlayerData("rewghywf", 2123);
        playerDataHandler.SavePlayerData("gdf", 44);
        playerDataHandler.SavePlayerData("qwe3q", 666);

        var data = playerDataHandler.GetAllPlayersData();

        foreach (var player in data)
        {
            Debug.Log(player.name + " " + player.score);
        }
    }

}
