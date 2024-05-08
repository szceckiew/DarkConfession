Shader "Masked/Mask" {
	
	Subshader{

		Tags {"Queue" = "Geometry+10"}

		ColorMask 0
		ZWrite On

		Pass {}

		}

	}