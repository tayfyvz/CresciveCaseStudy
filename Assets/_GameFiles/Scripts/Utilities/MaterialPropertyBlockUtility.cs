using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    //Responsible for Material Property Block.
    public static class MaterialPropertyBlockUtility
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        //Sets property block for material instances.
        public static void ColorSetter(Renderer renderer, Color color)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor(BaseColor, color);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
        public static void ColorSetter(ParticleSystemRenderer particleSystemRenderer, Color color)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            particleSystemRenderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor(BaseColor, color);
            particleSystemRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
