using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Animator animator;
    private int destination;
    private Scene scene;

    void Start()
    {
        destination = 0;
        FadeComplete();
    }

    public void FadeScene(int Index)
    {
        animator.SetTrigger("Fade Out");
        destination = Index;
    }

    //toggles scene on or off based on toggle. Does nothing is already satisfied
    private void HelpLoad(string scenename, bool toggle)
    {
        scene = SceneManager.GetSceneByName(scenename);
        if (toggle && !scene.isLoaded)
        {
            SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
        }
        else if (!toggle && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(scenename);
        }
    }

    IEnumerator Loader()
    {
        if (destination == 4)
        {
            yield return SceneManager.LoadSceneAsync("Hedeby-World", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Hedeby-World"));
        }
        else
        {
            yield return SceneManager.LoadSceneAsync("World-Environment", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("World-Environment"));
        }
    }
    private void HelpMainLoad(string scenename)
    {
        scene = SceneManager.GetSceneByName(scenename);
        if (!scene.isLoaded)
        {
            StartCoroutine(Loader());
        }
    }
    public void FadeComplete()
    {

        GameObject player = GameObject.FindWithTag("Player");
        if (destination == 0)  // Ship Building
        {
            HelpMainLoad("World-Environment");
            HelpLoad("1 to 10 Ship Building", true);
            HelpLoad("Ship-With-Annotations", false);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
        }
        else if (destination == 1) //Back of Ship Annotations
        {
            HelpMainLoad("World-Environment");
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", true);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
        }
        else if (destination == 2) // Front of Ship Annotations
        {
            HelpMainLoad("World-Environment");
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", true);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
            player.transform.SetPositionAndRotation(new Vector3(-15, .35f, 7), new Quaternion(0, 0, 0, 90));
        }
        else if (destination == 3) // Rowing Game
        {
            HelpMainLoad("World-Environment");
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", true);
            HelpLoad("Rowing-Game", true);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
            player.transform.SetPositionAndRotation(new Vector3(-15.83f, 0.3f, 7.071f), new Quaternion(0, 0, 0, 0));
        }
        else if (destination == 4) // Hedeby Chest
        {
            HelpLoad("World-Environment", false);
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", false);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", true);
            HelpMainLoad("Hedeby-World");
            player.transform.SetPositionAndRotation(new Vector3(1.123f, 2.05f, -12.234f), new Quaternion(0, 180, 0, 0));
        }
        animator.SetTrigger("Fade In");
    }
}
