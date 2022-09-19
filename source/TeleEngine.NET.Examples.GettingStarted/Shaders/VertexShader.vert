#version 330 core 

layout (location = 0) in vec3 aPos;
uniform vec3 vColor;
uniform mat4 model;
out vec3 fColor;

void main()
{
	gl_Position = model * vec4(aPos, 1.0);
	fColor = vColor;
}
