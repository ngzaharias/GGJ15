Shader "Custom/Ocean"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_NormalMap ("Base (RGB) Trans (A)", 2D) = "white" {}
		_DisplacementTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
//			"Queue" = "Transparent"
//			"RenderType" = "Transparent"
		}
		
		LOD 200

		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert vertex:disp
//		#pragma surface surf Lambert alpha vertex:disp

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _DisplacementTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float3 vertexOutput;
		};

		void disp (inout appdata_full v, out Input o)
		{
			half2 coords = v.texcoord.xy;
			coords.x += _Time; // 20 sec cycle time

			float d = tex2Dlod(_DisplacementTex, float4(coords, 0, 0)).r;

			v.vertex.y += d;
			v.color.r = d;

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertexOutput = v.color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
//			IN.uv_MainTex.x -= _Time * 0.25;
			IN.uv_MainTex.x += IN.vertexOutput.r * 0.15f;
			IN.uv_MainTex.x += _Time * 0.25;
			IN.uv_NormalMap.x += _Time;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Transparent/VertexLit"
}

