Shader "Custom/Ocean"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_DisplacementTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Transparent"
		}
		
		LOD 200

		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert vertex:disp

		sampler2D _MainTex;
		sampler2D _DisplacementTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
			float3 vertexOutput;
		};

		void disp (inout appdata_full v, out Input o)
		{
			half2 coords = v.texcoord.xy;
			coords.x += _Time; // 20 sec cycle time

			float d = tex2Dlod(_DisplacementTex, float4(coords, 0, 0)).r;

			v.vertex.y += d;

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertexOutput = v.color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			IN.uv_MainTex.x += _Time * 0.25;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Transparent/VertexLit"
}

