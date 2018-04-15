//

#if __VERSION__ == 330

	in vec4 vertexColor; // The input variable from the vertex shader (same name and same type)
	in vec3 pos;

	uniform vec4 lightColor; // = vec4(0.9, 0.2, 0.9, 1.0);
	uniform float ambientStrength;
	//uniform vec3 worldPosition;
	uniform float pad;
	uniform mat4 colourMat;

	out vec4 color;

	void main()
	{
		
		vec3 worldPosition = vec3(colourMat * vec4(pos, 1.0f));

		//if ((pos.x != 0 || worldPosition.x <= (-1.5 - pad)) && (pos.x != 1 || worldPosition.x >= (0.5 + pad)) &&
		//	(pos.y != 0 || worldPosition.y <= (-1.5 - pad)) && (pos.y != 1 || worldPosition.y >= (0.5 + pad)) && 
		//	(pos.z != 0 || worldPosition.z <= (-1.5 - pad)) && (pos.z != 1 || worldPosition.z >= (0.5 + pad))){

		
		if (((worldPosition.x <= -1.499999f - pad) || (worldPosition.x >= 1.499999f + pad)) ||
			((worldPosition.y <= -1.499999f - pad) || (worldPosition.y >= 1.499999f + pad)) ||
			((worldPosition.z <= -1.499999f - pad) || (worldPosition.z >= 1.499999f + pad))){

			vec3 ambient = ambientStrength * lightColor.xyz;
			vec3 result = ambient * vertexColor.xyz;		
			color = vec4(result, 1.0f);
		}
	} 

#endif
#if __VERSION__ == 120

	varying vec4 vertexColor; // The input variable from the vertex shader (same name and same type)
	varying vec3 pos;

	uniform vec4 lightColor;
	uniform float pad;
	uniform float ambientStrength;
	//uniform vec3 worldPosition;
	uniform mat4 colourMat;
  
	void main()
	{
		vec3 worldPosition = vec3(colourMat * vec4(pos, 1.0f));
					
		//if ((pos.x != 0 || worldPosition.x <= -1.5 - pad) && (pos.x != 1 || worldPosition.x >= 0.5 + pad) &&
		//	(pos.y != 0 || worldPosition.y <= -1.5 - pad) && (pos.y != 1 || worldPosition.y >= 0.5 + pad) && 
		//	(pos.z != 0 || worldPosition.z <= -1.5 - pad) && (pos.z != 1 || worldPosition.z >= 0.5 + pad)){

		
		if (((worldPosition.x <= -1.499999f - pad) || (worldPosition.x >= 1.499999f + pad)) ||
			((worldPosition.y <= -1.499999f - pad) || (worldPosition.y >= 1.499999f + pad)) ||
			((worldPosition.z <= -1.499999f - pad) || (worldPosition.z >=  1.499999f + pad))){


			vec3 ambient = ambientStrength * lightColor.xyz;
			vec3 result = ambient * vertexColor.xyz ;
			gl_FragColor = vec4(result, 1.0f);
			}
	} 

#endif