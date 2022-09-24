#version 330 core 

layout (location = 0) in vec3 aPos;
uniform vec3 vColor;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

out vec3 fColor;

void main()
{
	gl_Position = uModel * vec4(aPos, 1.0);
	fColor = vColor;
}
