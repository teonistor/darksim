using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour {
    public enum Type { Key, Light, Speed };

    [SerializeField] private Material keyMaterial, lightMaterial, speedMaterial;
    [SerializeField] private GameObject indicatorPrefab;

    private Type type;
    private TargetIndicator indicator;
    
	public void Init (Type type) {
        this.type = type;
        indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();

        Material material;
        switch (type) {
            case Type.Key: material = keyMaterial; break;
            case Type.Light: material = lightMaterial; break;
            default: material = speedMaterial; break;
        }
        GetComponent<Renderer>().material = material;
        indicator.Init(transform, Camera.main, WorldGenerator.Player.transform,
                       () => type == Type.Key ? Difficulty.KeysNecessary != Difficulty.KeysCollected : true,
                       material);
    }

    void OnTriggerEnter(Collider other) {
        print ("Collected " + type.ToString());

        switch (type) {
            case Type.Key: Difficulty.CollectKey(); Score.KeyCollection(); break;
            case Type.Light: Difficulty.CollectLight(); break;
            case Type.Speed: Difficulty.CollectSpeed(); break;
        }

        StartCoroutine(DestroyAnimation());
        GetComponent<Collider>().enabled = false;
        Destroy(indicator.gameObject);
    }

    private IEnumerator DestroyAnimation () {
        float frame = 1f / 30;
        WaitForSeconds wait = new WaitForSeconds(frame);
        frame *= 2;

        Vector3 s = transform.localScale;

        for (float t = 0f; t <= 1f; t += frame) {
            transform.localScale = Vector3.Lerp(s, Vector3.zero, t);
            yield return wait;
        }

        Destroy(gameObject);
    }

    // Disused
    // For demo unity logo
    private Vector3 tp = new Vector3(-8f, 4f, -8f),
                    tr = new Vector3(22.8f, 96.8f, -26.6f),
                    ts = new Vector3(1f, 1.03f, -1f);

    // Disused
    private IEnumerator collectAnimation() {
        float frame = 1f / 30;
        WaitForSeconds wait = new WaitForSeconds(frame);
        //transform.SetParent(cmr, true);

        Vector3 sp = transform.localPosition,
                sr = transform.localEulerAngles,
                ss = transform.localScale;

        for (float t=0f;t<1.1f ; t += frame) {
            transform.localPosition = Vector3.Lerp(sp, tp, t);
            transform.localEulerAngles = Vector3.Lerp(sr, tr, t);
            transform.localScale = Vector3.Lerp(ss, ts, t);
            yield return wait;
        }
    }
}
