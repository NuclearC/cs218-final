Shader "Hidden/TestShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Texture", 2D) = "white" {}
        _NoiseMultiplier("NoiseMultiplier", float) = 0.5
        _Contrast("Contrast", float) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _NoiseMultiplier;
            float _Contrast;
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv;
                uv.x = 0.35*sin(_Time.y*50.0);
                uv.y = 0.35*cos(_Time.y*50.0);   

                fixed4 col = tex2D(_MainTex, i.uv);

                const float3 proportion = (0.30, 0.59, 0.11);

                float intensity = dot(col.rgb, proportion);
	
                intensity = clamp(_Contrast * (intensity - 0.5) + 0.5, 0.0, 1.0);

                float green = clamp(intensity / 0.59, 0.0, 1.0);
                
                fixed4 noise = _NoiseMultiplier * tex2D(_NoiseTex, i.uv + uv);

                col = (float4(col.rgb * float3(0, green, 0), col.a) * noise);

                return col;
            }
            ENDCG
        }
    }
}
