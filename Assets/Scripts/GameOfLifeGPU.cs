using UnityEngine;

public class GameOfLifeGPU : MonoBehaviour
{
	public int textWidth = 8192;
	public int textHeight = 8192;
	
	[Space]
	public ComputeShader shader;
	public RenderTexture rt;
	
	[Space]
	public Renderer renderer;

	private int kernel;

	private void Start()
	{
		rt = new RenderTexture(textWidth, textHeight, 24);
		rt.enableRandomWrite = true;
		rt.Create();
		
		renderer.material.SetTexture("_MainTex", rt);
						
		InitComputeShader();
		InitSimulation();
	}

	private void Update()
	{
		SetShaderParameters();
		RunComputeShader();		
	}

	private void OnDestroy()
	{
		DeinitComputeShader();
	}

	private void InitSimulation()
	{
		int initKernel = shader.FindKernel("CSInit");		
		shader.SetTexture(initKernel, "Result", rt);
		shader.Dispatch(initKernel, (textWidth / 32) + 1, (textHeight / 32) + 1, 1);
	}

	private void InitComputeShader()
	{
		kernel = shader.FindKernel("CSMain");
	}

	private void SetShaderParameters()
	{
		shader.SetTexture(kernel, "Result", rt);		
	}
	
	private void RunComputeShader()
	{
		shader.Dispatch(kernel, (textWidth / 32) + 1, (textHeight / 32) + 1, 1);
	}

	private void DeinitComputeShader()
	{
		rt.Release();
	}
}