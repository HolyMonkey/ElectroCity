using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
	public void OnButtonClicked()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
