Shader "Custom/SetAlpha" {
	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			ColorMask A
			Color (1,1,1,1)
		}
	}
}