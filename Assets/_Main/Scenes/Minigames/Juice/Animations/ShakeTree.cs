using UnityEngine;
using ScriptLibrary;
using UnityEngine.SceneManagement;

public class ShakeTree : OnInteractTrigger2D
{
    private int _count = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource shakeSound;
    [SerializeField] private ParticleSystem leavesParticle;
    [SerializeField] private int shakesToFinish = 10;

    [SerializeField] private float cooldownTime = 0.3f;
    private float _timeSinceLastInteract = Mathf.Infinity;
    
    private bool withinCooldown => _timeSinceLastInteract > cooldownTime;
    protected override void OnInteract()
    {
        if (!withinCooldown)
            return;

        _timeSinceLastInteract = 0f;
        _count++;
        animator.SetTrigger("Shake");
        shakeSound.Play();
        leavesParticle.Play();
        if (_count >= shakesToFinish)
        {
            NextScene();
            enabled = false;
        }
    }

    private void Update()
    {
        _timeSinceLastInteract += Time.deltaTime;
    }

    private void NextScene()
    {
        SceneManager.LoadScene("CollectFruitMinigame");
    }
}