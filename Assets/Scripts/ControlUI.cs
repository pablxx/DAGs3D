using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public Image oclusor;
    public Coroutine rutinaTransicion;
    public WaitForSeconds espera = new WaitForSeconds(0.02f);
    public GameObject canvasPrincipal;
    public List<GameObject> listaOcultables; 

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvasPrincipal);
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene("nivelBlanco");
    }

    void OcultarObjetos()
    {
        foreach (var objeto in listaOcultables)
        {
            objeto.SetActive(false);
        }
    }

    public void IniciarTransicion()
    {
        if (rutinaTransicion == null)
        {
            rutinaTransicion = StartCoroutine(TaparEscena());
        }
    }

    IEnumerator TaparEscena()
    {
        oclusor.raycastTarget = true;

        while (oclusor.color.a < 1)
        {
            oclusor.color += new Color(0, 0, 0, 0.05f);
            yield return espera;
        }

        CambiarEscena();
        oclusor.raycastTarget = false;

        yield return new WaitForSeconds(4f);
        OcultarObjetos();

        StartCoroutine(DestaparEscena());
        rutinaTransicion = null;
    }

    IEnumerator DestaparEscena()
    {
        while (oclusor.color.a > 0)
        {
            oclusor.color -= new Color(0, 0, 0, 0.05f);
            yield return espera;
        }
    }
}
