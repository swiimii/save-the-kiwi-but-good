using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Color targetColor = Color.red;
    public float lerpTime = 60;
    public Transform lookTarget;
    public Material initialSkyboxMaterial, targetSkyboxMaterial;
   
    private Color initialColor;
    private Color initialSkyboxColor1, initialSkyboxColor2, initialSunColor;
    private List<string> skyboxPropertyNames;
    private Dictionary<string, UnityEngine.Rendering.ShaderPropertyType> skyboxPropertyDict;
    private Quaternion initialRotation;
    private float progress = 0;

    private void Start()
    {
        initialColor = GetComponent<Light>().color;
        Camera.main.gameObject.GetComponent<Skybox>().material = new Material(initialSkyboxMaterial);
        var mat = Camera.main.gameObject.GetComponent<Skybox>().material;

        skyboxPropertyNames = new List<string>();
        skyboxPropertyDict = new Dictionary<string, UnityEngine.Rendering.ShaderPropertyType>();
        for (int i = 0; i < 12; i++)
        {
            skyboxPropertyNames.Add(mat.shader.GetPropertyName(i));
            skyboxPropertyDict.Add(mat.shader.GetPropertyName(i), mat.shader.GetPropertyType(i));
        }
 
        initialRotation = transform.rotation;
        StartCoroutine("LerpColor");
    }

    public IEnumerator LerpColor()
    {
        while (progress < lerpTime)
        {
            progress += Time.deltaTime;
            GetComponent<Light>().color = Color.Lerp(initialColor, targetColor, progress / lerpTime);
            // Camera.main.gameObject.GetComponent<Skybox>().material.SetColor("_SkyGradientTop", Color.Lerp(initialSkyboxColor1, targetColor, progress / lerpTime));
            var mat = Camera.main.gameObject.GetComponent<Skybox>().material;
            foreach (string property in skyboxPropertyNames)
            {
                if (targetSkyboxMaterial.HasProperty(property) )
                {
                    // Lerp skybox colors
                    if (skyboxPropertyDict[property] == UnityEngine.Rendering.ShaderPropertyType.Color)
                    {
                        mat.SetColor(property, Color.Lerp(initialSkyboxMaterial.GetColor(property), targetSkyboxMaterial.GetColor(property), progress / lerpTime));
                    }
                    // Lerp skybox floats
                    else if (skyboxPropertyDict[property] == UnityEngine.Rendering.ShaderPropertyType.Float)
                    {
                        mat.SetFloat(property, Mathf.Lerp(initialSkyboxMaterial.GetFloat(property), targetSkyboxMaterial.GetFloat(property), progress / lerpTime));
                    }
                }
            }
            if (lookTarget)
            {
                transform.rotation = Quaternion.Lerp(initialRotation, Quaternion.LookRotation(lookTarget.position), progress / lerpTime);
            }

            yield return null;
        }
    }
}
