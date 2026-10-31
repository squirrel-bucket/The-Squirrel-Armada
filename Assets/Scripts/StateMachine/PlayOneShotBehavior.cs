using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehavior : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    //delayed sound timer
    public float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            AudioSource source = animator.gameObject.AddComponent<AudioSource>();

            source.clip = soundToPlay;
            source.volume = volume;
            source.spatialBlend = 0f;
            source.Play();

            Destroy(source, soundToPlay.length);
        }

        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;

            if(timeSinceEntered > playDelay)
            {
                AudioSource source = animator.gameObject.AddComponent<AudioSource>();

                source.clip = soundToPlay;
                source.volume = volume;
                source.spatialBlend = 0f;
                source.Play();

                Destroy(source, soundToPlay.length);

                hasDelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            AudioSource source = animator.gameObject.AddComponent<AudioSource>();

            source.clip = soundToPlay;
            source.volume = volume;
            source.spatialBlend = 0f;
            source.Play();

            Destroy(source, soundToPlay.length);
        }
    }
}
