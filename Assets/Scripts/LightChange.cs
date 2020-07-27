using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Color targetColor = Color.red;
    public float lerpTime = 60;
    public Transform lookTarget;
    private Color initialColor;
    private Color initialSkyboxColor;
    private Quaternion initialRotation;
    private float progress = 0;

    private void Start()
    {
        initialColor = GetComponent<Light>().color;
        initialSkyboxColor = RenderSettings.skybox.color;
        initialRotation = transform.rotation;
        StartCoroutine("LerpColor");
    }

    public IEnumerator LerpColor()
    {
        while (progress < lerpTime)
        {
            progress += Time.deltaTime;
            GetComponent<Light>().color = Color.Lerp(initialColor, targetColor, progress / lerpTime);
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(initialSkyboxColor, targetColor, progress / lerpTime));
            if (lookTarget) transform.rotation = Quaternion.Lerp(initialRotation, Quaternion.LookRotation(lookTarget.position), progress/lerpTime);

            yield return null;
        }
    }
}
