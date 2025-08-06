using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Animator animator;
    private int destination;
    private Scene world;
    private Scene build;
    private Scene ship;
    private Scene row;

    void Start()
    {
        world = SceneManager.GetSceneByName("World-Environment");
        build = SceneManager.GetSceneByName("1 to 10 Ship Building");
        ship = SceneManager.GetSceneByName("Ship-With-Annotations");
        row = SceneManager.GetSceneByName("Rowing-Game");
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
        world = SceneManager.GetSceneByName("World-Environment");
        build = SceneManager.GetSceneByName("1 to 10 Ship Building");
        ship = SceneManager.GetSceneByName("Ship-With-Annotations");
        row = SceneManager.GetSceneByName("Rowing-Game");
        GameObject player = GameObject.FindWithTag("Player");
        if (!world.isLoaded)
            {
                SceneManager.LoadScene("World-Environment", LoadSceneMode.Additive);
            }
        if (destination == 0)  // Shoreline
        {
            if (!build.isLoaded)
            {
                SceneManager.LoadScene("1 to 10 Ship Building", LoadSceneMode.Additive);
            }
        }
        if (destination == 1) //Back of Ship Annotations
        {
            if (!ship.isLoaded)
            {
                SceneManager.LoadScene("Ship-With-Annotations", LoadSceneMode.Additive);
            }
        }
        if (destination == 2) // Front of Ship Annotations
        {
            if (!ship.isLoaded)
            {
                SceneManager.LoadScene("Ship-With-Annotations", LoadSceneMode.Additive);
            }
            player.transform.SetPositionAndRotation(new Vector3(-15, 0, 7), new Quaternion(0, 0, 0, 90));
        }
        if (destination == 3) // Rowing MiniGame
        {
            player.transform.SetPositionAndRotation(new Vector3(-15.83f, -0.187f, 7.071f), new Quaternion(0, 0, 0, 0));
            if (!ship.isLoaded)
            {
                SceneManager.LoadScene("Ship-With-Annotations", LoadSceneMode.Additive);
            }
            if (!row.isLoaded)
            {
                SceneManager.LoadScene("Rowing-Game", LoadSceneMode.Additive);
            }
        }
        animator.SetTrigger("Fade In");
    }
}
