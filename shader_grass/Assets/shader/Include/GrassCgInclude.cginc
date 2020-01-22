#ifndef GRASS_CG_INCLUDE
#define GRASS_CG_INCLUDE

#include "UnityCG.cginc"

struct appdata
{
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float4 tangent : TANGENT;
	float2 texcoord : TEXCOORD0;
	float2 texcoord1 : TEXCOORD1;
};

struct v2f
{
	float2 texcoord : TEXCOORD0;
	UNITY_FOG_COORDS(1)
	float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float4 _MainTex_ST;

half4 _WindControl;
half4 _WaveControl;


inline float4 AnimateGrassVertex(float4 pos)
{
	return pos;
}

v2f vert(appdata v)
{
	v2f o;

	/*float4	mdlPos = AnimateGrassVertex(v.vertex);

	o.vertex = UnityObjectToClipPos(mdlPos);*/
	float4 worldPos = mul(unity_ObjectToWorld, v.vertex);


	worldPos.x += sin(_Time.x * 10) * v.texcoord.y;
	worldPos.y += sin(_Time.x * 10) * v.texcoord.y;

	o.vertex = mul(UNITY_MATRIX_VP, worldPos);



	o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
	UNITY_TRANSFER_FOG(o, o.vertex);
	return o;
}


fixed4 frag(v2f i) : SV_Target
{
	// sample the texture
	fixed4 col = tex2D(_MainTex, i.texcoord);
// apply fog
UNITY_APPLY_FOG(i.fogCoord, col);
return col;
}


#endif
