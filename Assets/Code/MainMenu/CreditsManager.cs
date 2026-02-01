using UnityEngine;

namespace Code.MainMenu
{
    public class CreditsManager : MonoBehaviour
    {
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}
