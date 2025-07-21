using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Animator animator;
    private int destination;

    public void FadeScene (int Index)
    {
        animator.SetTrigger("Fade Out");
        destination = Index;
    }

    public void FadeComplete ()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (destination == 0)
        {
            player.transform.SetPositionAndRotation(new Vector3(-24, 0, 3), new Quaternion(0, 0, 0, 0));
        }
        if (destination == 1)
        {
            player.transform.SetPositionAndRotation(new Vector3(-15, 0, 1), new Quaternion(0, 0, 0, 180));
        }
        if (destination == 2)
        {
            player.transform.SetPositionAndRotation(new Vector3(-15, 0, 7), new Quaternion(0, 0, 0, 90));
        }
    }
}
