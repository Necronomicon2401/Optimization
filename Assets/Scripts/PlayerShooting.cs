using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public bool spreadShot = false;

	[Header("General")]
	public Transform gunBarrel;
	public ParticleSystem shotVFX;
	public AudioSource shotAudio;
	public float fireRate = .1f;
	public int spreadAmount = 20;
	public int bulletsInPoolCount = 10000;

	[Header("Bullets")]
	public GameObject bulletPrefab;

	float timer;
	public GameObject[] _bulletsArray;

	void Start()
	{
		_bulletsArray = new GameObject[bulletsInPoolCount];
		for (int i = 0; i < bulletsInPoolCount; i++)
		{
			GameObject bullet = Instantiate(bulletPrefab) as GameObject;
			
			bullet.SetActive(false);
			_bulletsArray[i] = bullet;
		}
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= fireRate)
		{
			Vector3 rotation = gunBarrel.rotation.eulerAngles;
			rotation.x = 0f;

			if (spreadShot)
				SpawnBulletSpread(rotation);
			else
				SpawnBullet(rotation);
			

			timer = 0f;

			if (shotVFX)
				shotVFX.Play();

			if (shotAudio)
				shotAudio.Play();
		}
	}

	void SpawnBullet(Vector3 rotation)
	{
		//var bullet = Array.Find(_bulletsArray, g => !g.activeSelf);
		var bullet = TryToGetBullet();
		bullet.SetActive(true);

		bullet.transform.position = gunBarrel.position;
		bullet.transform.rotation = Quaternion.Euler(rotation);
	}

	void SpawnBulletSpread(Vector3 rotation)
	{
		int max = spreadAmount / 2;
		int min = -max;

		Vector3 tempRot = rotation;
		for (int x = min; x < max; x++)
		{
			tempRot.x = (rotation.x + 3 * x) % 360;

			for (int y = min; y < max; y++)
			{
				tempRot.y = (rotation.y + 3 * y) % 360;

				//var bullet = Array.Find(_bulletsArray, g => !g.activeSelf);
				var bullet = TryToGetBullet();
				bullet.SetActive(true);

				bullet.transform.position = gunBarrel.position;
				bullet.transform.rotation = Quaternion.Euler(tempRot);
			}
		}
	}

	GameObject TryToGetBullet()
	{
		var bullet = Array.Find(_bulletsArray, g => !g.activeSelf);
		if (bullet == default (GameObject))
		{
			ResizePool();
			bullet = Array.Find(_bulletsArray, g => !g.activeSelf);
		}

		return bullet;
	}

	void ResizePool()
	{
		var bulletsLength = _bulletsArray.Length;
		Array.Resize(ref _bulletsArray, _bulletsArray.Length * 2);
		for (int i = bulletsLength; i < _bulletsArray.Length; i++)
		{
			GameObject bullet = Instantiate(bulletPrefab) as GameObject;
			
			bullet.SetActive(false);
			
			_bulletsArray[i] = bullet;
		}
	}

}

