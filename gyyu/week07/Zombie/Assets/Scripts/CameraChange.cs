using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Object = UnityEngine.Object;

public class CameraChange : MonoBehaviour
{
	
	public CinemachineVirtualCamera firstView;
	public CinemachineVirtualCamera topView;
	public CinemachineVirtualCamera aimView;
	private PlayerInput playerInput; // 플레이어의 입력
	private void Awake() {

		playerInput = FindObjectOfType<PlayerInput>();
	}

	void Update() {

		if (playerInput != null)
		{
			if (playerInput.topView)
				SetTopView();
			if (playerInput.firstView)
				SetFirstView();
			if (playerInput.aimView)
			{
				if (topView.Priority == 1)
					return;
				if (aimView.Priority == 1)
					SetFirstView();
				else
					SetAimView();
			}
		}
	}

	private void SetTopView() {
		topView.Priority = 1;
		firstView.Priority = 0;
		aimView.Priority = 0;
	}
	private void SetFirstView() {
		topView.Priority = 0;
		firstView.Priority = 1;
		aimView.Priority = 0;
	}
	private void SetAimView() {
		topView.Priority = 0;
		firstView.Priority = 0;
		aimView.Priority = 1;
	}
}
