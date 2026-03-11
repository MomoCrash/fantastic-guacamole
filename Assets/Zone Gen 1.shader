Shader "Unlit/Zone Gen 1"
{
    Properties
    {
        _Color0 ("Color", Color) = (1, 1, 1, 1)
        _Color1 ("Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color0;
            float4 _Color1;
            float4 _Points[256];
            uint _PointCount;
            uint _ResolutionScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float s = max(_ResolutionScale, 1.0);
                o.uv = floor(v.uv * s) / s;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist_closest = 1000.0;
                uint closest = 0;
                for(uint j = 0; j < _PointCount; ++j)
                {
                    float2 delta = _Points[j].xy - i.uv.xy; 
                    float dist = sqrt(delta.x * delta.x + delta.y * delta.y);
                    if(dist < dist_closest)
                    {
                        dist_closest = dist;
                        closest = j;
                    }
                }

                fixed4 col = _Color1 * _Points[closest].z + _Color0 * (1 - _Points[closest].z);
                return col; // float4(i.uv.xy, 0, 1);
            }
            ENDCG
        }
    }
}
