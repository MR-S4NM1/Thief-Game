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

        Pass // Configuraci�n de Renderizaci�n.
        {
            Blend Zero One // El objeto hace que sea invisible.
            ZWrite Off // No bloquear objetos detr�s de �l.
        
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
