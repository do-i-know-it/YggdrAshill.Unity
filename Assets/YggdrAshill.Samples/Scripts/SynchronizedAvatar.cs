using UnityEngine;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    internal sealed class SynchronizedAvatar : MonoBehaviour
    {
        [SerializeField] private PhotonView photonView;

        [SerializeField] private Material performerMaterial;
        
        [SerializeField] private Material viewerMaterial;

        [SerializeField] private MeshRenderer headMeshRenderer;

        [SerializeField] private MeshRenderer leftHandMeshRenderer;

        [SerializeField] private MeshRenderer rightHandMeshRenderer;

        private void OnEnable()
        {
            if (photonView.IsMine)
            {
                headMeshRenderer.material = performerMaterial;
                leftHandMeshRenderer.material = performerMaterial;
                rightHandMeshRenderer.material = performerMaterial;
            }
            else
            {
                headMeshRenderer.material = viewerMaterial;
                leftHandMeshRenderer.material = viewerMaterial;
                rightHandMeshRenderer.material = viewerMaterial;
            }
        }
    }
}
