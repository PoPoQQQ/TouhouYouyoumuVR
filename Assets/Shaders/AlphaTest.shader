Shader "Custom/AlphaTest"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
    }
    SubShader
    {
    	Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }

        Pass
        {
        	Tags { "LightMode"="ForwardBase" }

            Cull front
            Lighting Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Cutoff;

            struct v2f
            {
                float4 pos : POSITION;
                half2 uv : TEXCOORD0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }

            float4 frag (v2f i) : COLOR
            {
                fixed4 texColor = tex2D(_MainTex, float2(1 - i.uv.x, i.uv.y)) * _Color;
                if ((texColor.a - _Cutoff) < 0)
                	discard;
                return texColor;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
