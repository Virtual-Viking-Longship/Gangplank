using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void FadeScene (int Index)
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
    public void FadeComplete()
    {
        
        GameObject player = GameObject.FindWithTag("Player");
        if (destination == 0)  // Ship Building
        {
            HelpLoad("World-Environment", true);
            HelpLoad("1 to 10 Ship Building", true);
            HelpLoad("Ship-With-Annotations", false);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
        }
        if (destination == 1) //Back of Ship Annotations
        {
            HelpLoad("World-Environment", true);
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", true);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
        }
        if (destination == 2) // Front of Ship Annotations
        {
            HelpLoad("World-Environment", true);
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", true);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
            player.transform.SetPositionAndRotation(new Vector3(-15, 0, 7), new Quaternion(0, 0, 0, 90));
        }
        if (destination == 3) // Rowing Game
        {
            HelpLoad("World-Environment", true);
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", true);
            HelpLoad("Rowing-Game", true);
            HelpLoad("Hedeby-Game", false);
            HelpLoad("Hedeby-World", false);
            player.transform.SetPositionAndRotation(new Vector3(-15.83f, 0.3f, 7.071f), new Quaternion(0, 0, 0, 0));
        }
        if (destination == 4) // Hedeby Chest
        {
            HelpLoad("World-Environment", false);
            HelpLoad("1 to 10 Ship Building", false);
            HelpLoad("Ship-With-Annotations", false);
            HelpLoad("Rowing-Game", false);
            HelpLoad("Hedeby-Game", true);
            HelpLoad("Hedeby-World", true);
            player.transform.SetPositionAndRotation(new Vector3(1.123f, 1.95f, -12.234f), new Quaternion(0, 180, 0, 0));
        }
        animator.SetTrigger("Fade In");
    }
}
