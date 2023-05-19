Shader "StylizedLava/StylizedLava"
{
	Properties
	{
		_LavaSpeed("LavaSpeed", Range( 0 , 0.1)) = 0.08
		_LavaDepth("LavaDepth", Float) = 0.2
		_FoamDepth("Foam Depth", Float) = 0.2
		_MainTexture("MainTexture", 2D) = "white" {}
		_EdgePower("Edge Power", Range( 0 , 1)) = 0.1
		[HDR]_LavaEmissive("LavaEmissive", Color) = (16,1.172775,0,0)
		[HDR]_LavaColor("LavaColor", Color) = (8,0.2509804,0,0)
		_FoamSpeed("Foam Speed", Vector) = (0.08,0.08,0,0)
		_FoamAmount("Foam Amount", Float) = 0.5
		_FoamScale("Foam Scale", Float) = 10
		[HDR]_EdgeColor("EdgeColor", Color) = (8,6.65098,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float4 _EdgeColor;
		uniform float4 _LavaColor;
		uniform sampler2D _MainTexture;
		uniform float _LavaSpeed;
		uniform float4 _LavaEmissive;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _LavaDepth;
		uniform float _EdgePower;
		uniform float _FoamDepth;
		uniform float _FoamAmount;
		uniform float2 _FoamSpeed;
		uniform float _FoamScale;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline float noise_randomValue (float2 uv) { return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453); }

		inline float noise_interpolate (float a, float b, float t) { return (1.0-t)*a + (t*b); }

		inline float valueNoise (float2 uv)
		{
			float2 i = floor(uv);
			float2 f = frac( uv );
			f = f* f * (3.0 - 2.0 * f);
			uv = abs( frac(uv) - 0.5);
			float2 c0 = i + float2( 0.0, 0.0 );
			float2 c1 = i + float2( 1.0, 0.0 );
			float2 c2 = i + float2( 0.0, 1.0 );
			float2 c3 = i + float2( 1.0, 1.0 );
			float r0 = noise_randomValue( c0 );
			float r1 = noise_randomValue( c1 );
			float r2 = noise_randomValue( c2 );
			float r3 = noise_randomValue( c3 );
			float bottomOfGrid = noise_interpolate( r0, r1, f.x );
			float topOfGrid = noise_interpolate( r2, r3, f.x );
			float t = noise_interpolate( bottomOfGrid, topOfGrid, f.y );
			return t;
		}


		float SimpleNoise(float2 UV)
		{
			float t = 0.0;
			float freq = pow( 2.0, float( 0 ) );
			float amp = pow( 0.5, float( 3 - 0 ) );
			t += valueNoise( UV/freq )*amp;
			freq = pow(2.0, float(1));
			amp = pow(0.5, float(3-1));
			t += valueNoise( UV/freq )*amp;
			freq = pow(2.0, float(2));
			amp = pow(0.5, float(3-2));
			t += valueNoise( UV/freq )*amp;
			return t;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord1 = i.uv_texcoord * float2( 2,2 );
			float simplePerlin2D2 = snoise( uv_TexCoord1 );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float temp_output_3_0 = (-1.5 + (simplePerlin2D2 - -1.0) * (0.62 - -1.5) / (1.0 - -1.0));
			float temp_output_6_0 = ( _Time.y * _LavaSpeed );
			float2 uv_TexCoord12 = i.uv_texcoord + float2( -0.8,0 );
			float temp_output_1_0_g1 = ( _Time.y * _LavaSpeed );
			float4 lerpResult11 = lerp( tex2D( _MainTexture, ( i.uv_texcoord + ( temp_output_3_0 + temp_output_6_0 ) ) ) , tex2D( _MainTexture, ( uv_TexCoord12 + ( temp_output_3_0 + temp_output_6_0 ) ) ) , abs( ( ( temp_output_1_0_g1 - floor( ( temp_output_1_0_g1 + 0.5 ) ) ) * 2 ) ));
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth35 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth35 = abs( ( screenDepth35 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _LavaDepth ) );
			float clampResult46 = clamp( ( distanceDepth35 * _EdgePower ) , 0.0 , 1.0 );
			float Edge48 = clampResult46;
			float4 lerpResult59 = lerp( ( _LavaColor * lerpResult11 ) , ( lerpResult11 * _LavaEmissive ) , Edge48);
			float screenDepth26 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth26 = abs( ( screenDepth26 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamDepth ) );
			float clampResult39 = clamp( ( distanceDepth26 * _FoamAmount ) , 0.0 , 1.0 );
			float2 uv_TexCoord42 = i.uv_texcoord * float2( 7,7 ) + ( _Time.y * _FoamSpeed );
			float simpleNoise44 = SimpleNoise( uv_TexCoord42*_FoamScale );
			float4 color50 = IsGammaSpace() ? float4(1,1,1,1) : float4(1,1,1,1);
			float Foam66 = ( step( ( clampResult39 * 1.0 ) , simpleNoise44 ) * color50.a );
			float4 temp_cast_0 = (Foam66).xxxx;
			float4 lerpResult63 = lerp( lerpResult59 , temp_cast_0 , color50.a);
			float4 screenColor61 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_screenPosNorm.xy);
			float4 lerpResult64 = lerp( lerpResult59 , screenColor61 , lerpResult59.a);
			o.Emission = ( ( _EdgeColor * lerpResult63 ) + lerpResult64 ).rgb;
			float temp_output_60_3 = lerpResult59.a;
			o.Alpha = temp_output_60_3;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}