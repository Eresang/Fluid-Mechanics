Shader "Hidden/Water"
{
	Properties
	{
		_gsTime ("Time", float) = 0
		_gsXsize ("Xsize", float) = 0
		_gsYsize ("Ysize", float) = 0
		_gsWaterMix ("WaterMix", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Cull Off ZWrite Off ZTest On

		Pass
		{
			CGPROGRAM

			//#pragma target 3.0
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			half _gsTime, _gsXsize, _gsYsize;
			static const half twoPI = 6.28;
			sampler2D _gsWaterMix;

			half4 frag (v2f_img i) : COLOR
			{
				half2 q = half2(_gsXsize, _gsYsize);
				q = 1 - (floor(i.uv * q) / q);

				half2 q2 = half2(q.x, 1 - q.y);

				half w = dot(q, q2);

				half r = abs(frac(sin(dot(q, q) * twoPI + _gsTime + 1.37) *
								  sin(dot(q2, q2) * 3.24 + _gsTime + 1.54) - w));

				w = sin((_gsTime + q.x * q.y + r * r) * twoPI) * 0.5 + 0.5;

				return tex2D(_gsWaterMix, half2(w, r));
			}

			ENDCG
		}
	}
}
