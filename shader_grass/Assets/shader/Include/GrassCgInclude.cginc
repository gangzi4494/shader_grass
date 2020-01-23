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

sampler2D _Noise;

half4 _WindControl;
half4 _WaveControl;


//交互
float4 _PlayerPos;
half _Strength;
half _PushRadius;

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

	// 草自身风吹草动的计算
	float2 samplePos = worldPos.xz / _WaveControl.w;
	samplePos += _Time.x * -_WaveControl.xz;
	fixed waveSample = tex2Dlod(_Noise, float4(samplePos, 0, 0)).r;
	worldPos.x += sin(_Time.x * waveSample) * v.texcoord.y;
	worldPos.y += sin(_Time.x * waveSample) * v.texcoord.y;


	//草地交互的计算
	float dis = distance(_PlayerPos, worldPos);
	float pushDown = saturate((1 - dis + _PushRadius) * v.texcoord.y * _Strength);
	float3 direction = normalize(worldPos.xyz - _PlayerPos.xyz);
	direction.y *= 0.5;
	worldPos.xyz += direction * pushDown;


	///
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
