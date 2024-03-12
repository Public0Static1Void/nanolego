using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ChangeTransparency : MonoBehaviour
{
    private float transparency = 0.2f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        // Iterate over each renderer
        foreach (Renderer renderer in renderers)
        {
            // Iterate over each material of the renderer
            foreach (Material material in renderer.materials)
            {
                // Get the current color
                Color color = material.color;

                // Set the new alpha value
                color.a = transparency;

                // Apply the new color to the material
                material.color = color;

                // Enable alpha blending for transparency
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            }
        }
        //StartCoroutine(TransparencyCoroutine());
    }

    void ChangeTransparent(float alpha)
    {
        alpha = Mathf.Clamp01(alpha);

        Material material = meshRenderer.material;

        Color color = material.color;

        color.a = alpha;
    }

    private IEnumerator TransparencyCoroutine()
    {
        while(transparency > 0)
        {
            yield return new WaitForSeconds(0.1f);
            transparency -= 0.1f;
            ChangeTransparent(transparency);
            StartCoroutine(TransparencyCoroutine());
        }
    }
}