#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix World;
matrix View;
matrix Projection;

float4 Color;

struct VertexShaderInput {
	float4 Position : SV_POSITION;
};

struct VertexShaderOutput {
	float4 Position : POSITION0;
	float4 PositionObject : TEXCOORD0;
};

struct PixelShaderOutput {
	float4 Color : COLOR0;
};

VertexShaderOutput DefaultVertexShader(VertexShaderInput input) {
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);

	output.Position = mul(viewPosition, Projection);
	output.PositionObject = input.Position;

	return output;
}

PixelShaderOutput DefaultPixelShader(VertexShaderOutput input) {
	PixelShaderOutput output;

	float4 topColor = Color;
	float4 bottomColor = 1;

	output.Color = lerp(bottomColor, topColor, input.PositionObject.z / 1.1);

	return output;
}

technique Default {
	pass P0 {
		VertexShader = compile VS_SHADERMODEL DefaultVertexShader();
		PixelShader = compile PS_SHADERMODEL DefaultPixelShader();
	}
};