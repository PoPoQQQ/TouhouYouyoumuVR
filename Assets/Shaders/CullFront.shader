Shader "Custom/CullFront"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _FogColor ("Fog Color", Color) = (1, 1, 1, 1)
        _Far ("Far", Float) = 300
    }
    SubShader
    {
        Pass
        {
        	Tags { "LightMode"="ForwardBase" }
            Cull front
            ZWrite Off
            ZTest Off
            Lighting Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _FogColor;
            float _Far;

            struct v2f
            {
                float4 pos : POSITION;
                half2 uv : TEXCOORD0;
                float3 viewSpacePosition : TEXCOORD1;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.viewSpacePosition = UnityObjectToViewPos(v.vertex);
                return o;
            }

            float4 frag (v2f i) : COLOR
            {
                float rate = sqrt(clamp(abs(i.viewSpacePosition.z) / _Far, 0, 1));
                fixed4 texColor = tex2D(_MainTex, float2(1 - i.uv.x, i.uv.y)) * _Color;
                return float4(texColor.rgb * texColor.a * (1 - rate)  + _FogColor.rgb * _FogColor.a * rate, texColor.a * (1 - rate) + _FogColor.a * rate);
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
