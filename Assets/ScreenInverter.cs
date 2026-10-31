using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenColorInverter : MonoBehaviour
{
    [SerializeField] private ScriptableRendererData rendererData;

    private ScriptableRendererFeature inversionFeature;
    private Coroutine inversionCoroutine;

    private void Awake()
    {

        foreach (ScriptableRendererFeature feature
                 in rendererData.rendererFeatures)
        {
            if (feature != null &&
                feature.GetType().Name == "FullScreenPassRendererFeature")
            {
                inversionFeature = feature;
                break;
            }
        }

        inversionFeature.SetActive(false);
    }

    public void SetInverted()
    {
        if (inversionFeature == null)
        {
            Debug.LogError("Inversion feature is not available.");
            return;
        }

        Debug.Log("Inverting!");

        if (inversionCoroutine != null)
        {
            StopCoroutine(inversionCoroutine);
        }

        inversionCoroutine = StartCoroutine(InvertForOneSecond());
    }

    private IEnumerator InvertForOneSecond()
    {
        inversionFeature.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        inversionFeature.SetActive(false);
        inversionCoroutine = null;
    }
}