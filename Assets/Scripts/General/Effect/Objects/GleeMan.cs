using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GleeMan : MonoBehaviour
{
    public int Content;
    public float Timy;
    public bool SoundTrue;
    public bool SectionUp;
    public bool InteractAgain = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && InteractAgain)
        {
            transform.position -= Vector3.up * 100;
            Speak();
        }
    }
    void Speak()
    {
        InteractAgain = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("Act", 0);
        FindObjectOfType<PostProcessingEffects>().Poem(Content, Timy, SoundTrue, SectionUp);
    }
}
