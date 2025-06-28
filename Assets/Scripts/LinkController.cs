using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LinkController : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
    private bool isProcessingClick = false;

    public void OpenLinkDelayed(string link)
    {
        if (!isProcessingClick)
        {
            StartCoroutine(OpenLinkAfterDelay(link, 1f));
        }
    }

    private System.Collections.IEnumerator OpenLinkAfterDelay(string link, float delay)
    {
        isProcessingClick = true;
        yield return new WaitForSeconds(delay);
        OpenLink(link);
        isProcessingClick = false;
    }
}