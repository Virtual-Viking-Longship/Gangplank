using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Animator animator;
    private int destination;

    public string[] sceneNames;
    void Start()
    {
        destination = 0;
        FadeComplete();
    }

    public void FadeScene (int Index)
    {
        animator.SetTrigger("Fade Out");
        destination = Index;
    }

    public void FadeComplete ()
    {
        GameObject player = GameObject.FindWithTag("Player");
        //GameObject Camera = GameObject.FindWithTag("Main Camera");
        if (destination == 0)  // Shoreline
        {
            player.transform.SetPositionAndRotation(new Vector3(-24, -0.187f, 3), new Quaternion(0, 0, 0, 0));
            //Camera.transform.rotation = new Quaternion(0, 90, 0, 0);
            for (int i = 0; i < sceneNames.Length; i++)
            {
                SceneManager.LoadScene(sceneNames[i], LoadSceneMode.Additive);
            }
        }
        if (destination == 1) //Back of Ship Annotations
        {
            player.transform.SetPositionAndRotation(new Vector3(-15, 0, 1), new Quaternion(0, 0, 0, 180));
        }
        if (destination == 2) // Front of Ship Annotations
        {
            player.transform.SetPositionAndRotation(new Vector3(-15, 0, 7), new Quaternion(0, 0, 0, 90));
        }
        if (destination == 3) // Rowing MiniGame
        {
            player.transform.SetPositionAndRotation(new Vector3(-15.83f, -0.187f, 7.071f), new Quaternion(0, 0, 0, 0));
            //Camera.transform.rotation = new Quaternion(0, 180, 0, 0);
            for (int i = 0; i < sceneNames.Length; i++)
            {
                SceneManager.LoadScene("Rowing-Game", LoadSceneMode.Additive);
            }
        }
        animator.SetTrigger("Fade In");
    }
}
