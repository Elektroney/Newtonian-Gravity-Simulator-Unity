using UnityEngine;
public class SceneManager : MonoBehaviour
{


    void Update()
    {
        if(Input.GetButtonDown("Jump"))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
