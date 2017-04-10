float4x4 World;
float4x4 View;
float4x4 Projection;

texture DiffuseTexture;

float3 CameraPos;
float3 LightPosition;
float3 LightDiffuseColor;

float3 LightSpecularColor;
float LightDistanceSquared;
float3 DiffuseColor;

float3 AmbientLightColor;
float3 EmissiveColor;
float3 SpecularColor;

float SpecularPower = 0.75f;
 
sampler texsampler = sampler_state
{
	Texture = (DiffuseTexture);
	MagFilter = Point;
    MinFilter = Anisotropic;
    MipFilter = Linear;
    MaxAnisotropy = 16;
};
 
struct VertexShaderInput
{
	float4 Position : POSITION0;
	float3 Normal : NORMAL0;
	float2 TexCoords : TEXCOORD0;
};
 
struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float3 WorldPos : TEXCOORD2;
};
 
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;
 
	float4x4 viewprojection = mul(View, Projection);
	float4 posWorld = mul(input.Position, World);
	output.Position = mul(posWorld, viewprojection);
	output.TexCoords = input.TexCoords;
 
	output.Normal = mul(input.Normal, (float3x3)World);
	output.WorldPos = posWorld;
 
	return output;
}
 
float4 PixelShaderFunctionWithTex(VertexShaderOutput input) : COLOR0
{
	float3 lightDir = normalize(input.WorldPos - LightPosition);
 
	float diffuseLighting = saturate(dot(input.Normal, -lightDir));

	diffuseLighting *= (LightDistanceSquared / dot(LightPosition - input.WorldPos, LightPosition - input.WorldPos));

	float3 h = normalize(normalize(CameraPos - input.WorldPos) - lightDir);

	float specLighting = pow(saturate(dot(h, input.Normal)), SpecularPower);

	float4 texel = tex2D(texsampler, input.TexCoords);

	float4 c = float4(saturate(AmbientLightColor 
		+ (texel.xyz * DiffuseColor * LightDiffuseColor * diffuseLighting * 0.6) 
		+ (SpecularColor * LightSpecularColor * specLighting * 0.5)), texel.w);

	return c;
}

float4 PixelShaderFunctionSimpleDebug(VertexShaderOutput input) : COLOR0
{
	float4 texel = tex2D(texsampler, input.TexCoords);

	return texel;
}

technique TechniqueWithTexture
{
	pass Normal
	{
	    VertexShader = compile vs_2_0 VertexShaderFunction();
	    PixelShader = compile ps_2_0 PixelShaderFunctionWithTex();
	}

	pass Debug
	{
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunctionSimpleDebug();
	}
}