using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] Transform m_HeadTransform;  // The head transform of the character
    [SerializeField] Transform m_PlayerTransform;  // The player's transform
    [Range(1, 180)]
    [SerializeField] float m_SightAngle = 90f;
    [Range(1, 30)]
    [SerializeField] float m_SightDistance = 10f;
    [Range(0.1f, 1f)]
    [SerializeField] float m_RefreshRate = 0.25f;

    float m_CurrentTime = 0f;

    void OnEnable()
    {
        if (!m_HeadTransform)
        {
            Animator animator = GetComponent<Animator>();
            if (animator)
                m_HeadTransform = animator.GetBoneTransform(HumanBodyBones.Head);
        }
    }

    void Update()
    {
        if (!IsValid())
            return;

        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= m_RefreshRate)
        {
            m_CurrentTime = 0;
            CheckAndRotateTowardsPlayer();
        }
    }

    bool IsValid()
    {
        return m_HeadTransform != null && m_PlayerTransform != null;
    }

    void CheckAndRotateTowardsPlayer()
    {
        float distance = Vector3.Distance(m_HeadTransform.position, m_PlayerTransform.position);
        if (distance <= m_SightDistance)
        {
            Vector3 directionToPlayer = (m_PlayerTransform.position - m_HeadTransform.position).normalized;
            float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

            if (angleToPlayer <= m_SightAngle * 0.5f)
            {
                // Rotate the head to look at the player
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                m_HeadTransform.rotation = Quaternion.Slerp(m_HeadTransform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!m_HeadTransform)
            return;

        Gizmos.color = Color.cyan;
        Vector3 trPosition = m_HeadTransform.position;
        for (float angle = m_SightAngle * -0.5f; angle < m_SightAngle * 0.5f; angle += m_SightAngle * 0.05f)
        {
            Gizmos.DrawLine(trPosition, trPosition + Quaternion.AngleAxis(angle, transform.up) * transform.forward * m_SightDistance);
        }

        Gizmos.color = Color.red;
        if (m_PlayerTransform)
        {
            Vector3 vecDirection = (m_PlayerTransform.position - trPosition).normalized;
            Gizmos.DrawLine(trPosition, trPosition + transform.forward * m_SightDistance);
            Gizmos.DrawLine(trPosition, trPosition + vecDirection * m_SightDistance);
        }
    }
}
