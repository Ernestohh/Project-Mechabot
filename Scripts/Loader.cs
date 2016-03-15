using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject ply;

    void Update()
    {
        loadScene();
    }

    public void loadScene()
    {
        if(ply.GetComponent<Player>().health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }   
    }

}
