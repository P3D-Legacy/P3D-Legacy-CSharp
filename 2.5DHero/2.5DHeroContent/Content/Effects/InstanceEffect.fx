#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4	World;
float4x4	View;
float4x4	Projection;

texture		Texture;
sampler TextureSampler = sampler_state
{
	Texture		=	<Texture>;
	
	MipFilter	=	POINT;
	MinFilter	=	POINT;
	MagFilter	=	POINT;
	
	AddressU	=	WRAP;
	AddressV	=	WRAP;
};

struct VertexShaderInput
{
	float4 GeometryPosition	:	POSITION0;
	float3 GeometryNormal	:	NORMAL;
	float2 InstanceTexCoord	:	TEXCOORD0;
	float4x4 InstanceWorld	:	TEXCOORD1;
};

struct VertexShaderOutput
{
	float4 Position	:	POSITION0;
	float2 TexCoord	:	TEXCOORD0;
	float4 Normal	:	TEXCOORD1;
};

VertexShaderOutput InstancingVS(VertexShaderInput input)
{
	VertexShaderOutput output;
	
	input.GeometryPosition.w = 1.0f;
	
    //input.GeometryPosition.x += input.InstancePosition.x;
    //input.GeometryPosition.y += input.InstancePosition.y;
    //input.GeometryPosition.z += input.InstancePosition.z;
	
	output.Position = mul(input.GeometryPosition, input.InstanceWorld);
    output.Position = mul(output.Position, View);
    output.Position = mul(output.Position, Projection);

	output.Normal = mul(input.GeometryNormal, input.InstanceWorld);
	
	output.TexCoord = input.InstanceTexCoord;

	return output;
}

float4 InstancingPS(VertexShaderOutput input) : COLOR0
{
	return tex2D(TextureSampler, input.TexCoord);
}

technique Instancing
{
	pass P0
	{
		VertexShader	=	compile	VS_SHADERMODEL	InstancingVS();
		PixelShader		=	compile	PS_SHADERMODEL	InstancingPS();
	}
};