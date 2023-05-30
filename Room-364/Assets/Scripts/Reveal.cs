//Shady

using UnityEngine;

[ExecuteInEditMode]
public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    Light SpotLight;

    void Update ()
    {
        GameObject objectsWithTag = GameObject.FindGameObjectWithTag("Lumiere-UV");
        if (objectsWithTag is null)
        {
            Mat.SetVector("MyLightPosition", Vector4.zero);
            Mat.SetVector("MyLightDirection", Vector4.zero);
            Mat.SetFloat("MyLightAngle", 0);
        }
        else
        {
            SpotLight = objectsWithTag.GetComponent<Light>();
            if (Mat && SpotLight)
            {
                Mat.SetVector("MyLightPosition", SpotLight.transform.position);
                Mat.SetVector("MyLightDirection", -SpotLight.transform.forward);
                Mat.SetFloat("MyLightAngle", SpotLight.spotAngle);
            }
            else
            {
                Mat.SetVector("MyLightPosition", Vector4.zero);
                Mat.SetVector("MyLightDirection", Vector4.zero);
                Mat.SetFloat("MyLightAngle", 0);
            }
        }
        

    }//Update() end
}//class end