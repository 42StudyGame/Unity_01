using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

// 총을 구현
public class Gun : MonoBehaviour {
    // 총의 상태를 표현하는 데 사용할 타입을 선언
    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄알집이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 탄알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 탄알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기

    public GunData gunData; // 총의 현재 데이터

    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남은 전체 탄알
    public int magAmmo; // 현재 탄알집에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();
		
		bulletLineRenderer.positionCount = 2;   // 사용할 점을 두개로 변경
        bulletLineRenderer.enabled = false;     // 라인렌더러 비활성화
    }

    private void OnEnable() {
		ammoRemain = gunData.startAmmoRemain;	// 전체 예비 탄알 양을 초기화한다?
		magAmmo = gunData.magCapacity;			// 탄알을 가득채운다.

		state = State.Ready;	// 상태를 Ready 상태로 변경해서 총을 쏠 준비가 되었음을 알린다.
		lastFireTime = 0;		// 마지막총을 쏜 시점 초기화
	}

    // 발사 시도
    public void Fire() {
		// ready 상태이고, 마지막 총 발사 시점에서 gunData.timeBetFire(총알 발사 시간) 만큼 시간이 지났는지 확인
		if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
		{
			lastFireTime = Time.time;
			Shot();
		}
    }

    // 실제 발사 처리
    private void Shot() {
		RaycastHit hit;						// 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
		Vector3 hitPosition = Vector3.zero;	// 탄알이 맞은 곳을 저장할 벡터 변수
		
		if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
		{
			IDamageable target = hit.collider.GetComponent<IDamageable>();
			if (target != null)
			{
				target.OnDamage(gunData.damage, hit.point, hit.normal);
			}
			hitPosition = hit.point;
		}
		else
		{
			hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
		}

		StartCoroutine(ShotEffect(hitPosition));
		magAmmo--;
		if (magAmmo <= 0)
		{
			state = State.Empty;
		}
	}

    // 발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator ShotEffect(Vector3 hitPosition) {
		muzzleFlashEffect.Play();
		shellEjectEffect.Play();
		gunAudioPlayer.PlayOneShot(gunData.shotClip); // 총격 소리 재생
		bulletLineRenderer.SetPosition(0, fireTransform.position); //총알 발사 위치
		bulletLineRenderer.SetPosition(1, hitPosition);			//충돌 위치
		
        // 라인 렌더러를 활성화하여 탄알 궤적을 그림
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 탄알 궤적을 지움
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool Reload() {
		if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
			return false;
		else
		{
			StartCoroutine(ReloadRoutine());
			return true;
		}
	}

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;
		gunAudioPlayer.PlayOneShot(gunData.reloadClip);
        // 재장전 소요 시간 만큼 처리 쉬기
        yield return new WaitForSeconds(gunData.reloadTime);
		int ammoToFill = gunData.magCapacity - magAmmo; // 탄창의 크기 - 탄창에 있는 탄알

		if (ammoRemain < ammoToFill)
			ammoToFill = ammoRemain;
		magAmmo += ammoToFill;
		ammoRemain -= ammoToFill;
		// 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }
}