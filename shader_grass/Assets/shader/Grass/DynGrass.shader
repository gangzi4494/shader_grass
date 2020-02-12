Shader "Unlit/DynGrass"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_Noise("Noise", 2D) = "black" {}

		_WindControl("WindControl(x:XSpeed y:YSpeed z:ZSpeed w:windMagnitude)",vector) = (1,0,1,0.5)
			//前面几个分量表示在各个轴向上自身摆动的速度, w表示摆动的强度
			_WaveControl("WaveControl(x:XSpeed y:YSpeed z:ZSpeed w:worldSize)",vector) = (1,0,1,1)
			//前面几个分量表示在各个轴向上风浪的速度, w用来模拟地图的大小,值越小草摆动的越凌乱，越大摆动的越整体


			_Strength("Strength", float) = 1
			//草地弯曲的强度
			_PushRadius("PushRadius", float) = 1
			//交互的范围

	}
		SubShader
		{
			Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" "IgnoreProjector" = "True" }
			LOD 100

			Cull Off

			Pass
			{
				CGPROGRAM
				// make fog work
				#pragma multi_compile_fog
				#include "../Include/GrassCgInclude.cginc"

				#pragma vertex vert_dyn
				#pragma fragment frag









				ENDCG
			}
		}
}
