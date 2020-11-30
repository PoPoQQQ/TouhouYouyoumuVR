Shader "Custom/AlphaBlending"
{
    //可见参数，在Material的Inspector上可以修改
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _FogColor ("Fog Color", Color) = (1, 1, 1, 1)
        _Far ("Far", Float) = 300
    }

    SubShader
    {
        //标签，指定渲染模式、渲染队列等，也可以写在Pass里面
    	Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent" }

        Pass
        {
            //渲染参数，例如ZWrite Off表示不使用Z Buffer，Cull Front表示只渲染背面（不渲染正面）
        	Tags { "LightMode"="ForwardBase" }
        	ZWrite Off
        	Blend SrcAlpha OneMinusSrcAlpha
            Cull front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            //所有参数的定义，包括可见参数和不可见参数，可以用material.SetXX("Name", value)方法修改运行时的值
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _FogColor;
            float _Far;

            //用于将数据从顶点着色器传到片元着色器的结构体
            struct v2f
            {
                float4 pos : POSITION;
                half2 uv : TEXCOORD0;
                float3 viewSpacePosition : TEXCOORD1;
            };

            //顶点着色器，用于坐标变换
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.viewSpacePosition = UnityObjectToViewPos(v.vertex);
                return o;
            }

            //片元着色器，用于决定最终渲染的颜色
            float4 frag (v2f i) : COLOR
            {
                float rate = pow(clamp(abs(i.viewSpacePosition.z) / _Far, 0, 1), 1.5);
                fixed4 texColor = tex2D(_MainTex, float2(1 - i.uv.x, i.uv.y)) * _Color;
                return float4(texColor.rgb * texColor.a * (1 - rate)  + _FogColor.rgb * _FogColor.a * rate, texColor.a * (1 - rate) + _FogColor.a * rate);
            }

            ENDCG
        }

        //可以写多个Pass，会依次进行渲染，可以用GrabPass获取上一个Pass的渲染结果
    }
    //退路，意思是这个Shader里面没有实现的效果（例如阴影等）全部由FallBack指定的Shader来实现
    FallBack "Diffuse"
}
