using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loader : MonoBehaviour
{
    /*
    Version: 0.1
    Changlog: Reload On Death
    Ernesto
*/
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
