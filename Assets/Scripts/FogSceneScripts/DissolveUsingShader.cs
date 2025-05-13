using System.Collections;
using UnityEngine;

public class DissolveUsingShader : MonoBehaviour
{
    public float dissolveDuration = 2f;
    public float dissolveStrength;

    public void StartDissolver()
    {
        StartCoroutine(Dissolver());
    }

    public IEnumerator Dissolver()
    {
        float elapsedTime = 0f;
        Material dissolveMaterial = GetComponent<Renderer>().material;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(1f, 0f, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);
            
            yield return null; 
        }



        yield return new WaitForSeconds(8f); // Adjust time the object stays fully visible

        // Fade Out
        elapsedTime = 0f; 

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(0f, 1f, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);
            
            yield return null; 
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("jai touché la barre espace");
            StartDissolver();
        }
    }
}
