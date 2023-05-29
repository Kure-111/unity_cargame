using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // 遷移先のシーン名

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName); // シーンを切り替える
    }
}
