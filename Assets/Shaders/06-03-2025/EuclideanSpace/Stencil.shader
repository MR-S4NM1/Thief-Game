Shader"Custom/Stencil"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }
    SubShader{

        Tags{
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass // Configuración de Renderización.
        {
            Blend Zero One // El objeto hace que sea invisible.
            ZWrite Off // No bloquear objetos detrás de él.
        
            Stencil
            {
                Ref[_StencilID]
                Comp Always
                Pass Replace
                Fail keep
            }
        }
    }
}
