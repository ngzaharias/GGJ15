Shader "Nyan Prime/Ocean2"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_MainTex2 ("Base (RGB) Trans (A)", 2D) = "white" {}
		_MainTex3 ("Modifier", 2D) = "white" {}
		_DisplacementTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
//			"Queue" = "Transparent"
			"Queue" = "Geometry"
//			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		LOD 200

		CGPROGRAM
//		#pragma surface surf Lambert alpha vertex:disp
		#pragma surface surf Lambert vertex:disp

		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _DisplacementTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_MainTex3;
			float3 vertexOutput;
		};

		void disp (inout appdata_full v, out Input o)
		{
			half2 coords = v.texcoord.xy;
			coords.x += _Time; // 20 sec cycle time

			float d = tex2Dlod(_DisplacementTex, float4(coords, 0, 0)).r;

			v.vertex.y += d;
			
			// Texture blend
			v.color.r = clamp(d - 0.4, 0, 1);
			
			// Darkening
			v.color.g = (d * 0.95) + 0.95;

			
//			v.texcoord.x += (d * 0.05) + (_Time * 0.25);

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertexOutput = v.color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
//			IN.uv_MainTex.x += _Time * 0.25;
//			IN.uv_MainTex2.x += _Time * 0.25;
			
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2) * _Color;
			fixed4 c3 = tex2D(_MainTex3, IN.uv_MainTex3);
			
			o.Albedo = lerp(c.rgb, c2.rgb, IN.vertexOutput.r + c3.r);
			o.Albedo *= (IN.vertexOutput.g);
			o.Albedo += c3.r;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Transparent/VertexLit"
}

