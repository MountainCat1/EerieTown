using UnityEngine;

namespace Import.RTS_Camera.Demo
{
    [RequireComponent(typeof(Scripts.RTS_Camera))]
    public class TargetSelector : MonoBehaviour 
    {
        private Scripts.RTS_Camera cam;
        private new Camera camera;
        public string targetsTag;

        private void Start()
        {
            cam = gameObject.GetComponent<Scripts.RTS_Camera>();
            camera = gameObject.GetComponent<Camera>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag(targetsTag))
                        cam.SetTarget(hit.transform);
                    else
                        cam.ResetTarget();
                }
            }
        }
    }
}
