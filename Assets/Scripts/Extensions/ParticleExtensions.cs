using UnityEngine;
using System.Collections;

public static class ParticleExtensions {
    

    public static void enableEmission(this ParticleSystem particles, bool enabled) {
        try {
            var em = particles.emission;
            em.enabled = enabled;
        } catch(Exception e) { }
    }
}
