#version 330 core 

layout (location = 0) in vec3 aPos;
uniform vec3 vColor;


uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

out vec3 fColor;

void main()
{
	vec4 transformPosition = uView * uModel * vec4(aPos, 1.0);
	gl_Position = uProjection * transformPosition;
	fColor = vColor;
}
